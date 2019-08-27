using LeagueViewer.Entities;
using System.Collections.Generic;

namespace LeagueViewer.Services
{
    public interface IPlayerService
    {
        IList<Player> GetPlayersWithTeamId(int teamId);
        Player AddPlayer(Player player);
    }
}
