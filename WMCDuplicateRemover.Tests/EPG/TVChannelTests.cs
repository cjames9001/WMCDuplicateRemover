using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            tv = new XmlTvEpgWrapper(@"..\..\EPG\TestData\EPG.xml");
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
