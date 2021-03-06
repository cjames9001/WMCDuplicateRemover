﻿using System;
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
                DtdProcessing = DtdProcessing.Ignore
            };

            using (XmlReader reader = XmlReader.Create(fs, settings))
            {
                var serializer = new XmlSerializer(typeof(Listing));
                Listings = (Listing)serializer.Deserialize(reader);
            }
            fs.Close();

            Listings.Programs = Listings.Programs.Where(x => x.Start >= currentDateTime.Now()).ToList();
        }

        public XmlTvEpgWrapper(string EPGPath, IEnumerable<int> channelsScheduledForRecordings)
        {
            var fs = new FileStream(EPGPath, FileMode.Open);
            XmlReaderSettings settings = new XmlReaderSettings()
            {
                XmlResolver = null,
                DtdProcessing = DtdProcessing.Ignore
            };

            using (XmlReader reader = XmlReader.Create(fs, settings))
            {
                var serializer = new XmlSerializer(typeof(Listing));
                Listings = (Listing)serializer.Deserialize(reader);
            }
            fs.Close();

            var scheduledChannelIds = channelsScheduledForRecordings.Select(x => GetEpgChannelFromNumber(x));
            Listings.Programs = Listings.Programs.Where(x => x.Start >= DateTime.Now && scheduledChannelIds.Contains(x.ChannelId)).ToList();
        }

        internal override string GetEpgChannelFromNumber(int channelNumber)
        {
            var tvChannel = Listings.Channels.FirstOrDefault(x => x.ChannelInfo[1] == channelNumber.ToString());
            return tvChannel == null ? "" : tvChannel.ChannelId;
        }
    }
}
