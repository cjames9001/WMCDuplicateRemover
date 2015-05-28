using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using WMCDuplicateRemover;
using WMCDuplicateRemover.Code.EPG;

namespace WMCDuplicateRemoverDriver
{
    public class Driver
    {
        private static String programDataFolder = String.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "WMCDuplicateRemover");
        private readonly string logPath = Path.Combine(programDataFolder, "WMCDuplicateRemover.log");

        public void Run()
        {
            AppendTextToFile("Begin Processing");
            var eventScheduler = new EventScheduleWrapper();
            var scheduledEvents = eventScheduler.GetEventsScheduledToRecord();
            scheduledEvents.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
            List<String> duplicateScheduledEvents = new List<String>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            AppendTextToFile("Updating EPG");
            UpdateEPG();
            AppendTextToFile(String.Format("EPG Updated After {0}", stopwatch.Elapsed));
            ProcessDuplicates(scheduledEvents, duplicateScheduledEvents);
            stopwatch.Stop();
            AppendTextToFile(String.Format("Finished Processing: {0}/{1} Cancelled in {2}\n", duplicateScheduledEvents.Count, scheduledEvents.Count, stopwatch.Elapsed));

            //var a = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Media Center\\Settings\\TVConfig", "iConfigured", null);
        }

        private void UpdateEPG()
        {
            var epgDownloadProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "mc2xml.exe"
                }
            };

            var cwd = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(programDataFolder);

            epgDownloadProcess.Start();
            epgDownloadProcess.WaitForExit();

            Directory.SetCurrentDirectory(cwd);
        }

        private void ProcessDuplicates(List<ScheduledEvent> scheduledEvents, List<String> duplicateScheduledEvents)
        {
            var eventLogWrapper = new MicrosoftEventLogWrapper();

            foreach (var scheduledEvent in scheduledEvents)
            {
                var episode = new Episode();
                try
                {
                    var tv = new TV(Path.Combine(programDataFolder, "xmltv.xml"));
                    episode = tv.GetEpisodeMetaDataBasedOnWMCMetaData(scheduledEvent.StartTime, scheduledEvent.EndTime, scheduledEvent.OriginalAirDate, Convert.ToInt32(scheduledEvent.ChannelID));
                    if (scheduledEvent.CanEventBeCancelled(eventLogWrapper, episode))
                    {
                        var scheduledEventText = String.Format("{0}State: {1}{2}Partial: {3}{2}Repeat: {4}{2}",
                            episode.ToString(),
                            scheduledEvent.State,
                            Environment.NewLine,
                            scheduledEvent.Partial.ToString(),
                            scheduledEvent.Repeat.ToString());

                        duplicateScheduledEvents.Add(scheduledEventText);
                        SendDuplicateInfoToFile(scheduledEventText);
                        scheduledEvent.CancelEvent();
                    }
                }
                catch (Exception ex)
                {
                    if (!ex.Message.Contains("There are no episodes found for this listing"))
                    {
                        String logText = String.Format("Error processing {0}{1}Exception Message: {2}{1}Inner Exception: {3}{1}Stack Trace: {4}{1}", episode.ToString(), Environment.NewLine, ex.Message, ex.InnerException != null ? ex.InnerException.ToString() : "No Inner Exception", ex.StackTrace);
                        AppendTextToFile(logText);
                    }
                }
            }
        }

        private void AppendTextToFile(String logText)
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

        private void SendDuplicateInfoToFile(String scheduledEventText)
        {
            String logText = String.Format("Cancelled: " + scheduledEventText);
            AppendTextToFile(logText);
        }

        private static string FormatLogText(string textToLog)
        {
            return String.Format("{0}: {1}{2}", DateTime.Now.ToString(), textToLog, Environment.NewLine);
        }
    }
}
