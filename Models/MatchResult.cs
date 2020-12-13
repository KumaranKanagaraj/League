using System;

namespace Dashboard.Models
{
	public class MatchResult
	{
		public Guid TeamOneId { get; set; }
		public Guid TeamTwoId { get; set; }
		public Guid WinningTeamId { get; set; }
		public bool IsTie { get; set; }
	}
}
