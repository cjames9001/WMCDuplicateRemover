using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WMCDuplicateRemover.Code.EPG
{
    public class XtvdEpisode : Episode
    {
        private schedulesSchedule _xtvdEpisodeSchedule;
        private programsProgram _xtvdEpisodeData;

        public XtvdEpisode(schedulesSchedule episodeSchedule, programsProgram episodeData)
        {
            _xtvdEpisodeSchedule = episodeSchedule;
            _xtvdEpisodeData = episodeData;
        }

        public override String ChannelID
        {
            get
            {
                return _xtvdEpisodeSchedule.station.ToString();
            }
        }

        public override String SeriesName
        {
            get
            {
                return _xtvdEpisodeData.title;
            }
        }

        public override String EpisodeName
        {
            get
            {
                return _xtvdEpisodeData.subtitle;
            }
        }

        public override String Description
        {
            get
            {
                return _xtvdEpisodeData.description;
            }
        }

        public override DateTime OriginalAirDate
        {
            get
            {
                return _xtvdEpisodeData.originalAirDate;
            }
        }

        public override DateTime Start
        {
            get
            {
                return _xtvdEpisodeSchedule.time.ToLocalTime();
            }
        }

        public override DateTime End
        {
            get
            {
                string duration = _xtvdEpisodeSchedule.duration;
                return Start.Add(new TimeSpan(int.Parse(duration.Substring(2, 2)), int.Parse(duration.Substring(5, 2)), 0));
            }
        }
    }
}
