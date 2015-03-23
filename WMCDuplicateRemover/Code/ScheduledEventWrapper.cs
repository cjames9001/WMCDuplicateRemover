﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.MediaCenter.TV.Scheduling;

namespace WMCDuplicateRemover
{
    public class ScheduledEventWrapper : IScheduledEvent
    {
        private ScheduleEvent _scheduledEvent;

        public ScheduledEventWrapper(ScheduleEvent scheduledEvent)
        {
            _scheduledEvent = scheduledEvent;
        }

        public String Title
        { 
            get 
            { 
                return _scheduledEvent.GetExtendedProperty("Title").ToString(); 
            } 
        }

        public String ServiceID
        {
            get
            {
                return _scheduledEvent.GetExtendedProperty("ServiceID").ToString();
            }
        }

        public String ChannelID
        {
            get
            {
                return _scheduledEvent.GetExtendedProperty("ChannelID").ToString();
            }
        }

        public String Description
        {
            get
            {
                return _scheduledEvent.GetExtendedProperty("Description").ToString();
            }
        }

        public int KeepUntil
        {
            get
            {
                return Convert.ToInt32(_scheduledEvent.GetExtendedProperty("KeepUntil"));
            }
        }

        public int Quality
        {
            get
            {
                return Convert.ToInt32(_scheduledEvent.GetExtendedProperty("Quality"));
            }
        }

        public bool Partial
        {
            get
            {
                return Convert.ToBoolean(_scheduledEvent.GetExtendedProperty("Partial"));
            }
        }

        public String ProviderCopyright
        {
            get
            {
                return _scheduledEvent.GetExtendedProperty("ProviderCopyright").ToString();
            }
        }

        public DateTime OriginalAirDate
        {
            get
            {
                return Convert.ToDateTime(_scheduledEvent.GetExtendedProperty("OriginalAirDate"));
            }
        }

        public bool Repeat
        {
            get
            {
                return Convert.ToBoolean(_scheduledEvent.GetExtendedProperty("Repeat"));
            }
        }

        public String Genre
        {
            get
            {
                return _scheduledEvent.GetExtendedProperty("Genre").ToString();
            }
        }

        public String FileName
        {
            get
            {
                return _scheduledEvent.GetExtendedProperty("FileName").ToString();
            }
        }

        public DateTime StartTime
        {
            get
            {
                return _scheduledEvent.StartTime;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return _scheduledEvent.EndTime;
            }
        }

        public ScheduleEventStates State
        {
            get
            {
                return _scheduledEvent.State;
            }
        }

        public void CancelEvent()
        {
            _scheduledEvent.Cancel();
        }
    }
}
