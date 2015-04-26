﻿using System;
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
            File.WriteAllText("BobbyGoesNuts.xml", "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> <Data> <Episode> <id>131946</id> <Combined_episodenumber>1</Combined_episodenumber> <Combined_season>6</Combined_season> <DVD_chapter></DVD_chapter> <DVD_discid></DVD_discid> <DVD_episodenumber></DVD_episodenumber> <DVD_season></DVD_season> <Director>Tricia Garcia</Director> <EpImgFlag></EpImgFlag> <EpisodeName>Bobby Goes Nuts</EpisodeName> <EpisodeNumber>1</EpisodeNumber> <FirstAired>2001-11-11</FirstAired> <GuestStars></GuestStars> <IMDB_ID></IMDB_ID> <Language>en</Language> <Overview>The episode opens innocently enough with Bobby's girlfriend Connie inviting him over to add some spark to her dying slumber party. But after Bobby's beaten up by a crasher, Hank urges him to enroll in a boxing class at the Y. That class is full, so Bobby </Overview> <ProductionCode>5ABE24</ProductionCode> <Rating></Rating> <SeasonNumber>6</SeasonNumber> <Writer>Norm Hiscock</Writer> <absolute_number></absolute_number> <filename>episodes/73903/131946.jpg</filename> <lastupdated>1311341141</lastupdated> <seasonid>6639</seasonid> <seriesid>73903</seriesid> </Episode> </Data>");
            File.WriteAllText("WalmartClosures&ACop'sNon-LethalForce.xml", "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> <Data> <Episode> <id>5122384</id> <Combined_episodenumber>45</Combined_episodenumber> <Combined_season>1</Combined_season> <DVD_chapter></DVD_chapter> <DVD_discid></DVD_discid> <DVD_episodenumber></DVD_episodenumber> <DVD_season></DVD_season> <Director></Director> <EpImgFlag></EpImgFlag> <EpisodeName>Walmart Closures &amp; A Cop's Non-Lethal Force</EpisodeName> <EpisodeNumber>45</EpisodeNumber> <FirstAired>2015-04-21</FirstAired> <GuestStars></GuestStars> <IMDB_ID></IMDB_ID> <Language>en</Language> <Overview>Ricky Velez, Cristela Alonzo and Scarface discuss Walmart's controversial store closings, a water crisis in Baltimore and a police officer's surprising show of restraint.</Overview> <ProductionCode></ProductionCode> <Rating></Rating> <SeasonNumber>1</SeasonNumber> <Writer></Writer> <absolute_number></absolute_number> <filename></filename> <lastupdated>1429713569</lastupdated> <seasonid>598413</seasonid> <seriesid>286373</seriesid> </Episode> </Data> ");
            File.WriteAllText("PeepingMom.xml", "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> <Data> <Episode> <id>5137481</id> <Combined_episodenumber>18</Combined_episodenumber> <Combined_season>26</Combined_season> <DVD_chapter></DVD_chapter> <DVD_discid></DVD_discid> <DVD_episodenumber></DVD_episodenumber> <DVD_season></DVD_season> <Director></Director> <EpImgFlag>2</EpImgFlag> <EpisodeName>Peeping Mom</EpisodeName> <EpisodeNumber>18</EpisodeNumber> <FirstAired>2015-04-19</FirstAired> <GuestStars></GuestStars> <IMDB_ID></IMDB_ID> <Language>en</Language> <Overview>Bart lies about being involved in a bulldozer crash, so Marge decides to follow him everywhere until he confesses. Meanwhile, Homer ignores Santa’s Little Helper when Flanders gets a new dog.</Overview> <ProductionCode></ProductionCode> <Rating></Rating> <SeasonNumber>26</SeasonNumber> <Writer></Writer> <absolute_number></absolute_number> <filename>episodes/71663/5137481.jpg</filename> <lastupdated>1429632329</lastupdated> <seasonid>588131</seasonid> <seriesid>71663</seriesid> </Episode> </Data>");
            File.WriteAllText("TheDisappearanceofHelleCrafts.xml", "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> <Data> <Episode> <id>45142</id> <Combined_episodenumber>1</Combined_episodenumber> <Combined_season>1</Combined_season> <DVD_chapter></DVD_chapter> <DVD_discid>1</DVD_discid> <DVD_episodenumber>1.0</DVD_episodenumber> <DVD_season></DVD_season> <Director></Director> <EpImgFlag></EpImgFlag> <EpisodeName>The Disappearance of Helle Crafts</EpisodeName> <EpisodeNumber>1</EpisodeNumber> <FirstAired>1996-04-21</FirstAired> <GuestStars></GuestStars> <IMDB_ID></IMDB_ID> <Language>en</Language> <Overview>In the series premiere of Forensic Files, the longest running true crime series in television history, a Connecticut flight attendant went missing and was never seen again. Police suspected her husband was guilty of murder and they were able to prove it –</Overview> <ProductionCode>1</ProductionCode> <Rating></Rating> <SeasonNumber>1</SeasonNumber> <Writer></Writer> <absolute_number>1</absolute_number> <filename></filename> <lastupdated>1406916456</lastupdated> <seasonid>2230</seasonid> <seriesid>71415</seriesid> </Episode> </Data>");
            File.WriteAllText("Patents.xml", "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> <Data> <Episode> <id>5147173</id> <Combined_episodenumber>10</Combined_episodenumber> <Combined_season>2</Combined_season> <DVD_chapter></DVD_chapter> <DVD_discid></DVD_discid> <DVD_episodenumber></DVD_episodenumber> <DVD_season></DVD_season> <Director></Director> <EpImgFlag>2</EpImgFlag> <EpisodeName>Patents</EpisodeName> <EpisodeNumber>10</EpisodeNumber> <FirstAired>2015-04-19</FirstAired> <GuestStars></GuestStars> <IMDB_ID></IMDB_ID> <Language>en</Language> <Overview>For inventors, patents are an essential protection against theft. But when patent trolls abuse the system by stockpiling patents and threatening lawsuits, businesses are forced to shell out tons of money. CNN produced an actual doomsday video to broadc</Overview> <ProductionCode></ProductionCode> <Rating></Rating> <SeasonNumber>2</SeasonNumber> <Writer></Writer> <absolute_number>34</absolute_number> <filename>episodes/278518/5147173.jpg</filename> <lastupdated>1429609597</lastupdated> <seasonid>603415</seasonid> <seriesid>278518</seriesid> </Episode> </Data> ");
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            File.Delete("KingOfTheHill.xml");
            File.Delete("TheNightlyShow.xml");
            File.Delete("BobbyGoesNuts.xml");
            File.Delete("WalmartClosures&ACop'sNon-LethalForce.xml");
            File.Delete("PeepingMom.xml");
            File.Delete("TheDisappearanceofHelleCrafts.xml");
            File.Delete("Patents.xml");
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
