using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover
{
    public class MicrosoftEventLogWrapper : EventLogWrapper
    {
        private HashSet<String> eventLogCache;

        public MicrosoftEventLogWrapper()
        {
            eventLogCache = new HashSet<String>();
            var eventLogEntries = new List<String>();

            var eventLog = new EventLog("Media Center");

            foreach (EventLogEntry entry in eventLog.Entries)
            {
                var entryWrapper = new EventLogEntryWrapper(entry.Message, entry.Source, entry.InstanceId);
                if (entryWrapper.IsValidRecordingEntry)
                    eventLogCache.Add(CleanRecordingName(entryWrapper.RecordingName));
            }

            eventLogCache.UnionWith(eventLogEntries);
        }

        public override bool FoundEventForRecording(String seriesName, String episodeName)
        {
            //Can't be cancelling shows we only have part of the information needed!
            if (String.IsNullOrEmpty(seriesName) || String.IsNullOrEmpty(episodeName))
                return false;

            //These are exceptions since I had to go and delete off these because WMC screwed up its DRM and I wasn't actually
            //finished watching these
            //TODO: Add the ability to add exceptions to this program without hard coding here
            if (seriesName.ToLower() == "True Detective".ToLower() || seriesName.ToLower() == "Silicon Valley".ToLower())
                return false;

            var formattedEventData = CleanRecordingName(String.Format("{0}: {1}", seriesName, episodeName));

            return eventLogCache.Contains(formattedEventData);
        }

        private String CleanRecordingName(String recordingName)
        {
            return recordingName.Replace("'", String.Empty).Trim().ToLower();
        }
    }
}
