using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
        public DateTime OriginalAirDate { get; private set; }
        public String SeriesID { get; private set; }

        public MetaDataWrapper()
        {
            throw new NotImplementedException("The MetaDataWrapper class cannot be used without a series name and an original air date.");
        }

        public MetaDataWrapper(String seriesName, DateTime originalAirDate)
        {
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
            String seriesId;
            seriesIdCache.TryGetValue(SeriesName, out seriesId);
            SeriesID = seriesId;

            if (String.IsNullOrEmpty(SeriesID))
                throw new NullReferenceException("The series was not found, not much we can do... I suppose you could contribute to the open source database.....");
        }

        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var typedObject = obj as TheTVDBWrapper;

            foreach(PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    var thisProperty = propertyInfo.GetValue(this, null);
                    var passedProperty = propertyInfo.GetValue(typedObject, null);

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
