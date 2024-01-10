using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetIdServer.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tag_reports",
                schema: "pet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    corrupted_tag_id = table.Column<int>(type: "integer", nullable: false),
                    reporter_id = table.Column<string>(type: "character varying(32)", nullable: false),
                    resolver_id = table.Column<string>(type: "character varying(32)", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    resolved_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag_reports", x => x.id);
                    table.ForeignKey(
                        name: "FK_tag_reports_admins_reporter_id",
                        column: x => x.reporter_id,
                        principalSchema: "pet",
                        principalTable: "admins",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tag_reports_admins_resolver_id",
                        column: x => x.resolver_id,
                        principalSchema: "pet",
                        principalTable: "admins",
                        principalColumn: "username");
                    table.ForeignKey(
                        name: "FK_tag_reports_tags_corrupted_tag_id",
                        column: x => x.corrupted_tag_id,
                        principalSchema: "pet",
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "pet",
                table: "admins",
                keyColumn: "username",
                keyValue: "Andrey.Kirik",
                column: "created_at",
                value: new DateTime(2024, 1, 10, 12, 1, 14, 983, DateTimeKind.Utc).AddTicks(750));

            migrationBuilder.CreateIndex(
                name: "IX_tag_reports_corrupted_tag_id",
                schema: "pet",
                table: "tag_reports",
                column: "corrupted_tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_reports_reporter_id",
                schema: "pet",
                table: "tag_reports",
                column: "reporter_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_reports_resolver_id",
                schema: "pet",
                table: "tag_reports",
                column: "resolver_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tag_reports",
                schema: "pet");

            migrationBuilder.UpdateData(
                schema: "pet",
                table: "admins",
                keyColumn: "username",
                keyValue: "Andrey.Kirik",
                column: "created_at",
                value: new DateTime(2023, 11, 29, 12, 30, 45, 372, DateTimeKind.Utc).AddTicks(950));
        }
    }
}
