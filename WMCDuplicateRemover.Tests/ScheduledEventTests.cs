using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Microsoft.MediaCenter.TV.Scheduling;

namespace WMCDuplicateRemover.Tests
{
    [TestFixture]
    public class ScheduledEventTests
    {
        IScheduledEvent _scheduledEvent;

        [SetUp]
        public void SetUp()
        {
            _scheduledEvent = new MockScheduledEvent(
                "The Daily Show", 
                "Jon Stewart Discusses Current Events", 
                false, 
                new DateTime(2015, 3, 23, 10, 0, 0), 
                false, 
                new DateTime(2015, 3, 23, 10, 0, 0), 
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur);

            
        }

        [Test]
        public void TestCreateScheduledEvent()
        {
            Assert.AreEqual("The Daily Show", _scheduledEvent.Title);
            Assert.AreEqual("Jon Stewart Discusses Current Events", _scheduledEvent.Description);
            Assert.False(_scheduledEvent.Partial);
            Assert.AreEqual(new DateTime(2015, 3, 23, 10, 0, 0), _scheduledEvent.OriginalAirDate);
            Assert.False(_scheduledEvent.Repeat);
            Assert.AreEqual(new DateTime(2015, 3, 23, 10, 0, 0), _scheduledEvent.StartTime);
            Assert.AreEqual(ScheduleEventStates.WillOccur, _scheduledEvent.State);
        }
    }
}
