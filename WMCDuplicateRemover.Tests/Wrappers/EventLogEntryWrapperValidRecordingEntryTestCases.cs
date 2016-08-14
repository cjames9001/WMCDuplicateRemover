using NUnit.Framework;
using System.Collections;

namespace WMCDuplicateRemover.Tests.Wrappers
{
    public class EventLogEntryWrapperValidRecordingEntryTestCases : TestData
    {
        private const string WMC_RECORDING_SOURCE = "Recording";
        protected override IEnumerable CreateTestData()
        {
            yield return new TestCaseData(new EventLogEntryWrapper(
                "The Daily Show With Jon Stewart started recording on 5/26/2015 9:55:01 PM and stopped on 5/26/2015 10:31:00 PM as scheduled.", 
                WMC_RECORDING_SOURCE, 1))
                .Returns(false)
                .SetDescription("Checks an event type 1 recording without an episode name")
                .SetName("Test1WithoutEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("While recording NHL Hockey: Tampa Bay Lightning at Detroit Red Wings, the computer lost power or experienced a temporary failure.", 
                WMC_RECORDING_SOURCE, 4))
                .Returns(false)
                .SetDescription("Checks an event type 4 recording with an episode name")
                .SetName("Test4WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Community was not recorded due to a conflict with It's Always Sunny in Philadelphia: The Gang Finds a Dead Guy.", 
                WMC_RECORDING_SOURCE, 6))
                .Returns(false)
                .SetDescription("Checks an event type 6 recording with an episode name")
                .SetName("Test6WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("The Simpsons: Treehouse of Horror XX was not recorded due to a temporary failure caused by either a system malfunction or a power loss.", 
                WMC_RECORDING_SOURCE, 7))
                .Returns(false)
                .SetDescription("Checks an event type 7 recording with an episode name")
                .SetName("Test7WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("NHL Hockey: Pittsburgh Penguins at New York Rangers was not recorded. There was no TV signal when the show was scheduled to record.", 
                WMC_RECORDING_SOURCE, 8))
                .Returns(false)
                .SetDescription("Checks an event type 8 recording with an episode name")
                .SetName("Test8WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of Fixer Upper began as scheduled on 3/14/2015 12:52:47 PM while the program was already in progress.", 
                WMC_RECORDING_SOURCE, 2))
                .Returns(false)
                .SetDescription("Checks an event type 2 without episode name")
                .SetName("Test2WithoutEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("The series recording of Raising Hope was removed from the recording schedule on 6/22/2014 9:58:50 AM by Corey.", 
                WMC_RECORDING_SOURCE, 21))
                .Returns(false)
                .SetDescription("Checks an event type 21 with episode name")
                .SetName("Test21WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of Parks and Recreation: The Pawnee-Eagleton Tip off Classic began as scheduled on 5/11/2015 10:57:34 AM while the program was already in progress.",
                WMC_RECORDING_SOURCE, 2))
                .Returns(false)
                .SetDescription("Checks an event type 2 with episode name")
                .SetName("Test2WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of Futurama: The Sting began as scheduled on 12/11/2013 3:37:58 AM while the program was already in progress.",
                WMC_RECORDING_SOURCE, 2))
                .Returns(false)
                .SetDescription("Checks an event type 2 with episode name")
                .SetName("Test2WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper(@"An unauthorized window was detected while running Windows Media Center, 'TeamViewer', with file name 'C:\Program Files (x86)\TeamViewer\Version9\TeamViewer.exe'.",
                "Media Center Extender", 107))
                .Returns(false)
                .SetDescription("Checks an event type 107 with episode name")
                .SetName("Test107WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper(@"An unauthorized window was detected while running Windows Media Center with file name 'C:\Program Files\Microsoft Mouse and Keyboard Center\ipoint.exe', could not retrieve the window title.",
                WMC_RECORDING_SOURCE, 109))
                .Returns(false)
                .SetDescription("Checks an event type 3 with episode name")
                .SetName("Test3WithoutEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of The Simpsons: Saddlesore Galactica began as scheduled on 4/13/2015 9:55:04 PM and was manually stopped on 4/13/2015 10:17:54 PM.", 
                "Media Center Extender", 3))
                .Returns(false)
                .SetDescription("Checks an event type 3 with episode name")
                .SetName("Test3WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Recording of The Simpsons: Wild Barts Can't Be Broken began as scheduled on 4/13/2015 9:25:01 PM and was manually stopped on 4/13/2015 9:53:38 PM.",
                WMC_RECORDING_SOURCE, 3))
                .Returns(false)
                .SetDescription("Checks an event type 3 with episode name")
                .SetName("Test3WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Media Center Extender Setup started with the following settings:\nAffiliation code: 576\nUsername: Corey",
                WMC_RECORDING_SOURCE, 301))
                .Returns(false)
                .SetDescription("Checks an event type 3 with episode name and is late")
                .SetName("Test3WithEpisodeLate");

            yield return new TestCaseData(new EventLogEntryWrapper("Forensic Files: Sunday's Wake was manually deleted on 5/27/2015 6:20:41 PM by Corey.", 
                WMC_RECORDING_SOURCE, 17))
                .Returns(false)
                .SetDescription("Checks an event type 17 with episode name")
                .SetName("Test17WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("The Simpsons was manually deleted on 3/1/2015 6:53:55 PM by Corey.", 
                WMC_RECORDING_SOURCE, 17))
                .Returns(false)
                .SetDescription("Checks an event type 17 without episode name")
                .SetName("Test17WithoutEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("Forensic Files: Breaking the Mold", 
                WMC_RECORDING_SOURCE, 24))
                .Returns(true)
                .SetDescription("Checks an event type 24 with episode name")
                .SetName("Test24WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("The Simpsons: Marge Be Not Proud", 
                WMC_RECORDING_SOURCE, 24))
                .Returns(true)
                .SetDescription("Checks an event type 24 with episode name")
                .SetName("Test24WithEpisode");

            yield return new TestCaseData(new EventLogEntryWrapper("The Simpsons", 
                WMC_RECORDING_SOURCE, 24))
                .Returns(false)
                .SetDescription("Checks an event type 24 with episode name")
                .SetName("Test24WithoutEpisode");
        }
    }
}
