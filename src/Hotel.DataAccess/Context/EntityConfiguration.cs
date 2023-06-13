using Hotel.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.DataAccess.Context;

//https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
public class EntityConfiguration
    : IEntityTypeConfiguration<User>,
    IEntityTypeConfiguration<HotelService>,
    IEntityTypeConfiguration<Invoice>,
    IEntityTypeConfiguration<ReservationCard>,
    IEntityTypeConfiguration<Role>,
    IEntityTypeConfiguration<Room>,
    IEntityTypeConfiguration<RoomRegulation>,
    IEntityTypeConfiguration<InvoiceHotelService>,
    IEntityTypeConfiguration<ServiceCategory>,
    IEntityTypeConfiguration<RoomDetail>
   
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasMany(r => r.Roles).WithMany(u=>u.Users);
    }

    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasMany(b => b.ReservationCards);
        builder.HasMany(b => b.HotelServices);
    }

    public void Configure(EntityTypeBuilder<ReservationCard> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        builder.HasOne(b => b.Room);
        builder.HasOne(b => b.Invoice);
        // https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
        builder.OwnsMany(b => b.Guests);
        builder.HasOne(b => b.RoomRegulation);
    }

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasMany(b => b.Users).WithMany(r=>r.Roles);
    }

    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        builder.HasOne(b => b.RoomDetail);
        builder.HasMany(b => b.ReservationCards);
    }

    public void Configure(EntityTypeBuilder<RoomRegulation> builder)
    {
        builder.HasKey(b => b.Id);

        //builder.HasMany(b => b.RoomDetails);
    }

    public void Configure(EntityTypeBuilder<InvoiceHotelService> builder)
    {
        builder.HasKey(c => new { c.InvoiceId, c.HotelServiceId });
        builder.HasOne(c => c.HotelService)
                .WithMany(c => c.Invoices)
                .HasForeignKey(c => c.HotelServiceId);

        builder.HasOne(c => c.Invoice)
                .WithMany(c => c.HotelServices)
                .HasForeignKey(c => c.InvoiceId);
    }

    public void Configure(EntityTypeBuilder<ServiceCategory> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        builder.HasMany(c => c.HotelServices);
    }

    public void Configure(EntityTypeBuilder<RoomDetail> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        builder.HasOne(b => b.RoomRegulation);
    }

    //public void Configure(EntityTypeBuilder<RoomRegulationRoomDetail> builder)
    //{
    //    builder.HasKey(b => new { b.RoomDetailId, b.RoomRegulationId });
    //    builder.HasOne(b => b.RoomDetail)
    //        .WithMany(b => b.RoomRegulations)
    //        .HasForeignKey(b => b.RoomDetailId);

    //    builder.HasOne(b => b.RoomRegulation)
    //        .WithMany(b => b.RoomDetails)
    //        .HasForeignKey(b => b.RoomRegulationId);

    //}

    void IEntityTypeConfiguration<HotelService>.Configure(EntityTypeBuilder<HotelService> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        builder.HasMany(b => b.Invoices);
        builder.HasOne(b => b.Category);
    }
}
