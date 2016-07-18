using Moq;
using NUnit.Framework;
using System;
using WMCDuplicateRemover.Code.EPG;
using WMCDuplicateRemover.Code.Wrappers;

namespace WMCDuplicateRemover.Tests.EPG
{
    [TestFixture]
    public class TVChannelTests
    {
        private XmlTvEpgWrapper tv;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(x => x.Now()).Returns(new DateTime(2015, 04, 20));
            tv = new XmlTvEpgWrapper(@"..\..\EPG\TestData\EPG.xml", dateTimeMock.Object);
        }

        [TestCaseSource(typeof(TVChannelTestCases))]
        public String TestGetChannelStringFromNumber(int channelNumber)
        {
           return tv.GetEpgChannelFromNumber(channelNumber);
        }

        [TestCaseSource(typeof(TVEpisodeTestCases))]
        public Episode TestGetEpisodeMetaDataFromWMCMetaData(DateTime startTime, DateTime endTime, DateTime originalAirDate, int channel)
        {
            return tv.GetEpisodeMetaDataBasedOnWMCMetaData(startTime, endTime, originalAirDate, channel);
        }
    }
}
