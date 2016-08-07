using System;

using Microsoft.MediaCenter.TV.Scheduling;
using WMCDuplicateRemover.Code.EPG;

namespace WMCDuplicateRemover
{
    public abstract class ScheduledEvent
    {
        public abstract string Title { get; }
        public abstract string ServiceId { get; }
        public abstract string ChannelId { get; }
        public abstract string Description { get; }
        public abstract int KeepUntil { get; }
        public abstract int Quality { get; }
        public abstract bool Partial { get; }
        public abstract string ProviderCopyright { get; }
        public abstract DateTime OriginalAirDate { get; }
        public abstract bool Repeat { get; }
        public abstract string Genre { get; }
        public abstract string FileName { get; }
        public abstract DateTime StartTime { get; }
        public abstract DateTime EndTime { get; }
        public abstract ScheduleEventStates State { get; }
        public string EpisodeTitle { get; internal set; }

        public abstract void CancelEvent();
        
        internal bool WMCMetaDataAllowsForCancellation()
        {
            if (OriginalAirDate.Date == DateTime.MinValue.Date || OriginalAirDate.Date == DateTime.Now.Date)
                return false;
            if (Repeat && OriginalAirDate < DateTime.Now)
                return true;
            return false;
        }

        public bool CanEventBeCancelled(EventLogWrapper entryWrapper, Episode episode)
        {
            bool canEventBeCancelled = episode.MetaDataAllowsForCancellation() && EventLogInformationAllowsForCancellation(entryWrapper, episode);
            EpisodeTitle = episode.EpisodeName;
            return canEventBeCancelled;
        }

        internal bool EventLogInformationAllowsForCancellation(EventLogWrapper entryWrapper, Episode episode)
        {
            return entryWrapper.FoundEventForRecording(episode.SeriesName, episode.EpisodeName);
        }

        public override string ToString()
        {
            try
            {
                return $"{GetType().Namespace}.{GetType().Name}{Environment.NewLine}Series Title: {Title}{Environment.NewLine}Episode Title: {EpisodeTitle}{Environment.NewLine}Description: {Description}{Environment.NewLine}ServiceId: {ServiceId}{Environment.NewLine}ChannelId: {ChannelId}{Environment.NewLine}Keep Until: {KeepUntil}{Environment.NewLine}Quality: {Quality}{Environment.NewLine}Partial: {Partial}{Environment.NewLine}Provider Copyright: {ProviderCopyright}{Environment.NewLine}Original Air Date: {OriginalAirDate}{Environment.NewLine}Repeat: {Repeat}{Environment.NewLine}Genre: {Genre}{Environment.NewLine}File Name: {FileName}{Environment.NewLine}Start Time: {StartTime}{Environment.NewLine}End Time: {EndTime}{Environment.NewLine}State: {State}{Environment.NewLine}";
            }
            catch (Exception ex)
            {
                return "UnableTo-Tostring()!" + ex.Message + ex.StackTrace;
            }
        }
    }
}
