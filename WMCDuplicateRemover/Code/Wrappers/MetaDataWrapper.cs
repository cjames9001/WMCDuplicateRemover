using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WMCDuplicateRemover
{
    public abstract class MetaDataWrapper
    {
        //TODO: Put these in a config setting to make this configurable
        private readonly string apiPrefix = "http://thetvdb.com/api/";
        private readonly string apiKey = "B568191E5807039C";
        internal Dictionary<String, String> seriesIdCache;

        private String _seriesName;
        public String SeriesName
        {
            get
            {
                return _seriesName;
            }
            private set
            {
                _seriesName = value.ToLower();
            }
        }
        public DateTime OriginalAirDate { get; protected set; }
        public String SeriesID { get; protected set; }

        public MetaDataWrapper()
        {
            throw new NotImplementedException("The MetaDataWrapper class cannot be used without a series name and an original air date.");
        }

        public MetaDataWrapper(String seriesName, DateTime originalAirDate)
        {
            //TODO: Maybe later make this cache persistent and write to a file or something
            seriesIdCache = new Dictionary<String, String>();
            SeriesName = seriesName;
            OriginalAirDate = originalAirDate;
        }

        internal String BuildSeriesURL()
        {
            return String.Format("{0}GetSeries.php?seriesname={1}", apiPrefix, SeriesName);
        }

        internal string BuildGetEpisodeByAirDateUrl()
        {
            GetSeriesID();
            return String.Format("{0}GetEpisodeByAirDate.php?apikey={1}&seriesid={2}&airdate={3}", apiPrefix, apiKey, SeriesID, OriginalAirDate.ToShortDateString());
        }

        private void GetSeriesID()
        {
            GetSeriesIDFromCache();

            if (String.IsNullOrEmpty(SeriesID))
                GetSeriesIDFromAPI();

            if (String.IsNullOrEmpty(SeriesID))
                throw new NullReferenceException("The series was not found, not much we can do... I suppose you could contribute to the open source database.....");
        }

        protected abstract void GetSeriesIDFromAPI();

        private void GetSeriesIDFromCache()
        {
            String seriesId;
            seriesIdCache.TryGetValue(SeriesName, out seriesId);
            SeriesID = seriesId;
        }

        internal String GetEpisodeTitleFromOriginalAirDate()
        {
            var requestUrl = BuildGetEpisodeByAirDateUrl();
            return RequestTitleFromAPI();
        }

        protected abstract String RequestTitleFromAPI();

        [XmlType("Series"), Serializable]
        public class Series
        {
            [XmlElement("seriesid")]
            public String seriesid { get; set; }
            public String language { get; set; }
            public String SeriesName { get; set; }

            public String banner { get; set; }
            public String Overview { get; set; }
            public DateTime FirstAired { get; set; }
            public String Network { get; set; }
            public String IMDB_ID { get; set; }
            public String zap2it_id { get; set; }
            public String id { get; set; }
        }

        protected Series DeserializeSeriesXML(Stream xmlStream)
        {
            Series tvSeries = null;

            using (XmlReader reader = XmlReader.Create(xmlStream))
            {
                reader.MoveToContent();
                reader.ReadToDescendant("Series");
                var serializer = new XmlSerializer(typeof(Series));
                tvSeries = serializer.Deserialize(reader) as Series;
            }

            return tvSeries;
        }

        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            foreach(PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    var thisProperty = propertyInfo.GetValue(this, null);
                    var passedProperty = propertyInfo.GetValue(obj, null);

                    if (!object.Equals(thisProperty, passedProperty))
                        return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
