using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Threading;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using Microsoft.Win32;
using WMCDuplicateRemover;
using WMCDuplicateRemover.Code.EPG;

namespace WMCDuplicateRemoverDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            var driver = new Driver();
            driver.Run();
        }
    }
}
