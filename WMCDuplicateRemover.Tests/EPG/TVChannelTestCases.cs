using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover.Tests.EPG
{
    public class TVChannelTestCases : TestData
    {
        protected override IEnumerable CreateTestData()
        {
            yield return new TestCaseData(7)
                .Returns("I7.28455196.microsoft.com")
                .SetDescription("Checks channel 7")
                .SetName("TestGetChannel7Name");

            yield return new TestCaseData(611)
                .Returns("I611.28460660.microsoft.com")
                .SetDescription("Checks channel 611")
                .SetName("TestGetChannel611Name");

            yield return new TestCaseData(659)
                .Returns("I659.204092670.microsoft.com")
                .SetDescription("Checks channel 659")
                .SetName("TestGetChannel659Name");

            yield return new TestCaseData(660)
                .Returns("I660.182414702.microsoft.com")
                .SetDescription("Checks channel 660")
                .SetName("TestGetChannel660Name");

            yield return new TestCaseData(647)
                .Returns("I647.47337845.microsoft.com")
                .SetDescription("Checks channel 647")
                .SetName("TestGetChannel647Name");

            yield return new TestCaseData(626)
                .Returns("I626.219537223.microsoft.com")
                .SetDescription("Checks channel 626")
                .SetName("TestGetChannel626Name");
        }
    }
}
