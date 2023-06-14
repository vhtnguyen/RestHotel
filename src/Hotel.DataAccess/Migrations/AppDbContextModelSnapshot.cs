﻿// <auto-generated />
using System;
using Hotel.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hotel.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hotel.DataAccess.Entities.HotelService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("HotelService");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("DownPayment")
                        .HasColumnType("float");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameCus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalSum")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.InvoiceHotelService", b =>
                {
                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("HotelServiceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime2");

                    b.HasKey("InvoiceId", "HotelServiceId");

                    b.HasIndex("HotelServiceId");

                    b.ToTable("InvoiceHotelService");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.ReservationCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ArrivalDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<int?>("RoomRegulationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("RoomId");

                    b.HasIndex("RoomRegulationId");

                    b.ToTable("ReservationCard");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NameType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoomDetailId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoomDetailId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.RoomDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("RoomRegulationId")
                        .HasColumnType("int");

                    b.Property<string>("RoomType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoomRegulationId");

                    b.ToTable("RoomDetail");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.RoomRegulation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DefaultGuest")
                        .HasColumnType("int");

                    b.Property<int>("MaxGuest")
                        .HasColumnType("int");

                    b.Property<double>("MaxOverseaSurchargeRatio")
                        .HasColumnType("float");

                    b.Property<double>("MaxSurchargeRatio")
                        .HasColumnType("float");

                    b.Property<double>("RoomExchangeFee")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("RoomRegulation");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.ServiceCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ServiceCategory");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Account")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.HotelService", b =>
                {
                    b.HasOne("Hotel.DataAccess.Entities.ServiceCategory", "Category")
                        .WithMany("HotelServices")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.InvoiceHotelService", b =>
                {
                    b.HasOne("Hotel.DataAccess.Entities.HotelService", "HotelService")
                        .WithMany("Invoices")
                        .HasForeignKey("HotelServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hotel.DataAccess.Entities.Invoice", "Invoice")
                        .WithMany("HotelServices")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HotelService");

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.ReservationCard", b =>
                {
                    b.HasOne("Hotel.DataAccess.Entities.Invoice", "Invoice")
                        .WithMany("ReservationCards")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("Hotel.DataAccess.Entities.Room", "Room")
                        .WithMany("ReservationCards")
                        .HasForeignKey("RoomId");

                    b.HasOne("Hotel.DataAccess.Entities.RoomRegulation", "RoomRegulation")
                        .WithMany()
                        .HasForeignKey("RoomRegulationId");

                    b.OwnsMany("Hotel.DataAccess.ObjectValues.Guest", "Guests", b1 =>
                        {
                            b1.Property<int>("ReservationCardId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Address")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PersonIdentification")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("TelephoneNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Type")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ReservationCardId", "Id");

                            b1.ToTable("Guest");

                            b1.WithOwner()
                                .HasForeignKey("ReservationCardId");
                        });

                    b.Navigation("Guests");

                    b.Navigation("Invoice");

                    b.Navigation("Room");

                    b.Navigation("RoomRegulation");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.Room", b =>
                {
                    b.HasOne("Hotel.DataAccess.Entities.RoomDetail", "RoomDetail")
                        .WithMany()
                        .HasForeignKey("RoomDetailId");

                    b.Navigation("RoomDetail");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.RoomDetail", b =>
                {
                    b.HasOne("Hotel.DataAccess.Entities.RoomRegulation", "RoomRegulation")
                        .WithMany()
                        .HasForeignKey("RoomRegulationId");

                    b.Navigation("RoomRegulation");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.User", b =>
                {
                    b.HasOne("Hotel.DataAccess.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.HotelService", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.Invoice", b =>
                {
                    b.Navigation("HotelServices");

                    b.Navigation("ReservationCards");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.Room", b =>
                {
                    b.Navigation("ReservationCards");
                });

            modelBuilder.Entity("Hotel.DataAccess.Entities.ServiceCategory", b =>
                {
                    b.Navigation("HotelServices");
                });
#pragma warning restore 612, 618
        }
    }
}
