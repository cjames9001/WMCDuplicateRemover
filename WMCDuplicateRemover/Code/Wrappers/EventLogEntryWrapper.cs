using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WMCDuplicateRemover
{
    public class EventLogEntryWrapper
    {
        private const string WMC_RECORDING_SOURCE = "Recording";

        enum RecordingInstanceType
        {
            CompletedSuccessfuly = 1,
            BeganWhileInProgress = 2,
            HadSignalLoss = 3,
            PartialRecordingDueToPowerLossOrTemporaryFailure = 4,
            NotRecordedDueToConflict = 6,
            NotRecordedDueToSystemMalfunctionOrPowerLoss = 7,
            TunerNotWorkingOrInstalled = 8,
            ManuallyDeleted = 17,
            ScheduledRecordingRemoved = 21,
            Cancelled = 24,
        }
        
        //TODO: Make the valid instances configurable, that way the control is left to the user as to which circumstances should count as a success.
        private List<long> ValidInstanceIds = new List<long>() { (long)RecordingInstanceType.CompletedSuccessfuly, (long)RecordingInstanceType.Cancelled };
        private string Message { get; set; }
        public long InstanceId { get; private set; }
        public string Source { get; private set; }

        public string RecordingName
        {
            get
            {
                return GetRecordingNameFromMessage();
            }
        }

        private string GetRecordingNameFromMessage()
        {
            switch (InstanceId)
            {
                case 1:
                    return ParseRecordingNameWithExpressions("", " started recording on .* and stopped on .* as scheduled.$");
                case 2:
                    return ParseRecordingNameWithExpressions("Recording of ", " began as scheduled on .* while the program was already in progress.$");
                case 3:
                    return ParseRecordingNameWithExpressions("Recording of ", " began as scheduled on .* and was manually stopped on .*.$");
                case 17:
                    return ParseRecordingNameWithExpressions("", " was manually deleted on .* by .*.$");
                default:
                    return Message;
            }
        }

        private string ParseRecordingNameWithExpressions(string prefixRegex, string suffixRegex)
        {
            var recordingName = Message;
            var regex = Regex.Match(Message, prefixRegex);

            if (regex.Success)
            {
                recordingName = recordingName.Substring(regex.Length, recordingName.Length - regex.Length);
            }

            regex = Regex.Match(recordingName, suffixRegex);

            if(regex.Success)
            {
                return recordingName.Substring(0, regex.Index);
            }
            return recordingName;
        }

        public bool IsValidRecordingEntry
        {
            get
            {
                return Source == WMC_RECORDING_SOURCE && ValidInstanceIds.Contains(InstanceId) && RecordingName.Contains(':');
            }
        }

        public EventLogEntryWrapper(string message, string source, long instanceId)
        {
            Message = message;
            InstanceId = instanceId;
            Source = source;
        }
    }
}
