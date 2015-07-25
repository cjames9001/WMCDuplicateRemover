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
        public virtual String ChannelID { get; set; }

        [XmlElement("title")]
        public virtual String SeriesName { get; set; }

        [XmlElement("sub-title")]
        public virtual String EpisodeName { get; set; }

        [XmlElement("desc")]
        public virtual String Description { get; set; }

        [XmlElement("date")]
        public String OriginalAirDateString { get; set; }

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

        private DateTime GetDateFromDateString(String timeString)
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
                return String.Format("{1}: {2}{0}Scheduled For: {3}{0}Description: {4}{0}ChannelID: {5}{0}Original Air Date: {6}{0}{7}.{8}{0}",
                    Environment.NewLine,
                    SeriesName,
                    EpisodeName,
                    Start,
                    Description,
                    ChannelID,
                    OriginalAirDate,
                    this.GetType().Namespace,
                    this.GetType().Name);
            }
            catch (Exception ex)
            {
                return "UnableTo-Tostring()!" + ex.Message + ex.StackTrace;
            }
        }

        #endregion
    }
}
