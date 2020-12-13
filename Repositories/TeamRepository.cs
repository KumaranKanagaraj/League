using Dashboard.DbContexts;
using Dashboard.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dashboard.Repositories
{
	public class TeamRepository : GenericRepository<Team>
	{
        public TeamRepository(DashboardContext context) : base(context)
        {
        }

		public override IEnumerable<Team> Find(Expression<Func<Team, bool>> predicate)
		{
			return base.Find(predicate);
		}

        public override Team Get(Guid id)
		{
			return base.Get(id);
		}

		public override Team Add(Team team)
        {
            return base.Add(team);
        }

        public override IEnumerable<Team> All()
        {
            return base.All();
        }
    }
}
