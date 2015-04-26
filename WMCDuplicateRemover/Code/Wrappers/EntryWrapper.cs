﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMCDuplicateRemover
{
    public abstract class EntryWrapper
    {
        public String EventLogMessage { get; private set; }
        public abstract bool FoundEventForRecording(String seriesName, String episodeName);
    }
}