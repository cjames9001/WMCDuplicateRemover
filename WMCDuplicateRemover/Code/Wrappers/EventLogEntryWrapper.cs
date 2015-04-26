using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover.Code.Wrappers
{
    public class EventLogEntryWrapper : EntryWrapper
    {
        public override bool FoundEventForRecording(String seriesName, String episodeName)
        {
            var formattedEventData = String.Format("{0}: {1}", seriesName, episodeName);

            var eventLog = new EventLog("Media Center");

            foreach (EventLogEntry entry in eventLog.Entries)
            {
                if (entry.Source == "Recording" && entry.Message.Contains(formattedEventData))
                    return true;
            }

            return false;
        }
    }
}
