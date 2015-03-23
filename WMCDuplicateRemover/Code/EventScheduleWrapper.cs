using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.MediaCenter.TV.Scheduling;

namespace WMCDuplicateRemover
{
    public class EventScheduleWrapper : IEventSchedule
    {
        private ICollection<ScheduleEvent> _scheduledEvents;

        public List<IScheduledEvent> GetEventsScheduledToRecord()
        {
            EventSchedule eventScheduler = new EventSchedule();
            _scheduledEvents = eventScheduler.GetScheduleEvents(DateTime.Now, DateTime.Now.AddDays(30), ScheduleEventStates.WillOccur);
            return ConvertMicrosoftScheduleEventsToWrapperType();
        }

        private List<IScheduledEvent> ConvertMicrosoftScheduleEventsToWrapperType()
        {
            List<IScheduledEvent> wrapperTypedScheduledEvents = new List<IScheduledEvent>();
            foreach(var evnt in _scheduledEvents)
            {
                wrapperTypedScheduledEvents.Add(new ScheduledEventWrapper(evnt));
            }

            return wrapperTypedScheduledEvents;
        }
    }
}
