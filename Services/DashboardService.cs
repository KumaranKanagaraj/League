using Dashboard.Entities;
using Dashboard.Models;
using Dashboard.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashboard.Services
{
	public class DashboardService
	{
		private readonly IUnitOfWork unitOfWork;
		public DashboardService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public IEnumerable<Team> SearchTeams(string name)
		{
			return unitOfWork.TeamRepository.All()
				.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
		}

		public IEnumerable<Team> GetTeams()
		{
			return unitOfWork.TeamRepository.All();
		}

		public Team AddTeam(string name)
		{
			var team = new Team
			{
				Id = Guid.NewGuid(),
				Name = name
			};
			unitOfWork.TeamRepository.Add(team);
			unitOfWork.SaveChanges();
			return team;
		}

		public Fixture AddFixture(MatchResult result)
		{
			var fixtureId = Guid.NewGuid();
			var fixture = new Fixture
			{
				Id = fixtureId,
				TeamOneId = result.TeamOneId,
				TeamTwoId = result.TeamTwoId,
				FixtureResult = new FixtureResult
				{
					Id = Guid.NewGuid(),
					WinnerId = !(result.IsTie) ? result.WinningTeamId : result.TeamOneId,
					IsMatchTied = result.IsTie
				}
			};
			unitOfWork.FixtureRepository.Add(fixture);
			unitOfWork.SaveChanges();
			return fixture;
		}

		public object LeagueSummary(int pageNumber, int pageSize, string teamName)
		{
			IEnumerable<Team> teams;
			if (!string.IsNullOrWhiteSpace(teamName))
			{
				teams = unitOfWork.TeamRepository.Find(item => item.Name.ToLower().IndexOf(teamName.ToLower()) >= 0);
			}
			else
			{
				teams = unitOfWork.TeamRepository.All();
			}
			var summary = new Dictionary<Guid, TeamSummary>();
			foreach (var item in teams)
			{
				summary.Add(item.Id,
					new TeamSummary
					{
						Id = item.Id,
						Name = item.Name
					});
			}

			var fixtures = unitOfWork
				.FixtureRepository
				.All();


			foreach (var item in fixtures)
			{
				if (!string.IsNullOrWhiteSpace(teamName))
				{
					if (item.TeamOne.Name.Contains(teamName, StringComparison.OrdinalIgnoreCase))
					{
						UpdateTeamSummary(summary[item.TeamOneId], item);
					}
					if (item.TeamTwo.Name.Contains(teamName, StringComparison.OrdinalIgnoreCase))
					{
						UpdateTeamSummary(summary[item.TeamTwoId], item);
					}
				}
				else
				{
					UpdateTeamSummary(summary[item.TeamOneId], item);
					UpdateTeamSummary(summary[item.TeamTwoId], item);
				}

			}

			var paginatedSummary = summary
				.Values
				.OrderByDescending(x => x.Scores)
				.ThenByDescending(x => x.MatchesPlayed)
				.Skip(pageSize * (pageNumber - 1))
				.Take(pageSize)
				.ToList();

			return new
			{
				currentPage = pageNumber,
				nextPage = pageNumber + 1,
				previousPage = pageNumber - 1,
				pageSize,
				totalPage = summary.Count / pageSize,
				summary = paginatedSummary
			};
		}

		private void UpdateTeamSummary(TeamSummary teamSummary, Fixture fixture)
		{
			teamSummary.MatchesPlayed += 1;

			if (fixture.FixtureResult.IsMatchTied)
			{
				teamSummary.Scores += 1;
				teamSummary.Ties += 1;
			}
			else if (fixture.FixtureResult.WinnerId == teamSummary.Id && !fixture.FixtureResult.IsMatchTied)
			{
				teamSummary.Wins += 1;
				teamSummary.Scores += 3;
			}
			else
			{
				teamSummary.Losses += 1;
			}
		}
	}
}
