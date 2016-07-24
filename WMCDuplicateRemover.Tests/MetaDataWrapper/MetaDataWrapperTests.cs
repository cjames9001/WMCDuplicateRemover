using System;

using NUnit.Framework;

namespace WMCDuplicateRemover.Tests
{
    [TestFixture]
    public class MetaDataWrapperTests
    {
        [TestFixtureSetUp]
        public void SetupFixture()
        {
            SetupHelper.SetupXMLData();
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
            SetupHelper.TearDownXMLData();
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
