using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using WMCDuplicateRemover.Code.Serialization;

namespace WMCDuplicateRemover
{
    public class MicrosoftEventLogWrapper : EventLogWrapper
    {
        private HashSet<string> eventLogCache;
        private static string programDataFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\WMCDuplicateRemover";
        private readonly string eventLogEntryCacheFilePath = Path.Combine(programDataFolder, "eventLogEntries.cache");

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
            if (string.IsNullOrEmpty(seriesName) || string.IsNullOrEmpty(episodeName))
                return false;

            //These are exceptions since I had to go and delete off these because WMC screwed up its DRM and I wasn't actually
            //finished watching these
            //TODO: Add the ability to add exceptions to this program without hard coding here
            //if (seriesName.ToLower() == "True Detective".ToLower() || seriesName.ToLower() == "Silicon Valley".ToLower())
            //    return false;

            var formattedEventData = CleanRecordingName($"{seriesName}: {episodeName}");

            return eventLogCache.Contains(formattedEventData);
        }

        private string CleanRecordingName(string recordingName)
        {
            return recordingName.Replace("'", string.Empty).Trim().ToLower();
        }
    }
}
