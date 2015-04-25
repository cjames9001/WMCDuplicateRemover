using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
    }
}
