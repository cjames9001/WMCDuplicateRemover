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
                        ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "Workaholics",
                        "Adam goes into a sales slump, so, to help him out, Ders and Blake hire an actor to snap him out of it.",
                        false,
                        new DateTime(2015, 3, 25, 2, 0, 0),
                        false,
                        new DateTime(2015, 3, 25, 2, 0, 0),
                        ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "The Daily Show With Jon Stewart",
                        "Kirby Dick and Amy Ziering.",
                        false,
                        new DateTime(2015, 3, 25, 3, 0, 0),
                        false,
                        new DateTime(2015, 3, 25, 3, 0, 0),
                        ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "The Simpsons",
                        "Home battles mutants in \"The Homega Man\"; Bart morphs into an insect in \"Fly vs. Fly\"; a bewitching Marge appears in \"Easy-Bake Coven.\".",
                        false,
                        new DateTime(1997, 10, 26),
                        true,
                        new DateTime(2015, 3, 27, 2, 0, 0),
                        ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "The Nightly Show With Larry Wilmore",
                        "Larry Wilmore and his panel of guests give their unique views on pop culture and current events.",
                        false,
                        new DateTime(2015, 3, 25),
                        false,
                        new DateTime(2015, 3, 25, 10, 30, 0),
                        ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "The Nightly Show With Larry Wilmore",
                        "Larry Wilmore and his panel of guests give their unique views on pop culture and current events.",
                        false,
                        new DateTime(2015, 3, 26),
                        false,
                        new DateTime(2015, 3, 26, 10, 30, 0),
                        ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "Last Week Tonight With John Oliver",
                        "A satirical look at the week in news, politics and current events.",
                        false,
                        new DateTime(1, 1, 1),
                        false,
                        new DateTime(2015, 3, 31, 8, 45, 0),
                        ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "Last Week Tonight With John Oliver",
                        "A satirical look at the week in news, politics and current events.",
                        false,
                        new DateTime(1, 1, 1),
                        true,
                        new DateTime(2015, 3, 31, 8, 45, 0),
                        ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "Last Week Tonight With John Oliver",
                        "A satirical look at the week in news, politics and current events.",
                        false,
                        new DateTime(2015, 4, 5),
                        false,
                        new DateTime(2015, 4, 5, 10, 0, 0),
                        ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "Tosh.0",
                        "",
                        false,
                        new DateTime(2015, 4, 1),
                        false,
                        new DateTime(2015, 4, 1, 9, 0, 0),
                        ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "Parks and Recreation",
                        "Leslie faces Bobby Newport in a televised debate; Ann, Tom and Chris man the media spin room; Ron works to save a party for Leslie's donors.",
                        false,
                        new DateTime(2012, 4, 26),
                        true,
                        new DateTime(2015, 3, 28, 14, 0, 0),
                        ScheduleEventStates.WillOccur),
                    new MockScheduledEvent(
                        "Parks and Recreation",
                        "Leslie Beats Bobby Newport.",
                        false,
                        new DateTime(2012, 4, 26),
                        true,
                        new DateTime(2015, 3, 28, 14, 0, 0),
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
