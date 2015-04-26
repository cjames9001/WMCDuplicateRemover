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
            //Can't be cancelling shows we only have part of the information needed!
            if (String.IsNullOrEmpty(seriesName) || String.IsNullOrEmpty(episodeName))
                return false;

            var formattedEventData = String.Format("{0}: {1}", seriesName, episodeName).Trim().ToLower();

            var eventLog = new EventLog("Media Center");

            foreach (EventLogEntry entry in eventLog.Entries)
            {
                if (entry.Source == "Recording" && entry.Message.Trim().ToLower().Contains(formattedEventData))
                    return true;
            }

            return false;
        }
    }
}
