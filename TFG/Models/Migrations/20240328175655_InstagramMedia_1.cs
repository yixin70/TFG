﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG.Migrations
{
    /// <inheritdoc />
    public partial class InstagramMedia_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InstagramLogs",
                table: "InstagramLogs");

            migrationBuilder.RenameTable(
                name: "InstagramLogs",
                newName: "InstagramLog");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstagramLog",
                table: "InstagramLog",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InstagramMedia",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstagramMedia", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstagramMedia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstagramLog",
                table: "InstagramLog");

            migrationBuilder.RenameTable(
                name: "InstagramLog",
                newName: "InstagramLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstagramLogs",
                table: "InstagramLogs",
                column: "Id");
        }
    }
}
