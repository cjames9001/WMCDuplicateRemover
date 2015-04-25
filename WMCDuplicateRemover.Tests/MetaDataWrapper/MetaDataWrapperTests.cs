using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using System.IO;

namespace WMCDuplicateRemover.Tests
{
    [TestFixture]
    public class MetaDataWrapperTests
    {
        [TestFixtureSetUp]
        public void SetupFixture()
        {
            File.WriteAllText("KingOfTheHill.xml", "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Data><Series><seriesid>73903</seriesid><language>en</language><SeriesName>King of the Hill</SeriesName><banner>graphical/73903-g2.jpg</banner><Overview>Set in Texas, this animated series follows the life of propane salesman Hank Hill, who lives with his substitute teacher wife Peggy, wannabe comedian son Bobby, and naive niece Luanne. Hank has strikingly stereotypical views about God and country, and is refreshingly honest about the way he sees the world. </Overview><FirstAired>1997-01-12</FirstAired><Network>FOX (US)</Network><IMDB_ID>tt0118375</IMDB_ID><zap2it_id>EP00207002</zap2it_id><id>73903</id></Series></Data>");
            File.WriteAllText("TheNightlyShow.xml", "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Data><Series><seriesid>286373</seriesid><language>en</language><SeriesName>The Nightly Show with Larry Wilmore</SeriesName><banner>graphical/286373-g.jpg</banner><Overview>The Nightly Show with Larry Wilmore is an American late-night panel talk show hosted by Larry Wilmore. The Nightly Show with Larry Wilmore is a spin-off of The Daily Show, which featured Wilmore as a recurring contributor billed as the \"Senior Black Correspondent\". It premiered on January 19, 2015 on Comedy Central, and airs Mondays through Thursdays at 11:30 PM (EST) following The Daily Show. It serves as a replacement for The Colbert Report, which aired in the same time-slot from October 2005 until December 2014. The show has been described as a combination of The Daily Show and Politically Incorrect. Each episode begins with Wilmore's scripted take on the news followed by a panel discussion with his guests.</Overview><FirstAired>2015-01-19</FirstAired><Network>Comedy Central</Network><IMDB_ID>tt3722332</IMDB_ID><zap2it_id>EP01981966</zap2it_id><id>286373</id></Series></Data>");
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.Delete("KingOfTheHill.xml");
            File.Delete("TheNightlyShow.xml");
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException), ExpectedMessage = "The MetaDataWrapper class cannot be used without a series name and an original air date.")]
        public void TestEmptyConstructorThrowsException()
        {
            new MockMetaDataWrapper();
        }

        [Test]
        public void TestSameParametersCreatesEqualObjects()
        {
            Assert.AreEqual(new MockMetaDataWrapper("The Simpsons", new DateTime(2015, 4, 26)), new MockMetaDataWrapper("The Simpsons", new DateTime(2015, 4, 26)), "The created objects are not the same");
        }

        [Test]
        public void TestDifferentNamesCreateDifferentObjects()
        {
            Assert.AreNotEqual(new MockMetaDataWrapper("The Simpson", new DateTime(2015, 4, 26)), new MockMetaDataWrapper("The Simpsons", new DateTime(2015, 4, 26)), "The created objects are not the same");
        }

        [Test]
        public void TestDifferentDatesCreateDifferentObjects()
        {
            Assert.AreNotEqual(new MockMetaDataWrapper("The Simpsons", new DateTime(2015, 4, 2)), new MockMetaDataWrapper("The Simpsons", new DateTime(2015, 4, 26)), "The created objects are not the same");
        }

        [Test]
        public void TestSameNameWithCaseDifferenceCreatesSameObjects()
        {
            Assert.AreEqual(new MockMetaDataWrapper("The simpsons", new DateTime(2015, 4, 26)), new MockMetaDataWrapper("The Simpsons", new DateTime(2015, 4, 26)), "The created objects are not the same");
        }

        [TestCaseSource(typeof(BuildGetSeriesUrlTests))]
        public String TestBuildSeriesURL(MetaDataWrapper metaDataWrapper)
        {
            return metaDataWrapper.BuildSeriesURL();
        }

        [TestCaseSource(typeof(BuildGetEpisodeByAirDateUrlTests))]
        public String TestBuildGetEpisodeByAirDateUrl(MetaDataWrapper metaDataWrapper)
        {
            return metaDataWrapper.BuildGetEpisodeByAirDateUrl();
        }

        [TestCaseSource(typeof(GetEpisodeTitleFromOriginalAirDateTests))]
        public String TestGetEpisodeTitleFromOriginalAirDate(MetaDataWrapper metaDataWrapper)
        {
            return metaDataWrapper.GetEpisodeTitleFromOriginalAirDate();
        }
    }
}
