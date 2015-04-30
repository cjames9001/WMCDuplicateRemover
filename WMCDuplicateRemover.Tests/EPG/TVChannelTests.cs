using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMCDuplicateRemover.Code.EPG;

namespace WMCDuplicateRemover.Tests.EPG
{
    [TestFixture]
    public class TVChannelTests
    {
        private TV tv;

        [TestFixtureSetUp]
        public void SetUp()
        {
            tv = new TV(@"..\..\EPG\TestData\EPG.xml");
        }

        [TestCaseSource(typeof(TVChannelTestCases))]
        public String TestGetChannelStringFromNumber(int channelNumber)
        {
           return tv.GetChannelFromNumber(channelNumber);
        }

        [TestCaseSource(typeof(TVEpisodeTestCases))]
        public Episode TestGetEpisodeMetaDataFromWMCMetaData(DateTime startTime, DateTime endTime, DateTime originalAirDate, int channel)
        {
            return tv.GetEpisodeMetaDataBasedOnWMCMetaData(startTime, endTime, originalAirDate, channel);
        }
    }
}
