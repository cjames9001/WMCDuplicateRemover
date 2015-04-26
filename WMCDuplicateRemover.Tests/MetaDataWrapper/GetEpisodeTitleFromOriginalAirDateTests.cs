using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover.Tests
{
    public class GetEpisodeTitleFromOriginalAirDateTests : TestMetaDataWrapper
    {
        protected virtual MetaDataWrapper CreateMetaDataWrapper(String seriesName, DateTime originalAirDate)
        {
            var metaDataWrapper = new MockMetaDataWrapper(seriesName, originalAirDate);
            metaDataWrapper.seriesIdCache = new Dictionary<string, string>() { { "the simpsons", "71663" }, { "forensic files", "71415" }, { "last week tonight", "278518" }, { "blahblahblah", "4568" } };
            return metaDataWrapper;
        }

        protected override IEnumerable CreateTestData()
        {
            yield return new TestCaseData(CreateMetaDataWrapper("The Simpsons", new DateTime(2015, 4, 19)))
                .Returns("Peeping Mom")
                .SetDescription("Check that series id and original air date has value")
                .SetName("TestGetTitleForSimpsonsEpisode");
            
            yield return new TestCaseData(CreateMetaDataWrapper("Forensic Files", new DateTime(1996, 04, 21)))
                .Returns("The Disappearance of Helle Crafts")
                .SetDescription("Check that series id and original air date has value")
                .SetName("TestGetTitleForForensicFielsEpisode");

            yield return new TestCaseData(CreateMetaDataWrapper("Last Week Tonight", new DateTime(2015, 4, 19)))
                .Returns("Patents")
                .SetDescription("Check that series id and original air date has value")
                .SetName("TestGetTitleForLastWeekTonightEpisode");

            yield return new TestCaseData(CreateMetaDataWrapper("The Nightly Show with Larry Wilmore", new DateTime(2015, 4, 21)))
                .Returns("Walmart Closures & A Cop's Non-Lethal Force")
                .SetDescription("Check that series id and original air date has value")
                .SetName("TestGetTheTitleForTheNightlyShowwithLarryWilmoreEpisode");

            yield return new TestCaseData(CreateMetaDataWrapper("King of The Hill", new DateTime(2001, 11, 11)))
                .Returns("Bobby Goes Nuts")
                .SetDescription("Check that series id and original air date has value")
                .SetName("TestGetTitleForKingOfTheHillEpisode");

            yield return new TestCaseData(CreateMetaDataWrapper("BlahBlahBlah", new DateTime(2015, 7, 21)))
                .Throws(typeof(NullReferenceException))
                .SetDescription("Check that series id and original air date has value")
                .SetName("TestFakeShowThrowsNullReferenceException");
        }
    }
}
