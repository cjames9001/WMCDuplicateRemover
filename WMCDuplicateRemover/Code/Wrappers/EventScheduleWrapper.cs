using System;
using System.Collections.Generic;

using Microsoft.MediaCenter.TV.Scheduling;

namespace WMCDuplicateRemover
{
    public class EventScheduleWrapper : IEventSchedule
    {
        private ICollection<ScheduleEvent> _scheduledEvents;

        public List<ScheduledEvent> GetEventsScheduledToRecord()
        {
            EventSchedule eventScheduler = new EventSchedule();
            _scheduledEvents = eventScheduler.GetScheduleEvents(DateTime.Now, DateTime.Now.AddDays(30), ScheduleEventStates.WillOccur);
            return ConvertEventsToWrapperType();
        }

        private List<ScheduledEvent> ConvertEventsToWrapperType()
        {
            List<ScheduledEvent> wrapperTypedScheduledEvents = new List<ScheduledEvent>();
            foreach(var evnt in _scheduledEvents)
            {
                wrapperTypedScheduledEvents.Add(new ScheduledEventWrapper(evnt));
            }

            return wrapperTypedScheduledEvents;
        }
    }
}
