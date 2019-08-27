using LeagueViewer.Entities;
using System;

namespace LeagueViewer.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<League> LeagueRepository { get; set; }
        IRepository<Team> TeamRepository { get; set; }
        IRepository<Player> PlayerRepository { get; set; }
        void Save();
    }
}
