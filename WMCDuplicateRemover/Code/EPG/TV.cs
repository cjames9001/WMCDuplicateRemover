using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WMCDuplicateRemover.Code.EPG
{
    public class TV
    {
        private Listing Listings { get; set; }

        public TV(String EPGPath)
        {
            var fs = new FileStream(EPGPath, FileMode.Open);
            XmlReaderSettings settings = new XmlReaderSettings()
            {
                XmlResolver = null,
                ProhibitDtd = false
            };

            using (XmlReader reader = XmlReader.Create(fs, settings))
            {
                var serializer = new XmlSerializer(typeof(Listing));
                Listings = (Listing)serializer.Deserialize(reader);
            }
            fs.Close();
        }

        [XmlType("tv"), Serializable]
        public class Listing
        {
            [XmlElement("programme")]
            public List<Episode> Programs { get; set; }

            [XmlElement("channel")]
            public List<TVChannel> Channels { get; set; }

            
        }

        internal String GetChannelFromNumber(int channelNumber)
        {
            var tvChannel = Listings.Channels.FirstOrDefault(x => x.ChannelInfo[1] == channelNumber.ToString());
            return tvChannel.ChannelID;
        }

        public Episode GetEpisodeMetaDataBasedOnWMCMetaData(DateTime startTime, DateTime endTime, DateTime originalAirDate, int channelNumber)
        {
            var channelId = GetChannelFromNumber(channelNumber);
            var possibleEpisodes = Listings.Programs.Where(x => (x.ChannelID == channelId && x.Start == startTime && x.End == endTime && x.OriginalAirDate == originalAirDate)).ToList();

            if (possibleEpisodes.Count > 1)
                throw new InvalidOperationException("There can only be one episode, there must be something terribly wrong with the EPG Data. Certainty is not 100% and cannot continue");
            if (possibleEpisodes.Count < 1)
                throw new NullReferenceException("There are no episodes found for this listing");

            return possibleEpisodes.First();
        }
    }
}
