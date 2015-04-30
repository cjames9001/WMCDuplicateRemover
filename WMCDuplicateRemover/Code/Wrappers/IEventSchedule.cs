using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover
{
    public interface IEventSchedule
    {
        List<ScheduledEvent> GetEventsScheduledToRecord();
    }
}
