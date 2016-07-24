using System;

namespace WMCDuplicateRemover
{
    public abstract class EventLogWrapper
    {
        public abstract bool FoundEventForRecording(String seriesName, String episodeName);
    }
}
