using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WMCDuplicateRemover.Code.EPG
{
    public class TVChannel
    {
        [XmlAttribute("id")]
        public String ChannelID { get; set; }

        [XmlElement("display-name")]
        public List<String> ChannelInfo { get; set; }
    }
}
