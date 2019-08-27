using LeagueViewer.Entities;
using LeagueViewer.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LeagueViewer.Services
{
    public class PlayerService : IPlayerService
    {
        #region Private Properties
        private IUnitOfWork UnitOfWork { get; set; }
        private ITeamService TeamService { get; set; }
        private const int MIN_SHIRT_NUMBER = 1;
        private const int MAX_SHIRT_NUMBER = 99;
        private const int MIN_AGE = 16;
        private const int MAX_AGE = 55;
        #endregion
        public PlayerService() { }

        public PlayerService(IUnitOfWork unitOfWork, ITeamService teamService)
        {
            UnitOfWork = unitOfWork;
            TeamService = teamService;
        }

        public IList<Player> GetPlayersWithTeamId(int teamId)
        {
            Expression<Func<Player, bool>> filter = 
                (player => player.TeamId == teamId);
            return UnitOfWork.PlayerRepository.Get(filter);
        }

        public Player AddPlayer(Player player)
        {
            ValidatePlayer(player);
            UnitOfWork.PlayerRepository.Insert(player);
            UnitOfWork.Save();
            return player;
        }

        private void ValidatePlayer(Player player)
        {
            ValidateName(player.FullName);
            ValidateAge(player.Age);
            ValidateShirtNumber(player.ShirtNumber);
            ValidateTeam(player.TeamId);
            ValidatePlayerExists(player);
        }

        private void ValidateName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new BusinessLogicException("Debe ingresar el nombre " +
                    "completo");
        }

        private void ValidateAge(int age)
        {
            if (age < MIN_AGE || age > MAX_AGE)
                throw new BusinessLogicException("Solo se pueden registrar " +
                    "jugadores entre los "+ MIN_AGE +" y "+ MAX_AGE +" años");
        }

        private void ValidateShirtNumber(int shirtNumber)
        {
            if (shirtNumber < MIN_SHIRT_NUMBER || shirtNumber > MAX_SHIRT_NUMBER)
                throw new BusinessLogicException("Solo se pueden registrar " +
                    "números entre el " + MIN_SHIRT_NUMBER +" y " + MAX_SHIRT_NUMBER);
        }

        private void ValidateTeam(int teamId)
        {
            if (!TeamService.ExistsTeam(teamId))
                throw new BusinessLogicException("El equipo seleccionado no " +
                    "existe");
        }

        private void ValidatePlayerExists(Player playerToCompare)
        {
            Expression<Func<Player, bool>> filter = (player => 
            player.FullName.Equals(playerToCompare.FullName) && 
            player.Age == playerToCompare.Age && 
            player.ShirtNumber == playerToCompare.ShirtNumber && 
            player.TeamId == playerToCompare.TeamId);
            Player gottenPlayer = UnitOfWork.PlayerRepository.GetByFilterWithOtherProperties(filter);
            if(gottenPlayer != null)
                throw new BusinessLogicException("El jugador ya está registrado en el equipo");
        }
    }
}
