using NUnit.Framework;
using System;
using System.Collections;

namespace WMCDuplicateRemover.Tests.Wrappers
{
    public class EventLogEntryWrapperRecordingNameTestCases : TestData
    {
        private const String WMC_RECORDING_SOURCE = "Recording";
        protected override IEnumerable CreateTestData()
        {
            yield return new TestCaseData(new EventLogEntryWrapper(
                "The Daily Show With Jon Stewart started recording on 5/26/2015 9:55:01 PM and stopped on 5/26/2015 10:31:00 PM as scheduled.", 
                WMC_RECORDING_SOURCE, 1))
                .Returns("The Daily Show With Jon Stewart")
                .SetDescription("Checks an event type 1 recording without an episode name")
                .SetName("Test1WithoutEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Two and a Half Men: A Live Woman of Proven Fertility started recording on 9/27/2014 9:00:03 AM and stopped on 9/27/2014 9:35:01 AM as scheduled.", 
                WMC_RECORDING_SOURCE, 1))
                .Returns("Two and a Half Men: A Live Woman of Proven Fertility")
                .SetDescription("Checks an event type 1 recording with an episode name")
                .SetName("Test1WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Wahlburgers: Who's Your Favorite? started recording on 2/28/2015 3:55:03 PM and stopped on 2/28/2015 4:35:00 PM as scheduled.", 
                WMC_RECORDING_SOURCE, 1))
                .Returns("Wahlburgers: Who's Your Favorite?")
                .SetDescription("Checks an event type 1 recording with an episode name")
                .SetName("Test1WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("South Park: 4th Grade started recording on 3/1/2015 3:34:01 PM and stopped on 3/1/2015 4:16:00 PM as scheduled.", 
                WMC_RECORDING_SOURCE, 1))
                .Returns("South Park: 4th Grade")
                .SetDescription("Checks an event type 1 recording with an episode name")
                .SetName("Test1WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("The Simpsons: Opposites A-Frack started recording on 11/2/2014 6:55:04 PM and stopped on 11/2/2014 7:35:00 PM as scheduled.", 
                WMC_RECORDING_SOURCE, 1))
                .Returns("The Simpsons: Opposites A-Frack")
                .SetDescription("Checks an event type 1 recording with an episode name")
                .SetName("Test1WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of Fixer Upper began as scheduled on 3/14/2015 12:52:47 PM while the program was already in progress.", 
                WMC_RECORDING_SOURCE, 2))
                .Returns("Fixer Upper")
                .SetDescription("Checks an event type 2 without episode name")
                .SetName("Test2WithoutEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of Fixer Upper: Searching for a New Homebuilder's Dream Home in an Old Neighborhood began as scheduled on 3/14/2015 12:52:47 PM while the program was already in progress.", 
                WMC_RECORDING_SOURCE, 2))
                .Returns("Fixer Upper: Searching for a New Homebuilder's Dream Home in an Old Neighborhood")
                .SetDescription("Checks an event type 2 with episode name")
                .SetName("Test2WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of Parks and Recreation: The Pawnee-Eagleton Tip off Classic began as scheduled on 5/11/2015 10:57:34 AM while the program was already in progress.",
                WMC_RECORDING_SOURCE, 2))
                .Returns("Parks and Recreation: The Pawnee-Eagleton Tip off Classic")
                .SetDescription("Checks an event type 2 with episode name")
                .SetName("Test2WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of Futurama: The Sting began as scheduled on 12/11/2013 3:37:58 AM while the program was already in progress.",
                WMC_RECORDING_SOURCE, 2))
                .Returns("Futurama: The Sting")
                .SetDescription("Checks an event type 2 with episode name")
                .SetName("Test2WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of The Voice: The Battles Continue began as scheduled on 3/24/2014 7:20:05 PM while the program was already in progress.",
                WMC_RECORDING_SOURCE, 2))
                .Returns("The Voice: The Battles Continue")
                .SetDescription("Checks an event type 2 with episode name")
                .SetName("Test2WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of The Simpsons began as scheduled on 4/13/2015 9:55:04 PM and was manually stopped on 4/13/2015 10:17:54 PM.",
                WMC_RECORDING_SOURCE, 3))
                .Returns("The Simpsons")
                .SetDescription("Checks an event type 3 with episode name")
                .SetName("Test3WithoutEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of The Simpsons: Saddlesore Galactica began as scheduled on 4/13/2015 9:55:04 PM and was manually stopped on 4/13/2015 10:17:54 PM.", 
                WMC_RECORDING_SOURCE, 3))
                .Returns("The Simpsons: Saddlesore Galactica")
                .SetDescription("Checks an event type 3 with episode name")
                .SetName("Test3WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of The Simpsons: Wild Barts Can't Be Broken began as scheduled on 4/13/2015 9:25:01 PM and was manually stopped on 4/13/2015 9:53:38 PM.",
                WMC_RECORDING_SOURCE, 3))
                .Returns("The Simpsons: Wild Barts Can't Be Broken")
                .SetDescription("Checks an event type 3 with episode name")
                .SetName("Test3WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of Silicon Valley: Fiduciary Duties began late on 4/11/2015 4:58:24 PM due to a temporary failure caused by either a system malfunction or a power loss and stopped on 4/11/2015 5:00:54 PM.",
                WMC_RECORDING_SOURCE, 3))
                .Returns("Silicon Valley: Fiduciary Duties began late on 4/11/2015 4:58:24 PM due to a temporary failure caused by either a system malfunction or a power loss and stopped on 4/11/2015 5:00:54 PM.")
                .SetDescription("Checks an event type 3 with episode name and is late")
                .SetName("Test3WithEpisodeLate");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of Person of Interest: The Perfect Mark began as scheduled on 4/28/2014 8:55:04 PM and was manually stopped on 4/28/2014 9:25:23 PM.",
                WMC_RECORDING_SOURCE, 3))
                .Returns("Person of Interest: The Perfect Mark")
                .SetDescription("Checks an event type 3 with episode name")
                .SetName("Test3WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Forensic Files: Sunday's Wake was manually deleted on 5/27/2015 6:20:41 PM by Corey.", 
                WMC_RECORDING_SOURCE, 17))
                .Returns("Forensic Files: Sunday's Wake")
                .SetDescription("Checks an event type 17 with episode name")
                .SetName("Test17WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Forensic Files: Family Ties (The Christoper Porco Case) was manually deleted on 5/26/2015 7:07:46 PM by Corey.", 
                WMC_RECORDING_SOURCE, 17))
                .Returns("Forensic Files: Family Ties (The Christoper Porco Case)")
                .SetDescription("Checks an event type 17 with episode name")
                .SetName("Test17WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Beach Volleyball: AVP Pro Tour was manually deleted on 5/27/2015 5:28:34 PM by Corey.", 
                WMC_RECORDING_SOURCE, 17))
                .Returns("Beach Volleyball: AVP Pro Tour")
                .SetDescription("Checks an event type 17 with episode name")
                .SetName("Test17WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("The Simpsons: Dumbbell Indemnity was manually deleted on 3/1/2015 6:53:55 PM by Corey.", 
                WMC_RECORDING_SOURCE, 17))
                .Returns("The Simpsons: Dumbbell Indemnity")
                .SetDescription("Checks an event type 17 with episode name")
                .SetName("Test17WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("The Simpsons was manually deleted on 3/1/2015 6:53:55 PM by Corey.", 
                WMC_RECORDING_SOURCE, 17))
                .Returns("The Simpsons")
                .SetDescription("Checks an event type 17 without episode name")
                .SetName("Test17WithoutEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("NHL Hockey: Chicago Blackhawks at Minnesota Wild", 
                WMC_RECORDING_SOURCE, 24))
                .Returns("NHL Hockey: Chicago Blackhawks at Minnesota Wild")
                .SetDescription("Checks an event type 24 with episode name")
                .SetName("Test24WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Forensic Files: Breaking the Mold", 
                WMC_RECORDING_SOURCE, 24))
                .Returns("Forensic Files: Breaking the Mold")
                .SetDescription("Checks an event type 24 with episode name")
                .SetName("Test24WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("The Simpsons: When Flanders Failed", 
                WMC_RECORDING_SOURCE, 24))
                .Returns("The Simpsons: When Flanders Failed")
                .SetDescription("Checks an event type 24 with episode name")
                .SetName("Test24WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Two and a Half Men: A Bag Full of Jawea", 
                WMC_RECORDING_SOURCE, 24))
                .Returns("Two and a Half Men: A Bag Full of Jawea")
                .SetDescription("Checks an event type 24 with episode name")
                .SetName("Test24WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("The Simpsons: Marge Be Not Proud", 
                WMC_RECORDING_SOURCE, 24))
                .Returns("The Simpsons: Marge Be Not Proud")
                .SetDescription("Checks an event type 24 with episode name")
                .SetName("Test24WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("The Simpsons", 
                WMC_RECORDING_SOURCE, 24))
                .Returns("The Simpsons")
                .SetDescription("Checks an event type 24 with episode name")
                .SetName("Test24WithoutEpisode");
        }
    }
}
