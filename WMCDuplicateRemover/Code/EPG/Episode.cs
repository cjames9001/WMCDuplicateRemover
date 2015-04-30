using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace WMCDuplicateRemover.Code.EPG
{
    public class Episode
    {
        [XmlAttribute("start")]
        public String StartString { get; set; }

        [XmlAttribute("stop")]
        public String EndString { get; set; }

        [XmlAttribute("channel")]
        public String ChannelID { get; set; }

        [XmlElement("title")]
        public String SeriesName { get; set; }

        [XmlElement("sub-title")]
        public String EpisodeName { get; set; }

        [XmlElement("desc")]
        public String Description { get; set; }

        [XmlElement("date")]
        public String OriginalAirDateString { get; set; }

        public DateTime OriginalAirDate
        {
            get
            {
                return GetDateFromDateString(OriginalAirDateString + "000000");
            }
        }

        public DateTime Start
        {
            get
            {
                return GetDateFromDateString(StartString);
            }
        }

        public DateTime End
        {
            get
            {
                return GetDateFromDateString(EndString);
            }
        }

        private DateTime GetDateFromDateString(String timeString)
        {
            var year = Convert.ToInt32(timeString.Substring(0, 4));
            var month = Convert.ToInt32(timeString.Substring(4, 2));
            var day = Convert.ToInt32(timeString.Substring(6, 2));
            var hour = Convert.ToInt32(timeString.Substring(8, 2));
            var minute = Convert.ToInt32(timeString.Substring(10, 2));
            var second = Convert.ToInt32(timeString.Substring(12, 2));
            return new DateTime(year, month, day, hour, minute, second);
        }

        #region Object overrides

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
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

        public override string ToString()
        {
            try
            {
                return String.Format("{0}.{1}{2}Series Title: {3}{2}Episode Title: {4}{2}Description: {5}{2}ChannelID: {6}{2}Original Air Date: {7}{2}",
                    this.GetType().Namespace,
                    this.GetType().Name,
                    Environment.NewLine,
                    SeriesName,
                    EpisodeName,
                    Description,
                    ChannelID,
                    OriginalAirDate);
            }
            catch (Exception ex)
            {
                return "UnableTo-Tostring()!" + ex.Message + ex.StackTrace;
            }
        }

        #endregion
    }
}
