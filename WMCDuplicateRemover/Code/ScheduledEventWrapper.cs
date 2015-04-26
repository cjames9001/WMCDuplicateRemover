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

        public ScheduledEventWrapper(ScheduleEvent scheduledEvent)
        {
            _scheduledEvent = scheduledEvent;
        }

        private String title;
        public override String Title
        { 
            get 
            {
                if(title == null || title == string.Empty)
                    title = _scheduledEvent.GetExtendedProperty("Title").ToString();
                return title;
            } 
        }

        public override String ServiceID
        {
            get
            {
                return _scheduledEvent.GetExtendedProperty("ServiceID").ToString();
            }
        }

        public override String ChannelID
        {
            get
            {
                return _scheduledEvent.GetExtendedProperty("ChannelID").ToString();
            }
        }

        private string description;
        public override String Description
        {
            get
            {
                if(description == null || description == string.Empty)
                    description = _scheduledEvent.GetExtendedProperty("Description").ToString();
                return description;
            }
        }

        public override int KeepUntil
        {
            get
            {
                return Convert.ToInt32(_scheduledEvent.GetExtendedProperty("KeepUntil"));
            }
        }

        public override int Quality
        {
            get
            {
                return Convert.ToInt32(_scheduledEvent.GetExtendedProperty("Quality"));
            }
        }

        public override bool Partial
        {
            get
            {
                return Convert.ToBoolean(_scheduledEvent.GetExtendedProperty("Partial"));
            }
        }

        public override String ProviderCopyright
        {
            get
            {
                return _scheduledEvent.GetExtendedProperty("ProviderCopyright").ToString();
            }
        }

        public override DateTime OriginalAirDate
        {
            get
            {
                return Convert.ToDateTime(_scheduledEvent.GetExtendedProperty("OriginalAirDate")).ToLocalTime();
            }
        }

        public override bool Repeat
        {
            get
            {
                return Convert.ToBoolean(_scheduledEvent.GetExtendedProperty("Repeat"));
            }
        }

        public override String Genre
        {
            get
            {
                return _scheduledEvent.GetExtendedProperty("Genre").ToString();
            }
        }

        public override String FileName
        {
            get
            {
                return _scheduledEvent.GetExtendedProperty("FileName").ToString();
            }
        }

        public override DateTime StartTime
        {
            get
            {
                return _scheduledEvent.StartTime.ToLocalTime();
            }
        }

        public override DateTime EndTime
        {
            get
            {
                return _scheduledEvent.EndTime.ToLocalTime();
            }
        }

        public override ScheduleEventStates State
        {
            get
            {
                return _scheduledEvent.State;
            }
        }

        public override void CancelEvent()
        {
            _scheduledEvent.Cancel();
        }

        protected override bool EventLogInformationAllowsForCancellation(EntryWrapper entryWrapper, MetaDataWrapper metaDataWrapper)
        {
            return true;
        }
    }
}
