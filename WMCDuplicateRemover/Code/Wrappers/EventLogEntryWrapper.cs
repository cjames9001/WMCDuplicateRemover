using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WMCDuplicateRemover
{
    public class EventLogEntryWrapper
    {
        private const String WMC_RECORDING_SOURCE = "Recording";
        private List<long> ValidInstanceIds = new List<long>() { 1, 2, 3, 17, 24 };
        private String Message { get; set; }
        public long InstanceId { get; private set; }
        public String Source { get; private set; }

        public String RecordingName
        {
            get
            {
                return GetRecordingNameFromMessage();
            }
        }

        private String GetRecordingNameFromMessage()
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

        private String ParseRecordingNameWithExpressions(String prefixRegex, String suffixRegex)
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

        public EventLogEntryWrapper(String message, String source, long instanceId)
        {
            Message = message;
            InstanceId = instanceId;
            Source = source;
        }
    }
}
