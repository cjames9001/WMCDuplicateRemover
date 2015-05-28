using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMCDuplicateRemover;
using WMCDuplicateRemover.Code.EPG;

namespace WMCDuplicateRemover.Tests.EPG
{
    [TestFixture, Explicit]
    public class EPGIntegrationTests
    {
        private TV tv;

        [TestFixtureSetUp]
        public void SetUp()
        {
            tv = new TV(@"..\..\EPG\TestData\EPG.xml");
        }

        [TestCaseSource(typeof(EPGIntegrationTestCases))]
        public bool TestCanCancelScheduledEvent(ScheduledEvent scheduledEvent)
        {
            var episode = tv.GetEpisodeMetaDataBasedOnWMCMetaData(scheduledEvent.StartTime, scheduledEvent.EndTime, scheduledEvent.OriginalAirDate, Convert.ToInt32(scheduledEvent.ChannelID));
            return scheduledEvent.CanEventBeCancelled(new MicrosoftEventLogWrapper(), episode);
        }
    }
}
