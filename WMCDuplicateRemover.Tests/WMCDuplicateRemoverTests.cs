using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace WMCDuplicateRemover.Tests
{
    [TestFixture]
    public class WMCDuplicateRemoverTests
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
            Assert.AreEqual(new DateTime(2015, 3, 23, 10, 0, 0), _scheduledEvent.OriginalAirDate);
        }
    }
}
