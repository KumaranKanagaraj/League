using Dashboard.DbContexts;
using Dashboard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Repositories
{
	public interface IUnitOfWork
	{
		IRepository<Team> TeamRepository { get; }
		IRepository<Fixture> FixtureRepository { get; }
		IRepository<FixtureResult> FixtureResultRepository { get; }
		void SaveChanges();
	}
	public class UnitOfWork : IUnitOfWork
	{
		private DashboardContext context;

		public UnitOfWork(DashboardContext context)
		{
			this.context = context;
		}

		private IRepository<Team> teamRepository;
		public IRepository<Team> TeamRepository
		{
			get
			{
				if (teamRepository == null)
				{
					teamRepository = new TeamRepository(context);
				}

				return teamRepository;
			}
		}

		private IRepository<Fixture> fixtureRepository;
		public IRepository<Fixture> FixtureRepository
		{
			get
			{
				if (fixtureRepository == null)
				{
					fixtureRepository = new FixtureRepository(context);
				}

				return fixtureRepository;
			}
		}

		private IRepository<FixtureResult> fixtureResultRepository;
		public IRepository<FixtureResult> FixtureResultRepository
		{
			get
			{
				if (fixtureResultRepository == null)
				{
					fixtureResultRepository = new FixtureResultRepository(context);
				}

				return fixtureResultRepository;
			}
		}

		public void SaveChanges()
		{
			context.SaveChanges();
		}
	}
}
