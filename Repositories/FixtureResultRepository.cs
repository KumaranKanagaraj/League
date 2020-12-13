using Dashboard.DbContexts;
using Dashboard.Entities;
using System;

namespace Dashboard.Repositories
{
	public class FixtureResultRepository : GenericRepository<FixtureResult>
	{
		public FixtureResultRepository(DashboardContext context) : base(context)
		{
		}

		public override FixtureResult Get(Guid id)
		{
			return base.Get(id);
		}

		public override FixtureResult Add(FixtureResult fixtureResult)
		{
			return base.Add(fixtureResult);
		}
	}
}
