using System;

namespace WMCDuplicateRemover.Tests
{
    public class GetEpisodeTitleFromOriginalAirDateIntegrationTests : GetEpisodeTitleFromOriginalAirDateTests
    {
        protected override MetaDataWrapper CreateMetaDataWrapper(String seriesName, DateTime originalAirDate)
        {
            var metaDataWrapper = new TheTVDBWrapper(seriesName, originalAirDate);
            return metaDataWrapper;
        }
    }
}
