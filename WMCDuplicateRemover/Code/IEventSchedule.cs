using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover
{
    interface IEventSchedule
    {
        List<IScheduledEvent> GetEventsScheduledToRecord();
    }
}
