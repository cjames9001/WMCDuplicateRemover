using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMCDuplicateRemover;

namespace WMCDuplicateRemover.Tests
{
    public class MockEventLogEntryWrapper : EntryWrapper
    {
        public override bool FoundEventForRecording(string seriesName, string episodeName)
        {
            return true;
        }
    }
}
