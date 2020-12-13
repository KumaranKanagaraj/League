using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Entities
{
	public class FixtureResult
	{
		public Guid Id { get; set; }
		public Guid FixtureId { get; set; }
		public Fixture Fixture { get; set; }
		public Guid WinnerId { get; set; }
		public Team WinnerTeam { get; set; }
		public bool IsMatchTied { get; set; }
	}
}
