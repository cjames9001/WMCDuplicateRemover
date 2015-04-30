using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMCDuplicateRemover.Code.EPG;

namespace WMCDuplicateRemover.Tests.EPG
{
    public class TVEpisodeTestCases : TestData
    {
        protected override IEnumerable CreateTestData()
        {
            yield return new TestCaseData(new DateTime(2015, 4, 29, 21, 0, 0), new DateTime(2015, 4, 29, 21, 30, 0), new DateTime(2002, 10, 03), 626)
                .Returns(new Episode()
                    {
                        ChannelID = "I626.219537223.microsoft.com",
                        StartString = "20150429210000 -0500",
                        EndString = "20150429213000 -0500",
                        SeriesName = "Forensic Files",
                        EpisodeName = "Palm Print Conviction",
                        Description = "A woman who leaves a bar with a stranger is found dead the next morning.",
                        OriginalAirDateString = "20021003"
                    })
                .SetDescription("Checks 9PM Showing of forensic files")
                .SetName("TestGetForensicFilesEpisode");

            yield return new TestCaseData(new DateTime(2015, 4, 29, 22, 0, 0), new DateTime(2015, 4, 29, 22, 31, 0), new DateTime(2015, 04, 29), 659)
                .Returns(new Episode()
                {
                    ChannelID = "I659.204092670.microsoft.com",
                    StartString = "20150429220000 -0500",
                    EndString = "20150429223100 -0500",
                    SeriesName = "The Daily Show With Jon Stewart",
                    EpisodeName = "Judith Miller",
                    Description = "Author Judith Miller.",
                    OriginalAirDateString = "20150429"
                })
                .SetDescription("Checks 10PM Episode of the daily show")
                .SetName("TestGetTheDailyShowEpisode");

            yield return new TestCaseData(new DateTime(2015, 5, 1, 15, 0, 0), new DateTime(2015, 5, 1, 15, 30, 0), new DateTime(2005, 03, 21), 631)
                .Returns(new Episode()
                {
                    ChannelID = "I631.182414698.microsoft.com",
                    StartString = "20150501150000 -0500",
                    EndString = "20150501153000 -0500",
                    SeriesName = "Two and a Half Men",
                    EpisodeName = "A Low, Gutteral Tongue-Flapping Noise",
                    Description = "Alan asks one of Charlie's beautiful ex-girlfriends (Jeri Ryan) on a date, but while they are out he is tormented by thoughts of her with Charlie.",
                    OriginalAirDateString = "20050321"
                })
                .SetDescription("Checks 3PM Episode of two and a half men")
                .SetName("TestGetTwoAndAHalfMenEpisode");

            yield return new TestCaseData(new DateTime(2015, 5, 1, 15, 30, 0), new DateTime(2015, 5, 1, 16, 0, 0), new DateTime(2005, 03, 20), 631)
                .Throws(typeof(NullReferenceException))
                .SetDescription("Checks null reference exception is thrown when there isn't anything as listed")
                .SetName("TestCatchNullReferenceException");

            yield return new TestCaseData(new DateTime(2015, 05, 03, 03, 57, 00), new DateTime(2015, 05, 03, 04, 30, 00), new DateTime(2003, 11, 28), 643)
                .Throws(typeof(InvalidOperationException))
                .SetDescription("Checks 3PM Episode of two and a half men")
                .SetName("TestGetTwoAndAHalfMenEpisode");
        }
    }
}
