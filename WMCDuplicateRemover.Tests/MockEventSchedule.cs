using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WMCDuplicateRemover;

namespace WMCDuplicateRemover.Tests
{
    internal class MockEventSchedule : IEventSchedule
    {
        private List<ScheduledEvent> _scheduledEvents;
        public MockEventSchedule(List<ScheduledEvent> scheduledEvents)
        {
            _scheduledEvents = scheduledEvents;
        }

        public List<ScheduledEvent> GetEventsScheduledToRecord()
        {
            return _scheduledEvents;
        }
    }
}
