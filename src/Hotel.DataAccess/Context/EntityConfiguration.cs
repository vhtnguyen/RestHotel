using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.DataAccess.Context;

public class EntityConfiguration
    : IEntityTypeConfiguration<User>,
    IEntityTypeConfiguration<HotelService>,
    IEntityTypeConfiguration<Invoice>,
    IEntityTypeConfiguration<ReservationCard>,
    IEntityTypeConfiguration<Role>,
    IEntityTypeConfiguration<Room>,
    IEntityTypeConfiguration<RoomRegulation>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(r => r.Roles);
    }

    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(b => b.ReservationCards);
        builder.HasMany(b => b.HotelServices);
    }

    public void Configure(EntityTypeBuilder<ReservationCard> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasOne(b => b.Room);
        builder.HasOne(b => b.Invoice);
        // https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
        builder.OwnsMany(b => b.Guests);
    }

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasMany(b => b.Users);
    }

    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasOne(b => b.RoomRegulation);
        builder.HasMany(b => b.ReservationCards);
    }

    public void Configure(EntityTypeBuilder<RoomRegulation> builder)
    {
        builder.HasKey(b =>b.Id);
        builder.HasMany(b => b.Rooms);
    }

    void IEntityTypeConfiguration<HotelService>.Configure(EntityTypeBuilder<HotelService> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasMany(b => b.Invoices);
    }
}
