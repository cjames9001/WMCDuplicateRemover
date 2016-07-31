using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WMCDuplicateRemover;
using WMCDuplicateRemover.Code.EPG;
using WMCDuplicateRemover.Code.Wrappers;

namespace WMCDuplicateRemoverDriver
{
    public class Driver
    {
        private static string programDataFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\WMCDuplicateRemover";
        private readonly string logPath = Path.Combine(programDataFolder, "WMCDuplicateRemover.log");

        public void Run()
        {
            AppendTextToFile("Begin Processing");
            var eventScheduler = new EventScheduleWrapper();
            var scheduledEvents = eventScheduler.GetEventsScheduledToRecord().ToList();
            scheduledEvents.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
            List<string> duplicateScheduledEvents = new List<string>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            AppendTextToFile("Updating EPG");
            UpdateEPG();
            var channelsWithScheduledEvents = scheduledEvents.Select(x => int.Parse(x.ChannelId)).Distinct();
            var epgWrapper = new XmlTvEpgWrapper(Path.Combine(programDataFolder, "xmltv.xml"), channelsWithScheduledEvents);
            AppendTextToFile($"EPG Updated After {stopwatch.Elapsed}");
            ProcessDuplicates(scheduledEvents, duplicateScheduledEvents, epgWrapper);
            stopwatch.Stop();
            AppendTextToFile($"Finished Processing: {duplicateScheduledEvents.Count}/{scheduledEvents.Count} Cancelled in {stopwatch.Elapsed}\n");
        }

        private void UpdateEPG()
        {
            var epgDownloadProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "mc2xml.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };

            var cwd = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(programDataFolder);

            using (var process = Process.Start(epgDownloadProcess.StartInfo))
            {
                using (var streamReader = process.StandardOutput)
                {
                    var output = streamReader.ReadToEnd();
                    AppendTextToFile($"MC2XML Output: \n{output}");
                }
            }

            Directory.SetCurrentDirectory(cwd);
        }

        private void ProcessDuplicates(List<ScheduledEvent> scheduledEvents, List<string> duplicateScheduledEvents, EpgWrapper epgWrapper)
        {
            var eventLogWrapper = new MicrosoftEventLogWrapper();

            foreach (var scheduledEvent in scheduledEvents)
            {
                var episode = new Episode();
                try
                {
                    episode = epgWrapper.GetEpisodeMetaDataBasedOnWMCMetaData(scheduledEvent.StartTime, scheduledEvent.EndTime, scheduledEvent.OriginalAirDate, Convert.ToInt32(scheduledEvent.ChannelId));
                    if (scheduledEvent.CanEventBeCancelled(eventLogWrapper, episode))
                    {
                        var scheduledEventText = $"{episode.ToString()}State: {scheduledEvent.State}{Environment.NewLine}Partial: {scheduledEvent.Partial.ToString()}{Environment.NewLine}Repeat: {scheduledEvent.Repeat.ToString()}{Environment.NewLine}";

                        duplicateScheduledEvents.Add(scheduledEventText);
                        SendDuplicateInfoToFile(scheduledEventText);
                        scheduledEvent.CancelEvent();
                    }
                }
                catch (Exception ex)
                {
                    if (!ex.Message.Contains("There are no episodes found for this listing"))
                    {
                        string logText = $"Error processing {episode.ToString()}{Environment.NewLine}Exception Message: {ex.Message}{Environment.NewLine}Inner Exception: {(ex.InnerException != null ? ex.InnerException.ToString() : "No Inner Exception")}{Environment.NewLine}Stack Trace: {ex.StackTrace}{Environment.NewLine}";
                        AppendTextToFile(logText);
                    }
                }
            }
        }

        private void AppendTextToFile(string logText)
        {
            if (!File.Exists(logPath))
            {
                var myfile = File.Create(logPath);
                myfile.Close();
                File.WriteAllText(logPath, FormatLogText(logText));
            }
            else
            {
                File.AppendAllText(logPath, FormatLogText(logText));
            }
        }

        private void SendDuplicateInfoToFile(string scheduledEventText)
        {
            string logText = $"Cancelled: {scheduledEventText}";
            AppendTextToFile(logText);
        }

        private static string FormatLogText(string textToLog)
        {
            return $"{DateTime.Now.ToString()}: {textToLog}{Environment.NewLine}";
        }
    }
}
