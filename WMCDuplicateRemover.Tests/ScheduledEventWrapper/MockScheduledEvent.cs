using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WMCDuplicateRemover;
using Microsoft.MediaCenter.TV.Scheduling;

namespace WMCDuplicateRemover.Tests
{
    public class MockScheduledEvent : ScheduledEvent
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

        public MockScheduledEvent(string title, string description, bool partial, DateTime originalAirDate, bool repeat, DateTime startTime, DateTime endTime, int channel, ScheduleEventStates state)
            : this()
        {
            _title = title;
            _description = description;
            _partial = partial;
            _originalAirDate = originalAirDate;
            _repeat = repeat;
            _startTime = startTime;
            _endTime = endTime;
            _channelId = channel.ToString();
            _state = state;
        }

        private string _title;
        public override string Title
        {
            get { return _title; }
        }

        private string _serviceId;
        public override string ServiceID
        {
            get { return _serviceId; }
        }

        private string _channelId;
        public override string ChannelID
        {
            get { return _channelId; }
        }

        private string _description;
        public override string Description
        {
            get { return _description; }
        }

        private int _keepUntil;
        public override int KeepUntil
        {
            get { return _keepUntil; }
        }

        private int _quality;
        public override int Quality
        {
            get { return _quality; }
        }

        private bool _partial;
        public override bool Partial
        {
            get { return _partial; }
        }

        private string _providerCopyright;
        public override string ProviderCopyright
        {
            get { return _providerCopyright; }
        }

        private DateTime _originalAirDate;
        public override DateTime OriginalAirDate
        {
            get { return _originalAirDate; }
        }

        private bool _repeat;
        public override bool Repeat
        {
            get { return _repeat; }
        }

        private string _genre;
        public override string Genre
        {
            get { return _genre; }
        }

        private string _fileName;
        public override string FileName
        {
            get { return _fileName; }
        }

        private DateTime _startTime;
        public override DateTime StartTime
        {
            get { return _startTime; }
        }

        private DateTime _endTime;
        public override DateTime EndTime
        {
            get { return _endTime; }
        }

        private ScheduleEventStates _state;
        public override ScheduleEventStates State
        {
            get { return _state; }
        }

        public override void CancelEvent()
        {
            //Event Gets Cancelled
        }
    }
}
