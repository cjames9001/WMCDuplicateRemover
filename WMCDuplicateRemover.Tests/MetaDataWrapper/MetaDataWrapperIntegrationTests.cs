using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover.Tests
{
    [Ignore]
    [TestFixture]
    public class MetaDataWrapperIntegrationTests
    {
        [TestCaseSource(typeof(GetEpisodeTitleFromOriginalAirDateIntegrationTests))]
        public String TestGetEpisodeTitleFromOriginalAirDate(MetaDataWrapper metaDataWrapper)
        {
            return metaDataWrapper.GetEpisodeTitleFromOriginalAirDate();
        }
    }
}
