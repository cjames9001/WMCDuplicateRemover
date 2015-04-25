using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover.Tests
{
    public class BuildGetEpisodeByAirDateUrlTests : TestMetaDataWrapper
    {
        private MetaDataWrapper CreateMetaDataWrapper(String seriesName, DateTime originalAirDate)
        {
            var metaDataWrapper = new MockMetaDataWrapper(seriesName, originalAirDate);
            metaDataWrapper.seriesIdCache = new Dictionary<string, string>() { { "the simpsons", "71663" }, { "forensic files", "71415" } };
            return metaDataWrapper;
        }

        protected override IEnumerable CreateTestData()
        {
            yield return new TestCaseData(CreateMetaDataWrapper("The Simpsons", new DateTime(2015, 4, 19)))
                .Returns("http://thetvdb.com/api/GetEpisodeByAirDate.php?apikey=B568191E5807039C&seriesid=71663&airdate=4/19/2015")
                .SetDescription("Check that series id and original air date has value")
                .SetName("TestBuildTheSimpsonsUrlFromCache");

            yield return new TestCaseData(CreateMetaDataWrapper("Forensic Files", new DateTime(2015, 5, 19)))
                .Returns("http://thetvdb.com/api/GetEpisodeByAirDate.php?apikey=B568191E5807039C&seriesid=71415&airdate=5/19/2015")
                .SetDescription("Check that series id and original air date has value")
                .SetName("TestBuildForensicFilesUrlFromCache");

            yield return new TestCaseData(CreateMetaDataWrapper("Last Week Tonight", new DateTime(2015, 5, 17)))
                .Throws(typeof(NullReferenceException))
                .SetDescription("Check that series id and original air date has value")
                .SetName("TestBuildLastWeekTonightUrlThrowsException");

            yield return new TestCaseData(CreateMetaDataWrapper("The Nightly Show with Larry Wilmore", new DateTime(2015, 5, 21)))
                .Returns("http://thetvdb.com/api/GetEpisodeByAirDate.php?apikey=B568191E5807039C&seriesid=286373&airdate=5/21/2015")
                .SetDescription("Check that series id and original air date has value")
                .SetName("TestBuildTheNightlyShowwithLarryWilmoreUrlFromApi");

            yield return new TestCaseData(CreateMetaDataWrapper("King of The Hill", new DateTime(2015, 7, 21)))
                .Returns("http://thetvdb.com/api/GetEpisodeByAirDate.php?apikey=B568191E5807039C&seriesid=73903&airdate=7/21/2015")
                .SetDescription("Check that series id and original air date has value")
                .SetName("TestBuildKingOfTheHillUrlFromApi");
        }
    }
}
