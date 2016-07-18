using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace WMCDuplicateRemover.Code.Wrappers
{
    public class XmlTvEpgWrapper : EpgWrapper
    {
        public XmlTvEpgWrapper(string epgPath, IDateTime currentDateTime)
        {
            var fs = new FileStream(epgPath, FileMode.Open);
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

            Listings.Programs = Listings.Programs.Where(x => x.Start >= currentDateTime.Now()).ToList();
        }

        public XmlTvEpgWrapper(String EPGPath, IEnumerable<int> channelsScheduledForRecordings)
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

            var scheduledChannelIds = channelsScheduledForRecordings.Select(x => GetEpgChannelFromNumber(x));
            Listings.Programs = Listings.Programs.Where(x => x.Start >= DateTime.Now && scheduledChannelIds.Contains(x.ChannelID)).ToList();
        }

        internal override String GetEpgChannelFromNumber(int channelNumber)
        {
            var tvChannel = Listings.Channels.FirstOrDefault(x => x.ChannelInfo[1] == channelNumber.ToString());
            return tvChannel == null ? "" : tvChannel.ChannelID;
        }
    }
}
