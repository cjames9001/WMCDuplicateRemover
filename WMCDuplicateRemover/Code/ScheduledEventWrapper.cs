using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.MediaCenter.TV.Scheduling;

namespace WMCDuplicateRemover
{
    public class ScheduledEventWrapper : ScheduledEvent
    {
        private ScheduleEvent _scheduledEvent;

        #region Constructors

        public ScheduledEventWrapper(ScheduleEvent scheduledEvent)
        {
            _scheduledEvent = scheduledEvent;
        }

        public ScheduledEventWrapper()
        {
            // TODO: Complete member initialization
        }

        #endregion

        #region Properties

        private String _title;
        public override String Title
        { 
            get 
            {
                try
                {
                    if (String.IsNullOrEmpty(_title))
                        _title = _scheduledEvent.GetExtendedProperty("Title").ToString();
                    return _title;
                }
                catch
                {
                    return String.Empty;
                }
            }
        }

        public override String ServiceID
        {
            get
            {
                try
                {
                    return _scheduledEvent.GetExtendedProperty("ServiceID").ToString();
                }
                catch
                {
                    return String.Empty;
                }
            }
        }

        public override String ChannelID
        {
            get
            {
                try
                {
                    return _scheduledEvent.GetExtendedProperty("ChannelID").ToString();
                }
                catch
                {
                    return String.Empty;
                }
            }
        }

        private string _description;
        public override String Description
        {
            get
            {
                try
                {
                    if (String.IsNullOrEmpty(_description))
                        _description = _scheduledEvent.GetExtendedProperty("Description").ToString();
                    return _description;
                }
                catch
                {
                    return String.Empty;
                }
            }
        }

        public override int KeepUntil
        {
            get
            {
                try
                {
                    return Convert.ToInt32(_scheduledEvent.GetExtendedProperty("KeepUntil"));
                }
                catch
                {
                    return 0;
                }
            }
        }

        public override int Quality
        {
            get
            {
                try
                {
                    return Convert.ToInt32(_scheduledEvent.GetExtendedProperty("Quality"));
                }
                catch
                {
                    return 0;
                }
            }
        }

        public override bool Partial
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(_scheduledEvent.GetExtendedProperty("Partial"));
                }
                catch
                {
                    return false;
                }
            }
        }

        public override String ProviderCopyright
        {
            get
            {
                try
                {
                    return _scheduledEvent.GetExtendedProperty("ProviderCopyright").ToString();
                }
                catch
                {
                    return String.Empty;
                }
            }
        }

        public override DateTime OriginalAirDate
        {
            get
            {
                try
                {
                    return Convert.ToDateTime(_scheduledEvent.GetExtendedProperty("OriginalAirDate")).ToLocalTime();
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
        }

        public override bool Repeat
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(_scheduledEvent.GetExtendedProperty("Repeat"));
                }
                catch
                {
                    return false;
                }
            }
        }

        public override String Genre
        {
            get
            {
                try
                {
                    return _scheduledEvent.GetExtendedProperty("Genre").ToString();
                }
                catch
                {
                    return String.Empty;
                }
            }
        }

        public override String FileName
        {
            get
            {
                try
                {
                    return _scheduledEvent.GetExtendedProperty("FileName").ToString();
                }
                catch
                {
                    return String.Empty;
                }
            }
        }

        public override DateTime StartTime
        {
            get
            {
                try
                {
                    return _scheduledEvent.StartTime.ToLocalTime();
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
        }

        public override DateTime EndTime
        {
            get
            {
                try
                {
                    return _scheduledEvent.EndTime.ToLocalTime();
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
        }

        public override ScheduleEventStates State
        {
            get
            {
                try
                {
                    return _scheduledEvent.State;
                }
                catch
                {
                    return ScheduleEventStates.None;
                }
            }
        }

        #endregion

        public override void CancelEvent()
        {
            _scheduledEvent.Cancel();
        }

        public override string ToString()
        {
            try
            {
                return String.Format("{0}.{1}{2}Series Title: {3}{2}Episode Title: {4}{2}Description: {5}{2}ServiceID: {6}{2}ChannelID: {7}{2}" +
                    "Keep Until: {8}{2}Quality: {9}{2}Partial: {10}{2}Provider Copyright: {11}{2}Original Air Date: {12}{2}Repeat: {13}{2}Genre: {14}{2}" +
                    "File Name: {15}{2}Start Time: {16}{2}End Time: {17}{2}State: {18}{2}",
                    this.GetType().Namespace,
                    this.GetType().Name,
                    Environment.NewLine,
                    Title,
                    EpisodeTitle,
                    Description,
                    ServiceID,
                    ChannelID,
                    KeepUntil,
                    Quality,
                    Partial,
                    ProviderCopyright,
                    OriginalAirDate,
                    Repeat,
                    Genre,
                    FileName,
                    StartTime,
                    EndTime,
                    State);
            }
            catch (Exception ex)
            {
                return "UnableTo-Tostring()!" + ex.Message + ex.StackTrace;
            }
        }
    }
}
