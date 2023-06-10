using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DataAccess.Context;

internal class AppDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
<<<<<<< HEAD
	public DbSet<RoomRegulation> RoomRegulation {get;set;}
	// datasets
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
=======
    public DbSet<Role> Role { get; set; }
    public DbSet<HotelService> HotelService { get; set; }
    public DbSet<ServiceCategory> ServiceCategory { get; set; }
    // datasets
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
>>>>>>> Nguyen

	// configured
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		var configuration = new EntityConfiguration();

		modelBuilder.ApplyConfiguration<User>(configuration);
        modelBuilder.ApplyConfiguration<HotelService>(configuration);
        modelBuilder.ApplyConfiguration<ReservationCard>(configuration);
        modelBuilder.ApplyConfiguration<Role>(configuration);
        modelBuilder.ApplyConfiguration<Room>(configuration);
        modelBuilder.ApplyConfiguration<RoomRegulation>(configuration);
		modelBuilder.ApplyConfiguration<InvoiceHotelService>(configuration);
		modelBuilder.ApplyConfiguration<ServiceCategory>(configuration);
		modelBuilder.ApplyConfiguration<Invoice>(configuration);
		modelBuilder.ApplyConfiguration<RoomDetail>(configuration);
        modelBuilder.ApplyConfiguration<RoomRegulationRoomDetail>(configuration);
    }

}
