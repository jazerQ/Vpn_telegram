using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddVpnClientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VpnClientId",
                table: "TelegramUser",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "VpnClient",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    flow = table.Column<string>(type: "text", nullable: false),
                    telegramId = table.Column<long>(type: "bigint", nullable: false),
                    limitIp = table.Column<int>(type: "integer", nullable: false),
                    totalGB = table.Column<int>(type: "integer", nullable: false),
                    expiryTime = table.Column<long>(type: "bigint", nullable: false),
                    enable = table.Column<bool>(type: "boolean", nullable: false),
                    tgId = table.Column<string>(type: "text", nullable: false),
                    subId = table.Column<string>(type: "text", nullable: false),
                    reset = table.Column<int>(type: "integer", nullable: false),
                    isPrimaryUser = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnClient", x => x.id);
                    table.ForeignKey(
                        name: "FK_VpnClient_TelegramUser_telegramId",
                        column: x => x.telegramId,
                        principalTable: "TelegramUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VpnClient_telegramId",
                table: "VpnClient",
                column: "telegramId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VpnClient");

            migrationBuilder.DropColumn(
                name: "VpnClientId",
                table: "TelegramUser");
        }
    }
}
