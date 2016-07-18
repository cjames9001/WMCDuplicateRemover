using System;

namespace WMCDuplicateRemover.Code.Wrappers
{
    public class DateTimeWrapper : IDateTime
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
