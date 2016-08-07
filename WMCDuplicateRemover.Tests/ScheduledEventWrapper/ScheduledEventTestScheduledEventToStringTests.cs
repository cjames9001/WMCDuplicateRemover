using Microsoft.MediaCenter.TV.Scheduling;
using NUnit.Framework;
using System;
using System.Collections;

namespace WMCDuplicateRemover.Tests.ScheduledEventWrapper
{
    public class ScheduledEventTestScheduledEventToStringTests : TestData
    {
        protected override IEnumerable CreateTestData()
        {
            yield return new TestCaseData(new MockScheduledEvent(), 
                $"WMCDuplicateRemover.Tests.MockScheduledEvent\r\nSeries Title: {default(string)}\r\nEpisode Title: {default(string)}\r\nDescription: {default(string)}\r\nServiceId: {default(string)}\r\nChannelId: {default(string)}\r\nKeep Until: {default(int)}\r\nQuality: {default(int)}\r\nPartial: {default(bool)}\r\nProvider Copyright: {default(string)}\r\nOriginal Air Date: {default(DateTime)}\r\nRepeat: {default(bool)}\r\nGenre: {default(string)}\r\nFile Name: {default(string)}\r\nStart Time: {default(DateTime)}\r\nEnd Time: {default(DateTime)}\r\nState: {default(ScheduleEventStates)}\r\n")
                .SetDescription("Checks the ToString method returns the correct string with an unpopulated object")
                .SetName("TestToStringWithoutValues");

            yield return new TestCaseData(new MockScheduledEvent("Star Trek The Next Generation",
                "23498",
                "Riker returns to the site of an 8-year-old mission to retrieve important research data and is unnerved to encounter his identical twin.",
                30,
                5,
                false,
                "None",
                new DateTime(1993, 5, 24),
                true,
                "Sci-Fi",
                null,
                new DateTime(2016, 7, 25, 17, 30, 0),
                new DateTime(2016, 7, 25, 18, 30, 0),
                637,
                ScheduleEventStates.WillOccur)
            {
                EpisodeTitle = "Second Chances"
            }, "WMCDuplicateRemover.Tests.MockScheduledEvent\r\nSeries Title: Star Trek The Next Generation\r\nEpisode Title: Second Chances\r\nDescription: Riker returns to the site of an 8-year-old mission to retrieve important research data and is unnerved to encounter his identical twin.\r\nServiceId: 23498\r\nChannelId: 637\r\nKeep Until: 30\r\nQuality: 5\r\nPartial: False\r\nProvider Copyright: None\r\nOriginal Air Date: 5/24/1993 12:00:00 AM\r\nRepeat: True\r\nGenre: Sci-Fi\r\nFile Name: \r\nStart Time: 7/25/2016 5:30:00 PM\r\nEnd Time: 7/25/2016 6:30:00 PM\r\nState: WillOccur\r\n")
                .SetDescription("Checks the ToString method returns the correct string with a populated object")
                .SetName("TestToStringWithValues");
        }
    }
}
