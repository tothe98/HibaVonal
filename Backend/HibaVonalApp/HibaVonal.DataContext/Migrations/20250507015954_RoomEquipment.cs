using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HibaVonal.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class RoomEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Room_RoomId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_RoomId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Equipment");

            migrationBuilder.CreateTable(
                name: "RoomEquipment",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomEquipment", x => new { x.RoomId, x.EquipmentId });
                    table.ForeignKey(
                        name: "FK_RoomEquipment_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomEquipment_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomEquipment_EquipmentId",
                table: "RoomEquipment",
                column: "EquipmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomEquipment");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Equipment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_RoomId",
                table: "Equipment",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Room_RoomId",
                table: "Equipment",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");
        }
    }
}
