using System;
using System.Collections.Generic;
using LeagueViewer.Entities;
using LeagueViewer.Repository;
using LeagueViewer.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LeagueViewer.Api.Controllers
{
    [Route("api/Leagues")]
    [ApiController]
    [EnableCors("MyPolicity")]
    public class LeagueController : ControllerBase
    {
        private ILeagueService leagueService;
        public LeagueController(ILeagueService leagueService)
        {
            this.leagueService = leagueService;
        }

        [HttpGet]
        public ActionResult GetLeagues()
        {
            try
            {
                IList<League> leagues = leagueService.GetLeagues();
                return Ok(leagues);
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

        [HttpGet]
        [Route("{leagueId}")]
        public ActionResult GetLeague(int leagueId)
        {
            try
            {
                League league = leagueService.GetLeague(leagueId);
                return Ok(league);
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