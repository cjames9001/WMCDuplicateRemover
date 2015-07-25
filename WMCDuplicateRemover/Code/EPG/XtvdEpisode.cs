using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WMCDuplicateRemover.Code.EPG
{
    class XtvdEpisode : Episode
    {
        private schedulesSchedule _xtvdEpisodeSchedule;
        private programsProgram _xtvdEpisodeData;

        public XtvdEpisode(schedulesSchedule episodeSchedule, programsProgram episodeData)
        {
            _xtvdEpisodeSchedule = episodeSchedule;
            _xtvdEpisodeData = episodeData;
        }

        public new String ChannelID
        {
            get
            {
                return _xtvdEpisodeSchedule.station.ToString();
            }
        }

        public new String SeriesName
        {
            get
            {
                return _xtvdEpisodeData.title;
            }
        }

        public new String EpisodeName
        {
            get
            {
                return _xtvdEpisodeData.subtitle;
            }
        }

        public new String Description
        {
            get
            {
                return _xtvdEpisodeData.description;
            }
        }

        public new DateTime OriginalAirDate
        {
            get
            {
                return _xtvdEpisodeData.originalAirDate;
            }
        }

        public new DateTime Start
        {
            get
            {
                return _xtvdEpisodeSchedule.time;
            }
        }

        public new DateTime End
        {
            get
            {
                string duration = _xtvdEpisodeSchedule.duration;
                return Start.Add(new TimeSpan(int.Parse(duration.Substring(2, 2)), int.Parse(duration.Substring(5, 2)), 2));
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
