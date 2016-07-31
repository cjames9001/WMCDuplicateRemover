using Microsoft.MediaCenter.TV.Scheduling;
using NUnit.Framework;
using System;
using System.Collections;

namespace WMCDuplicateRemover.Tests.ScheduledEventWrapper
{
    public class ScheduledEventTestsCanEventBeCancelledTestDataUnit
    {
        internal ScheduledEvent ScheduledEvent { get; set; }
        internal bool EventLogFoundEventForRecordingResponse { get; set; }
        internal bool ExpectedResult { get; set; }
    }

    internal class ScheduledEventTestsCanEventBeCancelledTests : TestData
    {
        protected override IEnumerable CreateTestData()
        {
            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "Marge follows Bart around in an effort to get him to confess being involved in a bulldozer crash; Homer ignores Santa's Little Helper when Flanders brings a new dog home.",
                false,
                new DateTime(2015, 4, 19, 7, 0, 0),
                false,
                new DateTime(2015, 4, 19, 7, 0, 0),
                ScheduleEventStates.WillOccur),
                ExpectedResult = true,
                EventLogFoundEventForRecordingResponse = true
            })
                .SetDescription("Check that an original air date in the past with an Event Log response of true can be cancelled.")
                .SetName("PastOriginalAirDateEventLogResponseTrue");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "Mary Spuckler (guest voice Zooey Deschanel) returns to Springfield and pulls Bart's heartstring once again; Homer winds up in the dog house.",
                false,
                DateTime.MinValue,
                true,
                new DateTime(2015, 4, 24, 9, 0, 0),
                ScheduleEventStates.WillOccur),
                ExpectedResult = false,
                EventLogFoundEventForRecordingResponse = true
            })
                .SetDescription("Check that an original air date as min date value with an Event Log response of true cannot be cancelled.")
                .SetName("MinDateForOriginalAirDateEventLogResponseTrue");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "A yellow family grows up in Springfield.",
                false,
                DateTime.Now,
                true,
                new DateTime(9999, 4, 24, 9, 0, 0),
                ScheduleEventStates.WillOccur),
                ExpectedResult = false,
                EventLogFoundEventForRecordingResponse = true
            })
                .SetDescription("Check that original air date of today and Event Log response of true can't be cancelled")
                .SetName("TestOriginalAirDateTodayEventLogResponseTrue");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "A yellow family grows up in Springfield.",
                false,
                DateTime.Now.AddDays(5),
                true,
                DateTime.MinValue,
                ScheduleEventStates.WillOccur),
                ExpectedResult = false,
                EventLogFoundEventForRecordingResponse = true
            })
                .SetDescription("Check that original air date is in the future and event log response true cannot cancel recording")
                .SetName("TestOriginalAirDateInFutureEventLogResponseTrue");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "Marge follows Bart around in an effort to get him to confess being involved in a bulldozer crash; Homer ignores Santa's Little Helper when Flanders brings a new dog home.",
                false,
                new DateTime(2015, 4, 19, 7, 0, 0),
                false,
                new DateTime(2015, 4, 19, 7, 0, 0),
                ScheduleEventStates.WillOccur),
                ExpectedResult = false,
                EventLogFoundEventForRecordingResponse = false
            })
                .SetDescription("Check that an original air date in the past with an Event Log response of false cannot be cancelled.")
                .SetName("PastOriginalAirDateEventLogResponseFalse");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "Mary Spuckler (guest voice Zooey Deschanel) returns to Springfield and pulls Bart's heartstring once again; Homer winds up in the dog house.",
                false,
                DateTime.MinValue,
                true,
                new DateTime(2015, 4, 24, 9, 0, 0),
                ScheduleEventStates.WillOccur),
                ExpectedResult = false,
                EventLogFoundEventForRecordingResponse = false
            })
                .SetDescription("Check that an original air date as min date value with an Event Log response of false cannot be cancelled.")
                .SetName("MinDateForOriginalAirDateEventLogResponseFalse");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "A yellow family grows up in Springfield.",
                false,
                DateTime.Now,
                true,
                new DateTime(9999, 4, 24, 9, 0, 0),
                ScheduleEventStates.WillOccur),
                ExpectedResult = false,
                EventLogFoundEventForRecordingResponse = false
            })
                .SetDescription("Check that original air date of today and Event Log response of false can't be cancelled")
                .SetName("TestOriginalAirDateTodayEventLogResponseFalse");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "A yellow family grows up in Springfield.",
                false,
                DateTime.Now.AddDays(5),
                true,
                DateTime.MinValue,
                ScheduleEventStates.WillOccur),
                ExpectedResult = false,
                EventLogFoundEventForRecordingResponse = false
            })
                .SetDescription("Check that original air date is in the future and event log response false cannot cancel recording")
                .SetName("TestOriginalAirDateInFutureEventLogResponseFalse");
        }
    }
}