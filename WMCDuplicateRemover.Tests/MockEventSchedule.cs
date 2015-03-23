using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WMCDuplicateRemover;

namespace WMCDuplicateRemover.Tests
{
    internal class MockEventSchedule : IEventSchedule
    {
        private List<IScheduledEvent> _scheduledEvents;
        public MockEventSchedule(List<IScheduledEvent> scheduledEvents)
        {
            _scheduledEvents = scheduledEvents;
        }

        public List<IScheduledEvent> GetEventsScheduledToRecord()
        {
            return _scheduledEvents;
        }
    }
}
