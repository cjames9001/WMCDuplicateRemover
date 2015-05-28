using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover.Tests.EPG
{
    public class EPGIntegrationTestCases : TestData
    {
        protected override IEnumerable CreateTestData()
        {
            yield return new TestCaseData(new MockScheduledEvent(
                "Forensic Files",
                "Investigating the worst arson spree in U.S. history -- a rash of more than 75 fires near Seattle.",
                false,
                new DateTime(2002,12, 09),
                false,
                new DateTime(2015, 05, 02, 02, 30, 00),
                new DateTime(2015, 05, 02, 03, 00, 00),
                626,
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur))
                .Returns(false)
                .SetDescription("Check that repeat can't be cancelled")
                .SetName("TestCannotCancelNonRepeat");

            yield return new TestCaseData(new MockScheduledEvent(
                "Forensic Files",
                "Sign of the Zodiac",
                false,
                new DateTime(2003, 12, 03),
                true,
                new DateTime(2015, 04, 30, 02, 30, 00),
                new DateTime(2015, 04, 30, 03, 00, 00),
                626,
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
                new DateTime(9999, 4, 24, 9, 0, 0),
                626,
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur))
                .Throws(typeof(NullReferenceException))
                .SetDescription("Check that original air date in future can't be cancelled")
                .SetName("TestCannotCancelFutureOriginalAirDate");

            yield return new TestCaseData(new MockScheduledEvent(
                "Community",
                "Dean Pelton asks the study group to clean and refurbish the Greendale flight simulator. After an accidental launch, Abed must navigate a safe return.",
                false,
                new DateTime(2010, 10, 14),
                true,
                new DateTime(2015, 05, 07, 00, 30, 00),
                new DateTime(2015, 05, 07, 01, 00, 00),
                653,
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur))
                .Returns(true)
                .SetDescription("Check that Community episode can be cancelled")
                .SetName("TestCancelCommunityEpisodeRepeat");

            yield return new TestCaseData(new MockScheduledEvent(
                "Forensic Files",
                "A determined forensic scientist pursues the robbery and murder of an elderly couple using material from a dentist's office.",
                false,
                new DateTime(2004, 08, 18),
                true,
                new DateTime(2015, 05, 01, 03, 30, 00),
                new DateTime(2015, 05, 01, 04, 00, 00),
                626,
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur))
                .Returns(true)
                .SetDescription("Check that Community episode can be cancelled")
                .SetName("TestCancelCommunityEpisodeRepeat");
            
            yield return new TestCaseData(new MockScheduledEvent(
                "King of the Hill",
                "When Hank forgets to mail his insurance payment, the coverage lapses for 36 hours, causing Hank and Bobby to go into a state of emergency to protect the house from any major disasters.  Meanwhile, Dale decides to raise bees, Bill and Boomhauer discover th",
                false,
                new DateTime(2003, 12, 14),
                true,
                new DateTime(2015, 04, 30, 19, 30, 00),
                new DateTime(2015, 04, 30, 20, 00, 00),
                622,
                Microsoft.MediaCenter.TV.Scheduling.ScheduleEventStates.WillOccur))
                .Returns(true)
                .SetDescription("Check that repeat recording can be cancelled")
                .SetName("TestCancelKingOfThEhillEpisodeRepeat");
        }
    }
}
