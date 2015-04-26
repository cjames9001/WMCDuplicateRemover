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
                if(String.IsNullOrEmpty(_title) && _scheduledEvent != null)
                    _title = _scheduledEvent.GetExtendedProperty("Title").ToString();
                return _title;
            }
        }

        public override String ServiceID
        {
            get
            {
                if(_scheduledEvent != null)
                return _scheduledEvent.GetExtendedProperty("ServiceID").ToString();
                return String.Empty;
            }
        }

        public override String ChannelID
        {
            get
            {
                if(_scheduledEvent != null)
                return _scheduledEvent.GetExtendedProperty("ChannelID").ToString();
                return String.Empty;
            }
        }

        private string _description;
        public override String Description
        {
            get
            {
                if(String.IsNullOrEmpty(_description) && _scheduledEvent != null)
                    _description = _scheduledEvent.GetExtendedProperty("Description").ToString();
                return _description;
            }
        }

        public override int KeepUntil
        {
            get
            {
                if(_scheduledEvent != null)
                return Convert.ToInt32(_scheduledEvent.GetExtendedProperty("KeepUntil"));
                return 0;
            }
        }

        public override int Quality
        {
            get
            {
                if(_scheduledEvent!= null)
                return Convert.ToInt32(_scheduledEvent.GetExtendedProperty("Quality"));
                return 0;
            }
        }

        public override bool Partial
        {
            get
            {
                if(_scheduledEvent!= null)
                return Convert.ToBoolean(_scheduledEvent.GetExtendedProperty("Partial"));

                return false;
            }
        }

        public override String ProviderCopyright
        {
            get
            {
                if(_scheduledEvent != null)
                return _scheduledEvent.GetExtendedProperty("ProviderCopyright").ToString();
                return String.Empty;
            }
        }

        public override DateTime OriginalAirDate
        {
            get
            {
                if(_scheduledEvent != null)
                return Convert.ToDateTime(_scheduledEvent.GetExtendedProperty("OriginalAirDate")).ToLocalTime();
                return DateTime.MinValue;
            }
        }

        public override bool Repeat
        {
            get
            {
                if(_scheduledEvent != null)
                return Convert.ToBoolean(_scheduledEvent.GetExtendedProperty("Repeat"));
                return false;
            }
        }

        public override String Genre
        {
            get
            {
                if(_scheduledEvent!= null)
                return _scheduledEvent.GetExtendedProperty("Genre").ToString();
                return String.Empty;
            }
        }

        public override String FileName
        {
            get
            {
                if(_scheduledEvent!= null)
                return _scheduledEvent.GetExtendedProperty("FileName").ToString();
                return String.Empty;
            }
        }

        public override DateTime StartTime
        {
            get
            {
                if(_scheduledEvent != null)
                return _scheduledEvent.StartTime.ToLocalTime();
                return DateTime.MinValue;
            }
        }

        public override DateTime EndTime
        {
            get
            {
                if(_scheduledEvent!= null)
                return _scheduledEvent.EndTime.ToLocalTime();
                return DateTime.MinValue;
            }
        }

        public override ScheduleEventStates State
        {
            get
            {
                if(_scheduledEvent!=null)
                return _scheduledEvent.State;
                return ScheduleEventStates.None;
            }
        }

        #endregion

        public override void CancelEvent()
        {
            _scheduledEvent.Cancel();
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}{2}Series Title: {3}{2}Episode Title: {4}{2}Description: {5}{2}ServiceID: {6}{2}ChannelID: {7}{2}" + 
                "Keep Until: {8}{2}Quality: {9}{2}Partial: {10}{2}Provider Copyright: {11}{2}Original Air Date: {12}{2}Repeat: {13}{2}Genre: {14}{2}" + 
                "File Name: {15}{2}Start Time: {16}{2}End Time: {17}{2}State: {18}{2}",
                this.GetType().Namespace,
                this.GetType().Name,
                Environment.NewLine,
                String.IsNullOrEmpty(Title) ? "" : Title,
                EpisodeTitle,
                String.IsNullOrEmpty(Description) ? "" : Description,
                String.IsNullOrEmpty(ServiceID) ? "" : ServiceID,
                String.IsNullOrEmpty(ChannelID) ? "" : ChannelID,
                KeepUntil == null ? "" : KeepUntil.ToString(),
                Quality == null ? "" : Quality.ToString(),
                Partial == null ? "" : Partial.ToString(),
                String.IsNullOrEmpty(ProviderCopyright) ? "" : ProviderCopyright,
                OriginalAirDate == null ? "" : OriginalAirDate.ToString(),
                Repeat == null ? "" : Repeat.ToString(),
                String.IsNullOrEmpty(Genre) ? "" : Genre,
                String.IsNullOrEmpty(FileName) ? "" : FileName,
                StartTime == null ? "" : StartTime.ToString(),
                EndTime == null ? "" : EndTime.ToString(),
                State == null ? "" : State.ToString());
        }
    }
}
