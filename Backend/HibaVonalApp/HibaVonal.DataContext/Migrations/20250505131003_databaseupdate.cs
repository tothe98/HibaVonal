using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HibaVonal.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class databaseupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_ErrorType_ErrorTypeId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLog_Room_RoomId",
                table: "ErrorLog");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLog_User_MaintenanceWorkerId",
                table: "ErrorLog");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLog_User_ReporterId",
                table: "ErrorLog");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "ErrorLog",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReporterId",
                table: "ErrorLog",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceWorkerId",
                table: "ErrorLog",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ErrorLog",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ErrorTypeId",
                table: "Equipment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_ErrorType_ErrorTypeId",
                table: "Equipment",
                column: "ErrorTypeId",
                principalTable: "ErrorType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLog_Room_RoomId",
                table: "ErrorLog",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLog_User_MaintenanceWorkerId",
                table: "ErrorLog",
                column: "MaintenanceWorkerId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLog_User_ReporterId",
                table: "ErrorLog",
                column: "ReporterId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_ErrorType_ErrorTypeId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLog_Room_RoomId",
                table: "ErrorLog");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLog_User_MaintenanceWorkerId",
                table: "ErrorLog");

            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLog_User_ReporterId",
                table: "ErrorLog");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "ErrorLog",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReporterId",
                table: "ErrorLog",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaintenanceWorkerId",
                table: "ErrorLog",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "ErrorLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ErrorTypeId",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_ErrorType_ErrorTypeId",
                table: "Equipment",
                column: "ErrorTypeId",
                principalTable: "ErrorType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLog_Room_RoomId",
                table: "ErrorLog",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLog_User_MaintenanceWorkerId",
                table: "ErrorLog",
                column: "MaintenanceWorkerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLog_User_ReporterId",
                table: "ErrorLog",
                column: "ReporterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
