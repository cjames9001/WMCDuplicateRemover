using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WMCDuplicateRemover;
using Microsoft.MediaCenter.TV.Scheduling;

namespace WMCDuplicateRemover.Tests
{
    public class MockScheduledEvent : IScheduledEvent
    {
        public MockScheduledEvent()
        {
            _title = "";
            _serviceId = "";
            _channelId = "";
            _description = "";
            _keepUntil = 0;
            _quality = 0;
            _partial = false;
            _providerCopyright = "";
            _originalAirDate = new DateTime();
            _repeat = false;
            _genre = "";
            _fileName = "";
            _startTime = new DateTime();
            _endTime = new DateTime();
            _state = ScheduleEventStates.None;
        }

        public MockScheduledEvent(string title, string description, bool partial, DateTime originalAirDate, bool repeat, DateTime startTime, ScheduleEventStates state) : this()
        {
            _title = title;
            _description = description;
            _partial = partial;
            _originalAirDate = originalAirDate;
            _repeat = repeat;
            _startTime = startTime;
            _state = state;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
        }

        private string _serviceId;
        public string ServiceID
        {
            get { return _serviceId; }
        }

        private string _channelId;
        public string ChannelID
        {
            get { return _channelId; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
        }

        private int _keepUntil;
        public int KeepUntil
        {
            get { return _keepUntil; }
        }

        private int _quality;
        public int Quality
        {
            get { return _quality; }
        }

        private bool _partial;
        public bool Partial
        {
            get { return _partial; }
        }

        private string _providerCopyright;
        public string ProviderCopyright
        {
            get { return _providerCopyright; }
        }

        private DateTime _originalAirDate;
        public DateTime OriginalAirDate
        {
            get { return _originalAirDate; }
        }

        private bool _repeat;
        public bool Repeat
        {
            get { return _repeat; }
        }

        private string _genre;
        public string Genre
        {
            get { return _genre; }
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
        }

        private DateTime _startTime;
        public DateTime StartTime
        {
            get { return _startTime; }
        }

        private DateTime _endTime;
        public DateTime EndTime
        {
            get { return _endTime; }
        }

        private ScheduleEventStates _state;
        public ScheduleEventStates State
        {
            get { return _state; }
        }

        public void CancelEvent()
        {
            //Event Gets Cancelled
        }

        public bool CanEventBeCancelled()
        {
            if (OriginalAirDate.Date == DateTime.MinValue.Date)
                return false;
            if (Repeat && OriginalAirDate < DateTime.Now)
                return true;
            return false;
        }
    }
}
