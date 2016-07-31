using System;
using System.Collections.Generic;

using Microsoft.MediaCenter.TV.Scheduling;
using System.Linq;

namespace WMCDuplicateRemover
{
    public class EventScheduleWrapper : IEventSchedule
    {
        private ICollection<ScheduleEvent> _scheduledEvents;

        public IEnumerable<ScheduledEvent> GetEventsScheduledToRecord()
        {
            EventSchedule eventScheduler = new EventSchedule();
            _scheduledEvents = eventScheduler.GetScheduleEvents(DateTime.Now, DateTime.Now.AddDays(30), ScheduleEventStates.WillOccur);
            return ConvertEventsToWrapperType();
        }

        private IEnumerable<ScheduledEvent> ConvertEventsToWrapperType()
        {
            return _scheduledEvents.Select(x => new ScheduledEventWrapper(x));
        }
    }
}
