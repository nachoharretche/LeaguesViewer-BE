using System;
using System.Collections.Generic;
using LeagueViewer.Entities;
using LeagueViewer.Repository;
using LeagueViewer.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LeagueViewer.Api.Controllers
{
    [Route("api/Players")]
    [ApiController]
    [EnableCors("MyPolicity")]
    public class PlayerController : ControllerBase
    {
        private IPlayerService playerService;
        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet]
        [Route("{teamId}")]
        public ActionResult Get(int teamId)
        {
            try
            {
                IList<Player> players = playerService.GetPlayersWithTeamId(teamId);
                return Ok(players);
            }
            catch (BusinessLogicException e)
            {
                return BadRequest(e.Message);
            }
            catch (PersistentStoreException e)
            {
                Console.WriteLine(e.Message, e.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult Post(Player player)
        {
            try
            {
                player = playerService.AddPlayer(player);
                return Ok(player);
            }
            catch (BusinessLogicException e)
            {
                return BadRequest(e.Message);
            }
            catch (PersistentStoreException e)
            {
                Console.WriteLine(e.Message, e.ToString());
                return StatusCode(500);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message, e.ToString());
                return StatusCode(500);
            }
        }
    }
}