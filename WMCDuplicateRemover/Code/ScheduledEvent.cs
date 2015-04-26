using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.MediaCenter.TV.Scheduling;

namespace WMCDuplicateRemover
{
    public abstract class ScheduledEvent
    {
        public abstract String Title { get; }
        public abstract String ServiceID { get; }
        public abstract String ChannelID { get; }
        public abstract String Description { get; }
        public abstract int KeepUntil { get; }
        public abstract int Quality { get; }
        public abstract bool Partial { get; }
        public abstract String ProviderCopyright { get; }
        public abstract DateTime OriginalAirDate { get; }
        public abstract bool Repeat { get; }
        public abstract String Genre { get; }
        public abstract String FileName { get; }
        public abstract DateTime StartTime { get; }
        public abstract DateTime EndTime { get; }
        public abstract ScheduleEventStates State { get; }
        public String EpisodeTitle { get; private set; }

        public abstract void CancelEvent();

        internal bool EventLogInformationAllowsForCancellation(EntryWrapper entryWrapper, MetaDataWrapper metaDataWrapper)
        {
            return entryWrapper.FoundEventForRecording(metaDataWrapper.SeriesName, metaDataWrapper.GetEpisodeTitleFromOriginalAirDate());
        }

        internal bool WMCMetaDataAllowsForCancellation()
        {
            if (OriginalAirDate.Date == DateTime.MinValue.Date || OriginalAirDate.Date == DateTime.Now.Date)
                return false;
            if (Repeat && OriginalAirDate < DateTime.Now)
                return true;
            return false;
        }

        public bool CanEventBeCancelled(EntryWrapper entryWrapper, MetaDataWrapper metaDataWrapper)
        {
            bool canEventBeCancelled = WMCMetaDataAllowsForCancellation() && EventLogInformationAllowsForCancellation(entryWrapper, metaDataWrapper);
            EpisodeTitle = metaDataWrapper.EpisodeTitle;
            return canEventBeCancelled;
        }
    }
}
