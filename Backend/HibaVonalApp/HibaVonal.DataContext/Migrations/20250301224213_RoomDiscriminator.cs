using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HibaVonal.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class RoomDiscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Room",
                newName: "RoomType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomType",
                table: "Room",
                newName: "Discriminator");
        }
    }
}
