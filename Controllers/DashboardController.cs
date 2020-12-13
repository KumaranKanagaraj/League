using System;
using Dashboard.Entities;
using Dashboard.Models;
using Dashboard.Repositories;
using Dashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
	[Route("api/dashboard")]
	public class DashboardController : Controller
	{
		private readonly DashboardService service;

		public DashboardController(DashboardService service)
		{
			this.service = service;
		}

		[HttpGet, Route("summary")]
		public IActionResult Summary([FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 10, [FromQuery] string teamName = null)
		{
			return Json(service.LeagueSummary(pageNumber: pageNumber, pageSize: pageSize, teamName:teamName));
		}

		[HttpGet, Route("{name}/teams")]
		public IActionResult SearchTeams(string name)
		{
			return Json(service.SearchTeams(name));
		}

		[HttpGet, Route("all/team")]
		public IActionResult GetTeams()
		{
			return Json(service.GetTeams());
		}

		[HttpPost, Route("add/team/{name}")]
		public ActionResult<MatchResult> AddTeam(string name)
		{
			var team = service.AddTeam(name);
			return Ok(team);
		}

		[HttpPost("add/fixture")]
		public ActionResult<Fixture> AddFixture([FromBody] MatchResult result)
		{
			var fixture = service.AddFixture(result);
			return Ok(fixture);
		}

	}
}
