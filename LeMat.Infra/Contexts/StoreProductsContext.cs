using System.Reflection;
using LeMat.Domain.Entities;
using LeMat.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace LeMat.Infra.Contexts;

public class SPContext : DbContext {
	public SPContext(DbContextOptions<SPContext> options) : base(options) { }

	public DbSet<Supplier> Suppliers { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}