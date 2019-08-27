using LeagueViewer.Entities;
using System.Collections.Generic;

namespace LeagueViewer.Services
{
    public interface ITeamService
    {
        IList<Team> GetTeamsWithLeagueId(int leagueId);
        bool ExistsTeam(int teamId);
    }
}
