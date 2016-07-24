using System.Collections.Generic;

namespace WMCDuplicateRemover
{
    public interface IEventSchedule
    {
        List<ScheduledEvent> GetEventsScheduledToRecord();
    }
}
