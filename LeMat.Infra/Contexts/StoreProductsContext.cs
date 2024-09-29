using System.Reflection;
using LeMat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeMat.Infra.Contexts;

public class SPContext : DbContext {
	public SPContext(DbContextOptions<SPContext> options) : base(options) { }

	public DbSet<Supplier> Suppliers { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Image> Images { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}