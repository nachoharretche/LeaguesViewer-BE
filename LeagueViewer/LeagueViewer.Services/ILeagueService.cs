using LeagueViewer.Entities;
using System.Collections.Generic;

namespace LeagueViewer.Services
{
    public interface ILeagueService
    {
        IList<League> GetLeagues();
        League GetLeague(int leagueId);
    }
}
