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
                .SetDescription("Check that repeat can't be cancelled")
                .SetName("TestCannotCancelNonRepeat");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "Mary Spuckler (guest voice Zooey Deschanel) returns to Springfield and pulls Bart's heartstring once again; Homer winds up in the dog house.",
                false,
                new DateTime(2015, 4, 24, 9, 0, 0),
                true,
                new DateTime(2015, 4, 24, 9, 0, 0),
                ScheduleEventStates.WillOccur),
                ExpectedResult = true,
                EventLogFoundEventForRecordingResponse = true
            })
                .SetDescription("Check that repeat can be cancelled")
                .SetName("TestCanCancelRepeat");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "A yellow family grows up in Springfield.",
                false,
                new DateTime(9999, 4, 24, 9, 0, 0),
                true,
                new DateTime(9999, 4, 24, 9, 0, 0),
                ScheduleEventStates.WillOccur),
                ExpectedResult = true,
                EventLogFoundEventForRecordingResponse = true
            })
                .SetDescription("Check that original air date in future can't be cancelled")
                .SetName("TestCannotCancelFutureOriginalAirDate");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "A yellow family grows up in Springfield.",
                false,
                DateTime.MinValue,
                true,
                DateTime.MinValue,
                ScheduleEventStates.WillOccur),
                ExpectedResult = true,
                EventLogFoundEventForRecordingResponse = true
            })
                .SetDescription("Check that original air date is min value does not cancel recording")
                .SetName("TestCannotCancelOriginalAirDateAsMinValueMarkedAsRepeat");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "A yellow family grows up in Springfield.",
                false,
                DateTime.MinValue,
                false,
                DateTime.MinValue,
                ScheduleEventStates.WillOccur),
                ExpectedResult = true,
                EventLogFoundEventForRecordingResponse = true
            })
                .SetDescription("Check that original air date is min value does not cancel recording")
                .SetName("TestCannotCancelOriginalAirDateAsMinValueNotMarkedAsRepeat");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "A yellow family grows up in Springfield.",
                false,
                DateTime.Now.Date,
                true,
                DateTime.Now.Date,
                ScheduleEventStates.WillOccur),
                ExpectedResult = true,
                EventLogFoundEventForRecordingResponse = true
            })
                .SetDescription("Check that original air date is today and marked as repeat will not cancel")
                .SetName("TestCannotCancelOriginalAirDateAsTodayAndMarkedRepeat");

            yield return new TestCaseData(new ScheduledEventTestsCanEventBeCancelledTestDataUnit
            {
                ScheduledEvent = new MockScheduledEvent(
                "The Simpsons",
                "A yellow family grows up in Springfield.",
                false,
                DateTime.Now.Date,
                false,
                DateTime.Now.Date,
                ScheduleEventStates.WillOccur),
                ExpectedResult = true,
                EventLogFoundEventForRecordingResponse = true
            })
                .SetDescription("Check that original air date is today and not marked as repeat will not cancel")
                .SetName("TestCannotCancelOriginalAirDateAsTodayAndMarkedNotRepeat");
        }
    }
}