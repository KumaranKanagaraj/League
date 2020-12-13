using System;

namespace Dashboard.Models
{
	public class TeamSummary
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int MatchesPlayed { get; set; }
		public int Wins { get; set; }
		public int Losses { get; set; }
		public int Ties { get; set; }
		public int Scores { get; set; }

	}
}
