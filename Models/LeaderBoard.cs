using Newtonsoft.Json;
using System;

namespace Dashboard.Models
{
	public class LeaderBoard
	{
		public Guid Id { get; set; }
		[JsonProperty("team_name")]
		public string TeamName { get; set; }
		public int Wins { get; set; }
		public int Losses { get; set; }
		public int Ties { get; set; }
		public int Score { get; set; }
	}
}
