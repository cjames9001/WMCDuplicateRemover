using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WMCDuplicateRemover;

namespace WMCDuplicateRemover.Tests
{
    internal class MockEventSchedule : IEventSchedule
    {
        public List<IScheduledEvent> GetEventsScheduledToRecord()
        {
            throw new NotImplementedException();
        }
    }
}
