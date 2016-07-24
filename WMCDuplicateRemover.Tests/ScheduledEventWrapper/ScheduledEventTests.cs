using System;

using NUnit.Framework;
using Microsoft.MediaCenter.TV.Scheduling;

namespace WMCDuplicateRemover.Tests.ScheduledEventWrapper
{
    [TestFixture]
    public class ScheduledEventTests
    {
        ScheduledEvent _scheduledEvent;

        [TestFixtureSetUp]
        public void SetupFixture()
        {
            SetupHelper.SetupXMLData();
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
            SetupHelper.TearDownXMLData();
        }

        [SetUp]
        public void SetUp()
        {
            _scheduledEvent = new MockScheduledEvent(
                "The Simpsons", 
                "Marge follows Bart around in an effort to get him to confess being involved in a bulldozer crash; Homer ignores Santa's Little Helper when Flanders brings a new dog home.", 
                false, 
                new DateTime(2015, 4, 19, 7, 0, 0), 
                false, 
                new DateTime(2015, 4, 19, 7, 0, 0), 
                ScheduleEventStates.WillOccur);
        }

        [Test]
        public void TestCreateScheduledEvent()
        {
            Assert.AreEqual("The Simpsons", _scheduledEvent.Title);
            Assert.AreEqual("Marge follows Bart around in an effort to get him to confess being involved in a bulldozer crash; Homer ignores Santa's Little Helper when Flanders brings a new dog home.", _scheduledEvent.Description);
            Assert.False(_scheduledEvent.Partial);
            Assert.AreEqual(new DateTime(2015, 4, 19, 7, 0, 0), _scheduledEvent.OriginalAirDate);
            Assert.False(_scheduledEvent.Repeat);
            Assert.AreEqual(new DateTime(2015, 4, 19, 7, 0, 0), _scheduledEvent.StartTime);
            Assert.AreEqual(ScheduleEventStates.WillOccur, _scheduledEvent.State);
        }

        [TestCaseSource(typeof(ScheduledEventTestScheduledEventToStringTests))]
        public void TestScheduledEventToString(ScheduledEvent scheduledEvent, string expectedString)
        {
            Assert.AreEqual(expectedString, scheduledEvent.ToString());
        }
    }
}
