using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Diagnostics; 
using Microsoft.MediaCenter; 
using Microsoft.MediaCenter.Hosting; 
using Microsoft.MediaCenter.UI;

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
        private readonly string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "WMCDuplicateRemoverDryRun.log");

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
                    ProcessDuplicates(scheduledEvents, duplicateScheduledEvents);

                    return duplicateScheduledEvents;
                }
                catch(Exception ex)
                {
                    MediaCenterEnvironment.Dialog(String.Format("There was an error processing:\n{0}", ex.Message), Resources.DialogCaption, new object[] { DialogButtons.Ok }, 0, true, null, delegate(DialogResult dialogResult) { });
                    return new List<String>();
                }
            }         
        }

        private void ProcessDuplicates(List<ScheduledEvent> scheduledEvents, List<String> duplicateScheduledEvents)
        {
            foreach (var scheduledEvent in scheduledEvents)
            {
                try
                {
                    var metaData = new TheTVDBWrapper(scheduledEvent.Title, scheduledEvent.OriginalAirDate);
                    if (scheduledEvent.CanEventBeCancelled(new EventLogEntryWrapper(), metaData))
                    {
                        var scheduledEventText = String.Format("StartTime:{0} Title:{1}\nOriginal Air:{2}\nDescription:{3}\nState:{4}\nPartial:{5}\nIsRepeat{6}",
                            scheduledEvent.StartTime.ToString(),
                            scheduledEvent.Title,
                            scheduledEvent.OriginalAirDate.ToShortDateString(),
                            scheduledEvent.Description,
                            scheduledEvent.State.ToString(),
                            scheduledEvent.Partial.ToString(),
                            scheduledEvent.Repeat.ToString());

                        duplicateScheduledEvents.Add(scheduledEventText);

                        SendDuplicateInfoToFile(scheduledEvent.ToString());
                    }
                }
                catch(Exception ex)
                {
                    String logText = String.Format("Error processing {0}{1}Exception Message: {2}{1}Inner Exception: {3}{1}Stack Trace: {4}{1}", scheduledEvent.ToString(), Environment.NewLine, ex.Message, ex.InnerException.ToString(), ex.StackTrace);
                    AppendTextToFile(logText);
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

        private void SendDuplicateInfoToFile(string scheduledEventText)
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