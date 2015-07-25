using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMCDuplicateRemover.Code.EPG;

namespace WMCDuplicateRemover.Code.Wrappers
{
    public class SchedulesDirectEpgXTvdWrapper : EpgWrapper
    {
        private lineupsLineupMap[] _stationToChannelMap;

        public SchedulesDirectEpgXTvdWrapper(IEnumerable<int> channelsScheduledForRecordings)
        {
            Listings = new Listing();
            xtvd epgDataFromService = RequestEpgData();
            _stationToChannelMap = epgDataFromService.lineups.First().map;
            Listings.Programs = GetEpisodes(epgDataFromService);
            var scheduledStationIds = channelsScheduledForRecordings.Select(x => GetEpgChannelFromNumber(x));
            Listings.Programs = Listings.Programs.Where(x => x.Start >= DateTime.Now && scheduledStationIds.Contains(x.ChannelID)).ToList();
        }

        private xtvd RequestEpgData()
        {
            xtvdWebService xtvd = new xtvdWebService();
            System.Net.NetworkCredential cred = new System.Net.NetworkCredential("cjames9001", "skyloo100");
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
                try
                {
                    episodes.Add(new XtvdEpisode(episode, epgEpisodeData.First(x => x.id == episode.program)));
                }
                catch (Exception)
                {
                    //Something is wrong with that particular epg episode data...
                }
            }
            return episodes;
        }

        internal override string GetEpgChannelFromNumber(int channelNumber)
        {
            return _stationToChannelMap.First(x => x.channel == channelNumber.ToString()).station.ToString();
        }
    }
}
