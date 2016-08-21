using NUnit.Framework;

namespace WMCDuplicateRemover.Tests.Wrappers
{
    [TestFixture]
    public class EventLogEntryWrapperTests
    {
        [TestCaseSource(typeof(EventLogEntryWrapperRecordingNameTestCases))]
        public string TestGetRecordingNameFromEventLogEntry(EventLogEntryWrapper eventLogEntry)
        {
            return eventLogEntry.RecordingName;
        }

        [TestCaseSource(typeof(EventLogEntryWrapperValidRecordingEntryTestCases))]
        public bool TestEventLogIsValidRecordingEntry(EventLogEntryWrapper eventLogEntry)
        {
            return eventLogEntry.IsValidRecordingEntry;
        }
    }
}
