using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.MediaCenter.TV.Scheduling;

namespace WMCDuplicateRemover
{
    public interface IScheduledEvent
    {
        String Title { get; }
        String ServiceID { get; }
        String ChannelID { get; }
        String Description { get; }
        int KeepUntil { get; }
        int Quality { get; }
        bool Partial { get; }
        String ProviderCopyright { get; }
        DateTime OriginalAirDate { get; }
        bool Repeat { get; }
        String Genre { get; }
        String FileName { get; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        ScheduleEventStates State { get; }

        void CancelEvent();
        bool CanEventBeCancelled();
    }
}
