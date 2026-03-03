namespace Infrastructure.Data;

using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

public class StoreContext: DbContext

{
	public StoreContext(DbContextOptions options) : base(options)
	{

	}

	DbSet<Product> products { get; set; };
}
