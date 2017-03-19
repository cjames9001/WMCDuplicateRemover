using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WMCDuplicateRemover.Code.Serialization;
using UserSettings = WMCDuplicateRemover.Properties.Settings;

namespace WMCDuplicateRemover
{
    public class MicrosoftEventLogWrapper : EventLogWrapper
    {
        private HashSet<string> eventLogCache;
        private readonly string eventLogEntryCacheFilePath = Path.Combine(StaticValues.WMCDuplicateRemoverApplicationDataFolder, "eventLogEntries.cache");

        public MicrosoftEventLogWrapper()
        {
            eventLogCache = new HashSet<string>();

            var eventLog = new EventLog("Media Center");

            foreach (EventLogEntry entry in eventLog.Entries)
            {
                var entryWrapper = new EventLogEntryWrapper(entry.Message, entry.Source, entry.InstanceId);
                if (entryWrapper.IsValidRecordingEntry)
                    eventLogCache.Add(CleanRecordingName(entryWrapper.RecordingName));
            }

            var existingEventLogCahceEntries = GetExistingEventLogCache();
            eventLogCache.UnionWith(existingEventLogCahceEntries);
            CreateFileCache(eventLogCache);
        }

        private HashSet<string> GetExistingEventLogCache()
        {
            var objectDeserializer = new ObjectDeserializer();
            var previouslyCachedEventLogEntries = objectDeserializer.ReadFromBinaryFile<HashSet<string>>(eventLogEntryCacheFilePath);
            return previouslyCachedEventLogEntries;
        }

        private void CreateFileCache(HashSet<string> eventLogCache)
        {
            var objectSerializer = new ObjectSerializer();
            objectSerializer.WriteToBinaryFile(eventLogEntryCacheFilePath, eventLogCache);
        }

        public override bool FoundEventForRecording(string seriesName, string episodeName)
        {
            //Can't be cancelling shows we only have part of the information needed!
            if (string.IsNullOrWhiteSpace(seriesName) || string.IsNullOrWhiteSpace(episodeName))
                return false;

            var exlusions = UserSettings.Default.Exclusions.Cast<string>().Select(x => x.ToLower());
            if (exlusions.Contains(seriesName.ToLower()))
                return false;

            var formattedEventData = CleanRecordingName($"{seriesName}: {episodeName}");

            return eventLogCache.Contains(formattedEventData);
        }

        private string CleanRecordingName(string recordingName)
        {
            return recordingName.Replace("'", string.Empty).Trim().ToLower();
        }
    }
}
