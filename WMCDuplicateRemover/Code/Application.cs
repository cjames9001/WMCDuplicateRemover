using System;
using System.Collections.Generic;
using System.Text;

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
                    List<String> scheduledEventNames = new List<String>();
                    foreach (var scheduledEvent in scheduledEvents)
                    {
                        try
                        {
                            scheduledEventNames.Add(
                                String.Format("StartTime:{0}\nTitle:{1}\nOriginal Air:{2}\nDescription {3}\nState:{4}", 
                                scheduledEvent.StartTime.ToString(),
                                scheduledEvent.Title, 
                                scheduledEvent.OriginalAirDate.ToShortDateString(), 
                                scheduledEvent.Description,
                                scheduledEvent.State.ToString()));
                        }
                        catch
                        {
                            //Just trying not to blow up
                        }
                    }

                    scheduledEventNames.Sort();
                    return scheduledEventNames;
                }
                catch
                {
                    return new List<String> { "Alpha", "Bravo", "Charlie", "Delta" };
                }
            }         
        }
    }
}