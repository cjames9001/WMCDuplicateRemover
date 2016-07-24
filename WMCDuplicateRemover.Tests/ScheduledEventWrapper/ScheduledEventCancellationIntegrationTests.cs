using NUnit.Framework;
using System;
using System.Collections;

namespace WMCDuplicateRemover.Tests
{
    public class ScheduledEventCancellationIntegrationTests : TestData
    {
        protected override IEnumerable CreateTestData()
        {
            yield return new TestCaseData(new MockScheduledEvent(
                "The Simpsons",
                "Marge follows Bart around in an effort to get him to confess being involved in a bulldozer crash; Homer ignores Santa's Little Helper when Flanders brings a new dog home.",
                false,
                new DateTime(2015, 4, 19, 19, 0, 0),
                false,
                new DateTime(2015, 4, 19, 19, 0, 0),
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur))
                .Returns(false)
                .SetDescription("Check that repeat can't be cancelled")
                .SetName("TestCannotCancelNonRepeat");

            yield return new TestCaseData(new MockScheduledEvent(
                "The Simpsons",
                "Days of Future Future",
                false,
                new DateTime(2014, 4, 13, 21, 0, 0),
                true,
                new DateTime(2016, 2, 10, 21, 0, 0),
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur))
                .Returns(true)
                .SetDescription("Check that repeat can be cancelled")
                .SetName("TestCanCancelRepeat");

            yield return new TestCaseData(new MockScheduledEvent(
                "The Simpsons",
                "A yellow family grows up in Springfield.",
                false,
                new DateTime(9999, 4, 24, 9, 0, 0),
                true,
                new DateTime(9999, 4, 24, 9, 0, 0),
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur))
                .Returns(false)
                .SetDescription("Check that original air date in future can't be cancelled")
                .SetName("TestCannotCancelFutureOriginalAirDate");

            yield return new TestCaseData(new MockScheduledEvent(
                "Community",
                "Dean Pelton asks the study group to clean and refurbish the Greendale flight simulator. After an accidental launch, Abed must navigate a safe return.",
                false,
                new DateTime(2010, 10, 14),
                true,
                new DateTime(2015, 5, 1),
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur))
                .Returns(true)
                .SetDescription("Check that Community episode can be cancelled")
                .SetName("TestCancelCommunityEpisodeRepeat");
            
            yield return new TestCaseData(new MockScheduledEvent(
                "King of the Hill",
                "When Hank forgets to mail his insurance payment, the coverage lapses for 36 hours, causing Hank and Bobby to go into a state of emergency to protect the house from any major disasters.  Meanwhile, Dale decides to raise bees, Bill and Boomhauer discover th",
                false,
                new DateTime(2005, 3, 6),
                true,
                new DateTime(2015, 4, 29),
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur))
                .Returns(true)
                .SetDescription("Check that repeat recording can be cancelled")
                .SetName("TestCancelKingOfThEhillEpisodeRepeat");

            yield return new TestCaseData(new MockScheduledEvent(
                "The Nightly Show With Larry Wilmore",
                "Larry Wilmore and his panel of guests give thier unique views on pop culture and current events.",
                false,
                new DateTime(2015, 4, 27, 22, 0, 0),
                false,
                new DateTime(2015, 4, 27, 22, 0, 0),
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur))
                .Returns(false)
                .SetDescription("Check that original air date is today and marked as repeat will not cancel")
                .SetName("TestCannotCancelNewShowMarkedAsNotRepeat");

            yield return new TestCaseData(new MockScheduledEvent(
                "Tosh.0",
                "A child who is not microwave-safe is redeemed; games rather than humor; making grieving for loved ones fun.",
                false,
                new DateTime(2015, 4, 28, 21, 0, 0),
                true,
                new DateTime(2015, 4, 28, 21, 0, 0),
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur))
                .Returns(false)
                .SetDescription("Check that original air date is today and not marked as repeat will not cancel")
                .SetName("TestCannotCancelNewShowMarkedAsRepeat");
        }
    }
}
