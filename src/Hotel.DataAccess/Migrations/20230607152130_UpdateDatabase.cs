using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomDetail_RoomDetailId",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "RoomDetailId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomDetail_RoomDetailId",
                table: "Room",
                column: "RoomDetailId",
                principalTable: "RoomDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomDetail_RoomDetailId",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "RoomDetailId",
                table: "Room",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomDetail_RoomDetailId",
                table: "Room",
                column: "RoomDetailId",
                principalTable: "RoomDetail",
                principalColumn: "Id");
        }
    }
}
