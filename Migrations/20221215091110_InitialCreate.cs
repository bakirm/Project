using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    slug = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    createdAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    createdAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    PostId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    PostId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => new { x.Text, x.PostId });
                    table.ForeignKey(
                        name: "FK_Tags_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Body", "Description", "Title", "createdAt", "slug", "updatedAt" },
                values: new object[,]
                {
                    { 1, "The app is simple to use, and will help you decide on your best furniture fit.", "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.", "Augmented Reality iOS Application", new DateTime(2022, 12, 15, 10, 11, 9, 964, DateTimeKind.Local).AddTicks(915), "augmented-reality-ios-application", new DateTime(2022, 12, 15, 10, 11, 9, 964, DateTimeKind.Local).AddTicks(948) },
                    { 2, "The app is simple to use, and will help you decide on your best furniture fit.", "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.", "Augmented Reality iOS Application", new DateTime(2022, 12, 15, 10, 11, 9, 964, DateTimeKind.Local).AddTicks(951), "augmented-reality-ios-application-2", new DateTime(2022, 12, 15, 10, 11, 9, 964, DateTimeKind.Local).AddTicks(953) }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Body", "PostId", "createdAt", "updatedAt" },
                values: new object[] { 1, "Great blog.", 1, new DateTime(2022, 12, 15, 10, 11, 9, 964, DateTimeKind.Local).AddTicks(1089), new DateTime(2022, 12, 15, 10, 11, 9, 964, DateTimeKind.Local).AddTicks(1092) });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "PostId", "Text" },
                values: new object[,]
                {
                    { 1, "AR" },
                    { 2, "AR" },
                    { 2, "Gazzda" },
                    { 1, "iOS" },
                    { 2, "iOS" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_slug",
                table: "Posts",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PostId",
                table: "Tags",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
