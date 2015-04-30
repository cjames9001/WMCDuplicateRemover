using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover
{
    public class EventLogEntryWrapper : EntryWrapper
    {
        private List<String> eventLogCache;

        public EventLogEntryWrapper()
        {
            eventLogCache = new List<String>();

            var eventLog = new EventLog("Media Center");

            foreach (EventLogEntry entry in eventLog.Entries)
            {
                if (entry.Source == "Recording")
                    eventLogCache.Add(entry.Message.Trim().ToLower());
            }
        }

        public override bool FoundEventForRecording(String seriesName, String episodeName)
        {
            //Can't be cancelling shows we only have part of the information needed!
            if (String.IsNullOrEmpty(seriesName) || String.IsNullOrEmpty(episodeName))
                return false;

            var formattedEventData = String.Format("{0}: {1}", seriesName, episodeName).Trim().ToLower();

            foreach (String entry in eventLogCache)
            {
                if (entry.Contains(formattedEventData))
                    return true;
            }

            return false;
        }
    }
}
