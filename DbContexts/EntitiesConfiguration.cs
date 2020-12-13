using Dashboard.Entities;
using Dashboard.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dashboard.DbContexts
{
	public class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
	{
		public void Configure(EntityTypeBuilder<Fixture> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.HasOne(x => x.TeamOne)
				.WithOne()
				.HasForeignKey<Fixture>(x => x.TeamOneId)
				.IsRequired(true);

			builder
				.HasOne(x => x.TeamTwo)
				.WithOne()
				.HasForeignKey<Fixture>(x => x.TeamTwoId)
				.IsRequired(true);

			builder
				.HasOne(x => x.FixtureResult)
				.WithOne(x => x.Fixture)
				.HasForeignKey<FixtureResult>(x => x.FixtureId)
				.IsRequired(true);

			builder
				.HasIndex(x => x.TeamOneId)
				.IsUnique(false);

			builder
				.HasIndex(x => x.TeamTwoId)
				.IsUnique(false);
		}
	}

	public class FixtureResultConfiguration : IEntityTypeConfiguration<FixtureResult>
	{
		public void Configure(EntityTypeBuilder<FixtureResult> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.HasOne(x => x.WinnerTeam)
				.WithMany()
				.HasForeignKey(x => x.WinnerId)
				.IsRequired(false);

			builder
				.Property(x => x.IsMatchTied)
				.HasDefaultValue(false)
				.IsRequired(true);
		}
	}
}
