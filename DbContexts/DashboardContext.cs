using Dashboard.Entities;
using Dashboard.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dashboard.DbContexts
{
	public class DashboardContext : DbContext
	{
		// add-migration MyFirstMigration 
		// Update-Database 
		public DashboardContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Team> Teams { get; set; }
		public DbSet<Fixture> Fixtures { get; set; }
		public DbSet<FixtureResult> FixtureResults { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new FixtureConfiguration());
			modelBuilder.ApplyConfiguration(new FixtureResultConfiguration());

			modelBuilder
				.Entity<Team>()
				.HasData(BuildTeamModel());
		}

		private List<Team> BuildTeamModel()
		{
			var teams = new List<Team>();

			string filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "DemoData", "Leaderboard_Initial_Dataset65148c7.json");
			var json = File.ReadAllText(filePath);
			var leaderBoards = JsonConvert.DeserializeObject<List<LeaderBoard>>(json);
			foreach (var item in leaderBoards)
			{
				if (teams.All(x => x.Name != item.TeamName))
				{
					teams.Add(new Team
					{
						Id = Guid.NewGuid(),
						Name = item.TeamName
					});
				}
			}

			return teams;
		}
	}
}
