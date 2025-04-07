using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HibaVonal.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class UserRoomId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Room_RoomId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_RoomId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoomId",
                table: "User",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Room_RoomId",
                table: "User",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
