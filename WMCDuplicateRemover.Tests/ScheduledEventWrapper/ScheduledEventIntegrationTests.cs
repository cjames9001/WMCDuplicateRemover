using NUnit.Framework;

namespace WMCDuplicateRemover.Tests
{
    [TestFixture, Explicit]
    public class ScheduledEventIntegrationTests
    {
        private MicrosoftEventLogWrapper eventLogWrapper;

        [TestFixtureSetUp]
        public void SetUp()
        {
            eventLogWrapper = new MicrosoftEventLogWrapper();
        }

        [TestCaseSource(typeof(ScheduledEventCancellationIntegrationTests))]
        public bool TestCanCancelScheduledEvent(ScheduledEvent scheduledEvent)
        {
            var metaDataWrapper = new TheTVDBWrapper(scheduledEvent.Title, scheduledEvent.OriginalAirDate);
            return scheduledEvent.CanEventBeCancelled(eventLogWrapper, metaDataWrapper);
        }
    }
}
