using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelService_ServiceCatagory_CatagoryId",
                table: "HotelService");

            migrationBuilder.DropTable(
                name: "ServiceCatagory");

            migrationBuilder.RenameColumn(
                name: "CatagoryId",
                table: "HotelService",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelService_CatagoryId",
                table: "HotelService",
                newName: "IX_HotelService_CategoryId");

            migrationBuilder.CreateTable(
                name: "ServiceCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategory", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_HotelService_ServiceCategory_CategoryId",
                table: "HotelService",
                column: "CategoryId",
                principalTable: "ServiceCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelService_ServiceCategory_CategoryId",
                table: "HotelService");

            migrationBuilder.DropTable(
                name: "ServiceCategory");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "HotelService",
                newName: "CatagoryId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelService_CategoryId",
                table: "HotelService",
                newName: "IX_HotelService_CatagoryId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_HotelService_ServiceCatagory_CatagoryId",
                table: "HotelService",
                column: "CatagoryId",
                principalTable: "ServiceCatagory",
                principalColumn: "Id");
        }
    }
}
