using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace WMCDuplicateRemover.Tests
{
    public class BuildGetSeriesUrlTests : TestMetaDataWrapper
    {
        protected override IEnumerable CreateTestData()
        {
            yield return new TestCaseData(new TheTVDBWrapper("The Simpsons", new DateTime(2015, 4, 26)))
                .Returns("http://thetvdb.com/api/GetSeries.php?seriesname=the simpsons")
                .SetDescription("Check that series name has a value")
                .SetName("TestBuildTheSimpsonsUrl");

            yield return new TestCaseData(new TheTVDBWrapper("Forensic Files", new DateTime(2015, 4, 24)))
                .Returns("http://thetvdb.com/api/GetSeries.php?seriesname=forensic files")
                .SetDescription("Check that series name has a value")
                .SetName("TestBuildForensicFilesUrl");

            yield return new TestCaseData(new TheTVDBWrapper("Last Week Tonight", new DateTime(2015, 8, 26)))
                .Returns("http://thetvdb.com/api/GetSeries.php?seriesname=last week tonight")
                .SetDescription("Check that series name has a value")
                .SetName("TestBuildLastWeekTonightUrl");

            yield return new TestCaseData(new TheTVDBWrapper("NHL Hockey", new DateTime(2015, 9, 26)))
                .Returns("http://thetvdb.com/api/GetSeries.php?seriesname=nhl hockey")
                .SetDescription("Check that series name has a value")
                .SetName("TestBuildLastWeekTonightUrl");
        }
    }
}
