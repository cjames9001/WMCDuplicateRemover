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
                    SeriesID = GetFromXML("KingOfTheHill.xml");
                    break;
                case "the nightly show with larry wilmore":
                    SeriesID = GetFromXML("TheNightlyShow.xml");
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
                    return "Bobby Goes Nuts";
                case "the nightly show with larry wilmore":
                    return "Walmart Closures & A Cop's Non-Lethal Force";
                case "the simpsons":
                    return "Peeping Mom";
                case "forensic files":
                    return "The Disappearance of Helle Crafts";
                case "last week tonight":
                    return "Patents";
            }

            throw new NullReferenceException("The episode was not found, not much we can do... I suppose you could contribute to the open source database.....");
        }

        private string GetFromXML(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open);
            Series series = DeserializeSeriesXML(fs);
            fs.Close();
            return series.SeriesID;
        }
    }
}
