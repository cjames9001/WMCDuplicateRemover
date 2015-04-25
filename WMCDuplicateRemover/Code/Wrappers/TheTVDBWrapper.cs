using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace WMCDuplicateRemover
{
    public class TheTVDBWrapper : MetaDataWrapper
    {
        public TheTVDBWrapper() : base()
        {
        }

        public TheTVDBWrapper(String seriesName, DateTime originalAirDate) : base(seriesName, originalAirDate)
        {

        }

        protected override void GetSeriesIDFromAPI()
        {
            HttpWebRequest request = WebRequest.Create(BuildSeriesURL()) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            SeriesID = DeserializeSeriesXML(response.GetResponseStream()).seriesid;
        }

        protected override string RequestTitleFromAPI()
        {
            //TODO: Do Some logic to get this to work and get my info from the api
            throw new NotImplementedException();
        }
    }
}
