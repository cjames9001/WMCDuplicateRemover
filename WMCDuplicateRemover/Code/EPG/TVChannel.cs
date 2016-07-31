using System.Collections.Generic;
using System.Xml.Serialization;

namespace WMCDuplicateRemover.Code.EPG
{
    public class TVChannel
    {
        [XmlAttribute("id")]
        public string ChannelId { get; set; }

        [XmlElement("display-name")]
        public List<string> ChannelInfo { get; set; }
    }
}
