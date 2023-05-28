using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHotelServiceInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelServiceInvoice");

            migrationBuilder.AddColumn<double>(
                name: "RoomExchangeFee",
                table: "RoomRegulation",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CatagoryId",
                table: "HotelService",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InvoiceHotelService",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    HotelServiceId = table.Column<int>(type: "int", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceHotelService", x => new { x.InvoiceId, x.HotelServiceId });
                    table.ForeignKey(
                        name: "FK_InvoiceHotelService_HotelService_HotelServiceId",
                        column: x => x.HotelServiceId,
                        principalTable: "HotelService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceHotelService_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCatagory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCatagory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelService_CatagoryId",
                table: "HotelService",
                column: "CatagoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHotelService_HotelServiceId",
                table: "InvoiceHotelService",
                column: "HotelServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelService_ServiceCatagory_CatagoryId",
                table: "HotelService",
                column: "CatagoryId",
                principalTable: "ServiceCatagory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelService_ServiceCatagory_CatagoryId",
                table: "HotelService");

            migrationBuilder.DropTable(
                name: "InvoiceHotelService");

            migrationBuilder.DropTable(
                name: "ServiceCatagory");

            migrationBuilder.DropIndex(
                name: "IX_HotelService_CatagoryId",
                table: "HotelService");

            migrationBuilder.DropColumn(
                name: "RoomExchangeFee",
                table: "RoomRegulation");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "CatagoryId",
                table: "HotelService");

            migrationBuilder.CreateTable(
                name: "HotelServiceInvoice",
                columns: table => new
                {
                    HotelServicesId = table.Column<int>(type: "int", nullable: false),
                    InvoicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelServiceInvoice", x => new { x.HotelServicesId, x.InvoicesId });
                    table.ForeignKey(
                        name: "FK_HotelServiceInvoice_HotelService_HotelServicesId",
                        column: x => x.HotelServicesId,
                        principalTable: "HotelService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelServiceInvoice_Invoice_InvoicesId",
                        column: x => x.InvoicesId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelServiceInvoice_InvoicesId",
                table: "HotelServiceInvoice",
                column: "InvoicesId");
        }
    }
}
