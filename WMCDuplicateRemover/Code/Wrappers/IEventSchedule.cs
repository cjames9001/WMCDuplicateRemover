using System.Collections.Generic;

namespace WMCDuplicateRemover
{
    public interface IEventSchedule
    {
        IEnumerable<ScheduledEvent> GetEventsScheduledToRecord();
    }
}
