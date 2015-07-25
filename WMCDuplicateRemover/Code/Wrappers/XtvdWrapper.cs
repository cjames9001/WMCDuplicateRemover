using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMCDuplicateRemover.Code.EPG
{
    public class XtvdWrapper
    {
        private lineupsLineupMap[] _stationToChannelMap;

        public XtvdWrapper(IEnumerable<int> channelsScheduledForRecordings)
        {
            xtvd epgDataFromService = RequestEpgData();
            _stationToChannelMap = epgDataFromService.lineups.First().map;
            Listings.Programs = GetEpisodes(epgDataFromService);
            var scheduledStationIds = channelsScheduledForRecordings.Select(x => GetStationIdFromChannelNumber(x));
            Listings.Programs = Listings.Programs.Where(x => x.Start >= DateTime.Now && scheduledStationIds.Contains(x.ChannelID)).ToList();
        }

        private xtvd RequestEpgData()
        {
            xtvdWebService xtvd = new xtvdWebService();
            System.Net.NetworkCredential cred = new System.Net.NetworkCredential("", "");
            xtvd.Credentials = cred;
            return xtvd.download(DateTime.Now.ToString(@"yyyy-MM-ddT00:00:00Z"), DateTime.Now.AddDays(7).ToString(@"yyyy-MM-ddT00:00:00Z")).xtvd;
        }

        private List<Episode> GetEpisodes(xtvd epgDataFromService)
        {
            schedulesSchedule[] epgEpisodes = epgDataFromService.schedules;
            programsProgram[] epgEpisodeData = epgDataFromService.programs;

            var episodes = new List<Episode>();
            foreach (var episode in epgEpisodes)
            {
                episodes.Add(new XtvdEpisode(episode, epgEpisodeData.First(x => x.id == episode.program)));
            }
            return episodes;
        }

        private Listing Listings { get; set; }

        public class Listing
        {
            public List<Episode> Programs { get; set; }
        }

        internal string GetStationIdFromChannelNumber(int channelNumber)
        {
            return _stationToChannelMap.First(x => x.channel == channelNumber.ToString()).station.ToString();
        }

        public Episode GetEpisodeMetaDataBasedOnWMCMetaData(DateTime startTime, DateTime endTime, DateTime originalAirDate, int channelNumber)
        {
            var channelId = GetStationIdFromChannelNumber(channelNumber);
            var possibleEpisodes = Listings.Programs.Where(x => (x.OriginalAirDate != DateTime.MinValue && x.ChannelID == channelId && x.Start == startTime && x.End == endTime && x.OriginalAirDate == originalAirDate)).ToList();

            if (possibleEpisodes.Count > 1)
                throw new InvalidOperationException("There can only be one episode, there must be something terribly wrong with the EPG Data. Certainty is not 100% and cannot continue");
            if (possibleEpisodes.Count < 1)
                throw new NullReferenceException("There are no episodes found for this listing");

            return possibleEpisodes.First();
        }
    }
}
