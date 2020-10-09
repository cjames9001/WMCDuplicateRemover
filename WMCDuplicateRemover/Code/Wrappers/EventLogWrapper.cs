namespace WMCDuplicateRemover
{
    public abstract class EventLogWrapper
    {
        public abstract bool FoundEventForRecording(string seriesName, string episodeName, string episodePart);
    }
}
