using LeagueViewer.Entities;
using LeagueViewer.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LeagueViewer.Services
{
    public class TeamService : ITeamService
    {
        private IUnitOfWork UnitOfWork { get; set; }

        public TeamService() { }

        public TeamService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IList<Team> GetTeamsWithLeagueId(int leagueId)
        {
            Expression<Func<Team, bool>> filter = (team => team.League.Id == leagueId);
            return UnitOfWork.TeamRepository.Get(filter, null, "League");
        }

        public bool ExistsTeam(int teamId)
        {
            Expression<Func<Team, bool>> condition = (team => team.Id == teamId);
            return UnitOfWork.TeamRepository.Exists(condition);
        }
    }
}
