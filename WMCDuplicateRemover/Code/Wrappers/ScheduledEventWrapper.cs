using System;

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
    
        #endregion

        #region Properties

        private string _title;
        public override string Title
        { 
            get 
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(_title))
                        _title = _scheduledEvent.GetExtendedProperty("Title").ToString();
                    return _title;
                }
                catch(NullReferenceException)
                {
                    return string.Empty;
                }
            }
        }
        
        public override string ServiceId
        {
            get
            {
                try
                {
                    return _scheduledEvent.GetExtendedProperty("ServiceID").ToString();
                }
                catch(NullReferenceException)
                {
                    return string.Empty;
                }
            }
        }

        public override string ChannelId
        {
            get
            {
                try
                {
                    return _scheduledEvent.GetExtendedProperty("ChannelID").ToString();
                }
                catch(NullReferenceException)
                {
                    return string.Empty;
                }
            }
        }

        private string _description;
        public override string Description
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(_description))
                        _description = _scheduledEvent.GetExtendedProperty("Description").ToString();
                    return _description;
                }
                catch(NullReferenceException)
                {
                    return string.Empty;
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
                catch(NullReferenceException)
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
                catch(NullReferenceException)
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
                catch(NullReferenceException)
                {
                    return false;
                }
            }
        }

        public override string ProviderCopyright
        {
            get
            {
                try
                {
                    return _scheduledEvent.GetExtendedProperty("ProviderCopyright").ToString();
                }
                catch(NullReferenceException)
                {
                    return string.Empty;
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
                catch(NullReferenceException)
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
                catch(NullReferenceException)
                {
                    return false;
                }
            }
        }

        public override string Genre
        {
            get
            {
                try
                {
                    return _scheduledEvent.GetExtendedProperty("Genre").ToString();
                }
                catch(NullReferenceException)
                {
                    return string.Empty;
                }
            }
        }

        public override string FileName
        {
            get
            {
                try
                {
                    return _scheduledEvent.GetExtendedProperty("FileName").ToString();
                }
                catch(NullReferenceException)
                {
                    return string.Empty;
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
                catch(NullReferenceException)
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
                catch(NullReferenceException)
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
                catch(NullReferenceException)
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
    }
}
