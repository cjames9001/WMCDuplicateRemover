using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace WMCDuplicateRemover.Tests
{
    [TestFixture]
    public class MetaDataWrapperTests
    {
        /*
        [TestCaseSource(typeof(TestMetaDataWrapperCreateAPIQuery))]
        public MetaDataWrapper FormatStringTests(MetaDataWrapper metaData)
        {
            return metaData;
        }
        */
        [Test]
        [ExpectedException(typeof(NotImplementedException), ExpectedMessage = "The MetaDataWrapper class cannot be used without a series name and an original air date.")]
        public void TestEmptyConstructorThrowsException()
        {
            new TheTVDBWrapper();
        }

        [Test]
        public void TestSameParametersCreatesEqualObjects()
        {
            Assert.AreEqual(new TheTVDBWrapper("The Simpsons", new DateTime(2015, 4, 26)), new TheTVDBWrapper("The Simpsons", new DateTime(2015, 4, 26)), "The created objects are not the same");
        }

        [Test]
        public void TestDifferentNamesCreateDifferentObjects()
        {
            Assert.AreNotEqual(new TheTVDBWrapper("The Simpson", new DateTime(2015, 4, 26)), new TheTVDBWrapper("The Simpsons", new DateTime(2015, 4, 26)), "The created objects are not the same");
        }

        [Test]
        public void TestDifferentDatesCreateDifferentObjects()
        {
            Assert.AreNotEqual(new TheTVDBWrapper("The Simpsons", new DateTime(2015, 4, 2)), new TheTVDBWrapper("The Simpsons", new DateTime(2015, 4, 26)), "The created objects are not the same");
        }

        [Test]
        public void TestSameNameWithCaseDifferenceCreatesSameObjects()
        {
            Assert.AreEqual(new TheTVDBWrapper("The simpsons", new DateTime(2015, 4, 26)), new TheTVDBWrapper("The Simpsons", new DateTime(2015, 4, 26)), "The created objects are not the same");
        }

        [TestCaseSource(typeof(BuildGetSeriesUrlTests))]
        public String TestBuildSeriesURL(TheTVDBWrapper metaDataWrapper)
        {
            return metaDataWrapper.BuildSeriesURL();
        }

        [TestCaseSource(typeof(BuildGetEpisodeByAirDateUrlTests))]
        public String TestBuildGetEpisodeByAirDateUrl(TheTVDBWrapper metaDataWrapper)
        {
            return metaDataWrapper.BuildGetEpisodeByAirDateUrl();
        }
    }
}
