namespace Infrastructure.Data;

using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

public class StoreContext: DbContext

{
	public StoreContext(DbContextOptions options) : base(options)
	{


	}

	public DbSet<Product> products { get; set; }

	protected override void OnModelCreating(ModelBuilder Builder)
	{
		base.OnModelCreating(Builder);
		Builder.ApplyConfigurationsFromAssembly(typeof(ProductConfig).Assembly);
	}

}
