using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMCDuplicateRemover;

namespace WMCDuplicateRemover.Tests.Wrappers
{
    [TestFixture]
    public class EventLogEntryWrapperTests
    {
        [TestFixtureSetUp]
        public void SetUp()
        {

        }

        [TestCaseSource(typeof(EventLogEntryWrapperRecordingNameTestCases))]
        public String TestGetRecordingNameFromEventLogEntry(EventLogEntryWrapper eventLogEntry)
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
