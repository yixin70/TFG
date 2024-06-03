using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG.Migrations
{
    /// <inheritdoc />
    public partial class Add_TwitterLog_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TwitterLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TweetId = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Text = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateTweeted = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsSuspicious = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TwitterLog", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TwitterLog");
        }
    }
}
