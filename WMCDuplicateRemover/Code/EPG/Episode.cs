using System;
using System.Xml.Serialization;

namespace WMCDuplicateRemover.Code.EPG
{
    public class Episode
    {
        [XmlAttribute("start")]
        public string StartString { get; set; }

        [XmlAttribute("stop")]
        public string EndString { get; set; }

        [XmlAttribute("channel")]
        public virtual string ChannelId { get; set; }

        [XmlElement("title")]
        public virtual string SeriesName { get; set; }

        [XmlElement("sub-title")]
        public virtual string EpisodeName { get; set; }

        [XmlElement("desc")]
        public virtual string Description { get; set; }

        [XmlElement("date")]
        public string OriginalAirDateString { get; set; }

        public virtual DateTime OriginalAirDate
        {
            get
            {
                return GetDateFromDateString(OriginalAirDateString + "000000");
            }
        }

        public virtual DateTime Start
        {
            get
            {
                return GetDateFromDateString(StartString);
            }
        }

        public virtual DateTime End
        {
            get
            {
                return GetDateFromDateString(EndString);
            }
        }

        private DateTime GetDateFromDateString(string timeString)
        {
            try
            {
                var year = Convert.ToInt32(timeString.Substring(0, 4));
                var month = Convert.ToInt32(timeString.Substring(4, 2));
                var day = Convert.ToInt32(timeString.Substring(6, 2));
                var hour = Convert.ToInt32(timeString.Substring(8, 2));
                var minute = Convert.ToInt32(timeString.Substring(10, 2));
                var second = Convert.ToInt32(timeString.Substring(12, 2));
                return new DateTime(year, month, day, hour, minute, second);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public bool MetaDataAllowsForCancellation()
        {
            if (OriginalAirDate.Date == DateTime.MinValue.Date || OriginalAirDate.Date == DateTime.Now.Date)
                return false;
            if (OriginalAirDate < DateTime.Now)
                return true;
            return false;
        }

        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            foreach (var propertyInfo in GetType().GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    var thisProperty = propertyInfo.GetValue(this, null);
                    var passedProperty = propertyInfo.GetValue(obj, null);

                    if (!Equals(thisProperty, passedProperty))
                        return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            try
            {
                return $"{SeriesName}: {EpisodeName}{Environment.NewLine}Scheduled For: {Start}{Environment.NewLine}Description: {Description}{Environment.NewLine}ChannelID: {ChannelId}{Environment.NewLine}Original Air Date: {OriginalAirDate}{Environment.NewLine}{GetType().Namespace}.{GetType().Name}{Environment.NewLine}";
            }
            catch (Exception ex)
            {
                return "UnableTo-Tostring()!" + ex.Message + ex.StackTrace;
            }
        }

        #endregion
    }
}
