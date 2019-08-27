using System;
using System.Collections.Generic;
using LeagueViewer.Entities;
using LeagueViewer.Repository;
using LeagueViewer.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LeagueViewer.Api.Controllers
{
    [Route("api/Teams")]
    [ApiController]
    [EnableCors("MyPolicity")]
    public class TeamController : ControllerBase
    {
        private ITeamService teamService;
        public TeamController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        [HttpGet]
        [Route("{leagueId}")]
        public ActionResult Get(int leagueId)
        {
            try
            {
                IList<Team> teams = teamService.GetTeamsWithLeagueId(leagueId);
                return Ok(teams);
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e.ToString());
                return StatusCode(500);
            }
        }
    }
}