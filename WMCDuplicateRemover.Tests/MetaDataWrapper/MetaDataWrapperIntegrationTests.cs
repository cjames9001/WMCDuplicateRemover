using NUnit.Framework;
using System;

namespace WMCDuplicateRemover.Tests
{
    [TestFixture, Explicit]
    public class MetaDataWrapperIntegrationTests
    {
        [TestCaseSource(typeof(GetEpisodeTitleFromOriginalAirDateIntegrationTests))]
        public String TestGetEpisodeTitleFromOriginalAirDate(MetaDataWrapper metaDataWrapper)
        {
            return metaDataWrapper.GetEpisodeTitleFromOriginalAirDate();
        }
    }
}
