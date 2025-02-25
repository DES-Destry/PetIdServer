using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetIdServer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pet");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "pet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    role = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    password = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    address = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                schema: "pet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    sex = table.Column<bool>(type: "boolean", nullable: false),
                    is_castrated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    photo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pets", x => x.id);
                    table.ForeignKey(
                        name: "FK_pets_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "pet",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_contacts",
                schema: "pet",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    contact = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_contacts", x => new { x.user_id, x.contact_type });
                    table.ForeignKey(
                        name: "FK_user_contacts_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "pet",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "pet",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    hash_code = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    control_code = table.Column<long>(type: "bigint", nullable: false),
                    pet_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "tag_reports",
                schema: "pet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    corrupted_tag_id = table.Column<int>(type: "integer", nullable: false),
                    reporter_id = table.Column<Guid>(type: "uuid", maxLength: 32, nullable: false),
                    resolver_id = table.Column<Guid>(type: "uuid", maxLength: 32, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    resolved_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag_reports", x => x.id);
                    table.ForeignKey(
                        name: "FK_tag_reports_tags_corrupted_tag_id",
                        column: x => x.corrupted_tag_id,
                        principalSchema: "pet",
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tag_reports_users_UserEntityId",
                        column: x => x.UserEntityId,
                        principalSchema: "pet",
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tag_reports_users_reporter_id",
                        column: x => x.reporter_id,
                        principalSchema: "pet",
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tag_reports_users_resolver_id",
                        column: x => x.resolver_id,
                        principalSchema: "pet",
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_pets_user_id",
                schema: "pet",
                table: "pets",
                column: "user_id");

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
                name: "IX_tag_reports_UserEntityId",
                schema: "pet",
                table: "tag_reports",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_tags_pet_id",
                schema: "pet",
                table: "tags",
                column: "pet_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tag_reports",
                schema: "pet");

            migrationBuilder.DropTable(
                name: "user_contacts",
                schema: "pet");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "pet");

            migrationBuilder.DropTable(
                name: "pets",
                schema: "pet");

            migrationBuilder.DropTable(
                name: "users",
                schema: "pet");
        }
    }
}
