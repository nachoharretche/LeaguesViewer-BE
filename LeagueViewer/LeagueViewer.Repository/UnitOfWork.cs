using LeagueViewer.DataAccess;
using LeagueViewer.Entities;
using System;

namespace LeagueViewer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Properties
        private LeagueViewerContext context;
        private GenericRepository<League> leagueRepository;
        private GenericRepository<Team> teamRepository;
        private GenericRepository<Player> playerRepository;
        private bool disposed = false;
        #endregion

        public UnitOfWork() { }

        public UnitOfWork(LeagueViewerContext leagueViewerContext)
        {
            context = leagueViewerContext;
        }

        public UnitOfWork(LeagueViewerContext leagueViewerContext, 
            GenericRepository<League> leagueRepository,
            GenericRepository<Team> teamRepository,
            GenericRepository<Player> playerRepository)
        {
            context = leagueViewerContext;
            this.leagueRepository = leagueRepository;
            this.teamRepository = teamRepository;
            this.playerRepository = playerRepository;
        }

        #region Public Properties
        public virtual IRepository<League> LeagueRepository
        {
            get
            {
                if (leagueRepository == null)
                    leagueRepository = new GenericRepository<League>(context);
                return leagueRepository;
            }
            set
            {
                LeagueRepository = leagueRepository;
            }
        }

        public virtual IRepository<Team> TeamRepository
        {
            get
            {
                if (teamRepository == null)
                    teamRepository = new GenericRepository<Team>(context);
                return teamRepository;
            }
            set
            {
                TeamRepository = teamRepository;
            }
        }

        public virtual IRepository<Player> PlayerRepository
        {
            get
            {
                if (playerRepository == null)
                    playerRepository = new GenericRepository<Player>(context);
                return playerRepository;
            }
            set
            {
                PlayerRepository = playerRepository;
            }
        }
        #endregion

        public virtual void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
