using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Diagnostics; 
using Microsoft.MediaCenter; 
using Microsoft.MediaCenter.Hosting; 
using Microsoft.MediaCenter.UI;
using WMCDuplicateRemover.Code.EPG;

namespace WMCDuplicateRemover
{
    public class ScheduledEventProperties
    {
        public const String Title = "Title";
        public const String ServiceID = "ServiceID";
        public const String ChannelID = "ChannelID";
        public const String Description = "Description";
        public const String KeepUntil = "KeepUntil";
        public const String Quality = "Quality";
        public const String Partial = "Partial";
        public const String ProviderCopyright = "ProviderCopyright";
        public const String OriginalAirDate = "OriginalAirDate";
        public const String Repeat = "Repeat";
        public const String Genre = "Genre";
        public const String FileName = "FileName";
    }

    public class Application : ModelItem
    {
        private AddInHost host;
        private HistoryOrientedPageSession session;
        private static String programDataFolder = String.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "WMCDuplicateRemover");
        private readonly string logPath = Path.Combine(programDataFolder, "WMCDuplicateRemover.log");

        public Application()
            : this(null, null)
        {
        }
        public Application(HistoryOrientedPageSession session, AddInHost host)
        {
            this.session = session;
            this.host = host;
        }
        public MediaCenterEnvironment MediaCenterEnvironment
        {
            get
            {
                if (host == null)
                    return null;
                return host.MediaCenterEnvironment;
            }
        }

        public void GoToMenu()
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties["Application"] = this;

            if (session != null)
            {
                session.GoToPage("resx://WMCDuplicateRemover/WMCDuplicateRemover.Resources/Menu", properties);
            }
            else
            {
                Debug.WriteLine("GoToMenu");
            }
        }

        public void DialogTest(string strClickedText)
        {
            int timeout = 0;
            bool modal = true;
            string caption = Resources.DialogCaption;

            if (session != null)
            {
                MediaCenterEnvironment.Dialog(strClickedText, caption, new object[] { DialogButtons.Ok }, timeout, modal, null, delegate(DialogResult dialogResult) { });
            }
            else
            {
                Debug.WriteLine("DialogTest");
            }
        }

        public List<String> MyData
        {
            get
            {
                try
                {
                    var eventScheduler = new EventScheduleWrapper();
                    var scheduledEvents = eventScheduler.GetEventsScheduledToRecord();
                    scheduledEvents.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
                    List<String> duplicateScheduledEvents = new List<String>();
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    UpdateEPG();
                    ProcessDuplicates(scheduledEvents, duplicateScheduledEvents);
                    stopwatch.Stop();
                    AppendTextToFile(String.Format("Finished Processing: {0}/{1} Cancelled in {2}", duplicateScheduledEvents.Count, scheduledEvents.Count, stopwatch.Elapsed));
                    return duplicateScheduledEvents;
                }
                catch(Exception ex)
                {
                    MediaCenterEnvironment.Dialog(String.Format("There was an error processing:\n{0}", ex.Message), Resources.DialogCaption, new object[] { DialogButtons.Ok }, 0, true, null, delegate(DialogResult dialogResult) { });
                    String logText = String.Format("Error processing Exception Message: {1}{0}Inner Exception: {2}{0}Stack Trace: {3}{0}", Environment.NewLine, ex.Message, ex.InnerException.ToString(), ex.StackTrace);
                    AppendTextToFile(logText);
                    return new List<String>();
                }
            }         
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
            var eventLogWrapper = new EventLogEntryWrapper();

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
                catch(Exception ex)
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