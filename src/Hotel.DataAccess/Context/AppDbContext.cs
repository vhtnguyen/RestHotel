using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DataAccess.Context;

internal class AppDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	// datasets
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

	// configured

}
