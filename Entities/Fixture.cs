using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Entities
{
	public class Fixture
	{
		public Guid Id { get; set; }
		public Guid TeamOneId { get; set; }
		public Team TeamOne { get; set; }
		public Guid TeamTwoId { get; set; }
		public Team TeamTwo { get; set; }
		public FixtureResult FixtureResult { get; set; }
	}
}
