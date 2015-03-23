using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Microsoft.MediaCenter.TV.Scheduling;

namespace WMCDuplicateRemover.Tests
{
    [TestFixture]
    public class WMCDuplicateRemoverTests
    {
        IScheduledEvent _scheduledEvent;
        IEventSchedule _eventSchedule;

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

            _eventSchedule = new MockEventSchedule(
                new List<IScheduledEvent>
                {
                    new MockScheduledEvent(
                        "The Daily Show", 
                        "Jon Stewart Discusses Current Events", 
                        false, 
                        new DateTime(2015, 3, 23, 10, 0, 0), 
                        false, 
                        new DateTime(2015, 3, 23, 10, 0, 0), 
                        Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "The Daily Show", 
                        "Jon Stewart Discusses Current Events", 
                        false, 
                        new DateTime(2015, 3, 24, 10, 0, 0), 
                        false, 
                        new DateTime(2015, 3, 24, 10, 0, 0), 
                        ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "The Simpsons", 
                        "Bart and Lisa organize a celebrity-studded comeback special for an out-of-work Krusty the Clown.", 
                        false,
                        new DateTime(1993, 5, 13, 10, 0, 0), 
                        false, 
                        new DateTime(2015, 3, 23, 18, 30, 0), 
                        ScheduleEventStates.WillOccur)
                }
                );
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
