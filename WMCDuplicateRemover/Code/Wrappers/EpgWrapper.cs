using System;
using System.Collections.Generic;
using System.Linq;
using WMCDuplicateRemover.Code.EPG;
using System.Xml.Serialization;

namespace WMCDuplicateRemover.Code.Wrappers
{
    public abstract class EpgWrapper
    {
        protected Listing Listings { get; set; }

        [XmlType("tv"), Serializable]
        public class Listing
        {
            [XmlElement("programme")]
            public List<Episode> Programs { get; set; }

            [XmlElement("channel")]
            public List<TVChannel> Channels { get; set; }
        }

        private bool DateTimeWithinFiveMinutes(DateTime firstDate, DateTime secondDate)
        {
            return (secondDate.AddMinutes(-5) < firstDate && firstDate < secondDate.AddMinutes(5));
        }

        public Episode GetEpisodeMetaDataBasedOnWMCMetaData(DateTime startTime, DateTime endTime, DateTime originalAirDate, int channelNumber)
        {
            var channelId = GetEpgChannelFromNumber(channelNumber);
            var possibleEpisodes = Listings.Programs.Where(x => x.ChannelId == channelId && DateTimeWithinFiveMinutes(x.Start, startTime) && DateTimeWithinFiveMinutes(x.End, endTime)).ToList();
            
            if (possibleEpisodes.Count > 1)
                throw new InvalidOperationException("There can only be one episode, there must be something terribly wrong with the EPG Data. Certainty is not 100% and cannot continue");
            if (possibleEpisodes.Count < 1)
                throw new NullReferenceException("There are no episodes found for this listing");

            return possibleEpisodes.First();
        }

        internal abstract string GetEpgChannelFromNumber(int channelNumber);
    }
}
