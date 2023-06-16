using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DataAccess.Context;

internal class AppDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	// datasets
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

	public DbSet<RoomRegulation> RoomRegulation {get;set;}

    public DbSet<Role> Role { get; set; }
    public DbSet<HotelService> HotelService { get; set; }
    public DbSet<ServiceCategory> ServiceCategory { get; set; }
    public DbSet<Invoice> Invoice { get; set; }
    public DbSet<ReservationCard> ReservationCard { get; set; }
    public DbSet<Room> Room {get; set;}
    public DbSet<InvoiceHotelService> InvoiceHotelService {get; set;}

    public DbSet<RoomDetail> roomDetails { get; set;}   
    // datasets

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
        //modelBuilder.ApplyConfiguration<RoomRegulationRoomDetail>(configuration);
    }

}
