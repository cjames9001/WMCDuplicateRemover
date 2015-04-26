using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover
{
    public class EventLogEntryWrapper : EntryWrapper
    {
        public override bool FoundEventForRecording(String seriesName, String episodeName)
        {
            var formattedEventData = String.Format("{0}: {1}", seriesName, episodeName).ToLower();

            var eventLog = new EventLog("Media Center");

            foreach (EventLogEntry entry in eventLog.Entries)
            {
                if (entry.Source == "Recording" && entry.Message.ToLower().Contains(formattedEventData))
                    return true;
            }

            return false;
        }
    }
}
