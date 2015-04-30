using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMCDuplicateRemover;

namespace WMCDuplicateRemover.Tests
{
    [TestFixture, Explicit]
    public class ScheduledEventIntegrationTests
    {
        private EventLogEntryWrapper eventLogWrapper;

        [TestFixtureSetUp]
        public void SetUp()
        {
            eventLogWrapper = new EventLogEntryWrapper();
        }

        [TestCaseSource(typeof(ScheduledEventCancellationIntegrationTests))]
        public bool TestCanCancelScheduledEvent(ScheduledEvent scheduledEvent)
        {
            var metaDataWrapper = new TheTVDBWrapper(scheduledEvent.Title, scheduledEvent.OriginalAirDate);
            return scheduledEvent.CanEventBeCancelled(eventLogWrapper, metaDataWrapper);
        }
    }
}
