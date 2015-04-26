using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover.Tests
{
    public class MockMetaDataWrapper : MetaDataWrapper
    {
        public MockMetaDataWrapper() : base()
        {
        }

        public MockMetaDataWrapper(String seriesName, DateTime originalAirDate) : base(seriesName, originalAirDate)
        {

        }

        protected override void GetSeriesIDFromAPI()
        {
            switch (SeriesName)
            {
                case "king of the hill":
                    SeriesID = GetSeriesMetaDataFromXML("KingOfTheHill.xml");
                    break;
                case "the nightly show with larry wilmore":
                    SeriesID = GetSeriesMetaDataFromXML("TheNightlyShow.xml");
                    break;
                default:
                    break;
            }
        }

        protected override string RequestTitleFromAPI()
        {
            switch (SeriesName)
            {
                case "king of the hill":
                    return GetEpisodeMetaDataFromXML("BobbyGoesNuts.xml");
                case "the nightly show with larry wilmore":
                    return GetEpisodeMetaDataFromXML("WalmartClosures&ACop'sNon-LethalForce.xml");
                case "the simpsons":
                    return GetEpisodeMetaDataFromXML("PeepingMom.xml");
                case "forensic files":
                    return GetEpisodeMetaDataFromXML("TheDisappearanceofHelleCrafts.xml");
                case "last week tonight":
                    return GetEpisodeMetaDataFromXML("Patents.xml");
            }

            throw new NullReferenceException("The episode was not found, not much we can do... I suppose you could contribute to the open source database.....");
        }

        private string GetSeriesMetaDataFromXML(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open);
            SeriesMetaData series = DeserializeSeriesXML<SeriesMetaData>(fs, "Series");
            fs.Close();
            return series.SeriesID;
        }

        private string GetEpisodeMetaDataFromXML(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open);
            EpisodeMetaData episode = DeserializeSeriesXML<EpisodeMetaData>(fs, "Episode");
            fs.Close();
            return episode.EpisodeName;
        }
    }
}
