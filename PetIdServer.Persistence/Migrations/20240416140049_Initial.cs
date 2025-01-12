using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PetIdServer.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pet");

            migrationBuilder.CreateTable(
                name: "admins",
                schema: "pet",
                columns: table => new
                {
                    username = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    password = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    password_last_changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "owners",
                schema: "pet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    address = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owners", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "owners_contacts",
                schema: "pet",
                columns: table => new
                {
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_type = table.Column<string>(type: "text", nullable: false),
                    contact = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owners_contacts", x => new { x.owner_id, x.contact_type });
                    table.ForeignKey(
                        name: "FK_owners_contacts_owners_owner_id",
                        column: x => x.owner_id,
                        principalSchema: "pet",
                        principalTable: "owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                schema: "pet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    sex = table.Column<bool>(type: "boolean", nullable: false),
                    is_castrated = table.Column<bool>(type: "boolean", nullable: false),
                    photo = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pets", x => x.id);
                    table.ForeignKey(
                        name: "FK_pets_owners_owner_id",
                        column: x => x.owner_id,
                        principalSchema: "pet",
                        principalTable: "owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "pet",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    control_code = table.Column<long>(type: "bigint", nullable: false),
                    pet_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pet_added_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_scanned_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                    table.ForeignKey(
                        name: "FK_tags_pets_pet_id",
                        column: x => x.pet_id,
                        principalSchema: "pet",
                        principalTable: "pets",
                        principalColumn: "id");
                });

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

            migrationBuilder.InsertData(
                schema: "pet",
                table: "admins",
                columns: new[] { "username", "created_at", "password", "password_last_changed_at" },
                values: new object[] { "Andrey.Kirik", new DateTime(2024, 4, 16, 14, 0, 49, 628, DateTimeKind.Utc).AddTicks(4440), null, null });

            migrationBuilder.CreateIndex(
                name: "IX_pets_owner_id",
                schema: "pet",
                table: "pets",
                column: "owner_id");

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

            migrationBuilder.CreateIndex(
                name: "IX_tags_pet_id",
                schema: "pet",
                table: "tags",
                column: "pet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "owners_contacts",
                schema: "pet");

            migrationBuilder.DropTable(
                name: "tag_reports",
                schema: "pet");

            migrationBuilder.DropTable(
                name: "admins",
                schema: "pet");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "pet");

            migrationBuilder.DropTable(
                name: "pets",
                schema: "pet");

            migrationBuilder.DropTable(
                name: "owners",
                schema: "pet");
        }
    }
}
