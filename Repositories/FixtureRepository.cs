using Dashboard.DbContexts;
using Dashboard.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashboard.Repositories
{
	public class FixtureRepository : GenericRepository<Fixture>
	{
		public FixtureRepository(DashboardContext context) : base(context)
		{
		}

		public override IEnumerable<Fixture> All()
		{
			return context
				.Fixtures
				.Include(x => x.FixtureResult)
				.Include(x => x.TeamOne)
				.Include(x => x.TeamTwo);
		}

		public override Fixture Get(Guid id)
		{
			return context
				.Fixtures
				.Include(x => x.FixtureResult)
				.FirstOrDefault(x => x.Id == id);
		}

		public override Fixture Add(Fixture fixture)
		{
			return base.Add(fixture);
		}
	}
}
