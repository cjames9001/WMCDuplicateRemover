using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace WMCDuplicateRemover
{
    public class TheTVDBWrapper : MetaDataWrapper
    {
        public TheTVDBWrapper() : base()
        {
        }

        public TheTVDBWrapper(String seriesName, DateTime originalAirDate) : base(seriesName, originalAirDate)
        {

        }

        protected override void GetSeriesIDFromAPI()
        {
            HttpWebRequest request = WebRequest.Create(BuildSeriesURL()) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            
            SeriesID = DeserializeXMLStreamStartingAtNode<SeriesMetaData>(response.GetResponseStream(), "Series").SeriesID;

            response.Close();

            UpdateCache();
        }

        private void UpdateCache()
        {
            if (!String.IsNullOrEmpty(SeriesID))
                seriesIdCache.Add(SeriesName, SeriesID);
        }

        protected override string RequestTitleFromAPI()
        {
            HttpWebRequest request = WebRequest.Create(BuildGetEpisodeByAirDateUrl()) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            var episodeName = DeserializeXMLStreamStartingAtNode<EpisodeMetaData>(response.GetResponseStream(), "Episode").EpisodeName;

            response.Close();

            return episodeName;
        }
    }
}
