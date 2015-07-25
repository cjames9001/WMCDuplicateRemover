using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMCDuplicateRemover;
using WMCDuplicateRemover.Code.EPG;
using WMCDuplicateRemover.Code.Wrappers;

namespace WMCDuplicateRemover.Tests.EPG
{
    [TestFixture, Explicit]
    public class EPGIntegrationTests
    {
        private XmlTvEpgWrapper tv;

        [TestFixtureSetUp]
        public void SetUp()
        {
            tv = new XmlTvEpgWrapper(@"..\..\EPG\TestData\EPG.xml");
        }

        [TestCaseSource(typeof(EPGIntegrationTestCases))]
        public bool TestCanCancelScheduledEvent(ScheduledEvent scheduledEvent)
        {
            var episode = tv.GetEpisodeMetaDataBasedOnWMCMetaData(scheduledEvent.StartTime, scheduledEvent.EndTime, scheduledEvent.OriginalAirDate, Convert.ToInt32(scheduledEvent.ChannelID));
            return scheduledEvent.CanEventBeCancelled(new MicrosoftEventLogWrapper(), episode);
        }
    }
}
