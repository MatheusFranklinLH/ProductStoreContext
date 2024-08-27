using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using StoreProductsContext.Domain.Entities;
using StoreProductsContext.Domain.Enums;

namespace StoreProductsContext.Infra.Contexts;

public class SPContext : DbContext {
	public SPContext(DbContextOptions<SPContext> options) : base(options) { }

	public DbSet<Supplier> Suppliers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}