using Moq;
using NUnit.Framework;
using System;
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
            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(x => x.Now()).Returns(new DateTime(2015, 04, 20));
            tv = new XmlTvEpgWrapper(@"..\..\EPG\TestData\EPG.xml", dateTimeMock.Object);
        }

        [TestCaseSource(typeof(EPGIntegrationTestCases))]
        public bool TestCanCancelScheduledEvent(ScheduledEvent scheduledEvent)
        {
            var episode = tv.GetEpisodeMetaDataBasedOnWMCMetaData(scheduledEvent.StartTime, scheduledEvent.EndTime, scheduledEvent.OriginalAirDate, Convert.ToInt32(scheduledEvent.ChannelId));
            return scheduledEvent.CanEventBeCancelled(new MicrosoftEventLogWrapper(), episode);
        }
    }
}
