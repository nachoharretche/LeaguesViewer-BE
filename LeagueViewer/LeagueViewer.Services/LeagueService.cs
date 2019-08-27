using LeagueViewer.Entities;
using LeagueViewer.Repository;
using System.Collections.Generic;

namespace LeagueViewer.Services
{
    public class LeagueService : ILeagueService
    {
        private IUnitOfWork UnitOfWork { get; set; }

        public LeagueService() { }

        public LeagueService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IList<League> GetLeagues()
        {
            return UnitOfWork.LeagueRepository.Get();
        }

        public League GetLeague(int leagueId)
        {
            return UnitOfWork.LeagueRepository.GetByID(leagueId);
        }
    }
}
