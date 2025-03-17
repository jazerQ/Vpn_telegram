using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class inuserPaymentsidfrominttoGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TelegramUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Shortname = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VpnClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserPaymentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TelegramId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPayments_TelegramUser_TelegramId",
                        column: x => x.TelegramId,
                        principalTable: "TelegramUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    isPrimaryUser = table.Column<bool>(type: "boolean", nullable: false),
                    ConnectionString = table.Column<string>(type: "text", nullable: false)
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
                name: "IX_UserPayments_TelegramId",
                table: "UserPayments",
                column: "TelegramId",
                unique: true);

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
                name: "UserPayments");

            migrationBuilder.DropTable(
                name: "VpnClient");

            migrationBuilder.DropTable(
                name: "TelegramUser");
        }
    }
}
