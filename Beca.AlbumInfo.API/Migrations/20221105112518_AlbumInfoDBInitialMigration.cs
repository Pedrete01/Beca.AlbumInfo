using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beca.AlbumInfo.API.Migrations
{
    public partial class AlbumInfoDBInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albumes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Canciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    AlbumId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canciones_Albumes_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Albumes",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { 1, "Album de Imagine Dragons.", "Evolve" });

            migrationBuilder.InsertData(
                table: "Albumes",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { 2, "Album de AJR.", "OK Orchestra" });

            migrationBuilder.InsertData(
                table: "Albumes",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { 3, "Album de Kaleo.", "Surface sounds" });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "AlbumId", "Description", "Title" },
                values: new object[] { 1, 1, "Pain!\r\nYou made me a, you made me a believer, believer\r\nPain!\r\nYou break me down, and build me up, believer, believer", "Believer" });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "AlbumId", "Description", "Title" },
                values: new object[] { 2, 1, "Whatever it takes\r\n'Cause I love the adrenaline in my veins\r\nI do whatever it takes\r\n'Cause I love how it feels when I break the chains\r\nWhatever it takes", "Whatever it takes" });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "AlbumId", "Description", "Title" },
                values: new object[] { 3, 2, "So put your best face on, everybody\r\nPretend you know this song, everybody\r\nCome hang\r\nLet's go out with a bang (bang! Bang! Bang!)", "Bang!" });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "AlbumId", "Description", "Title" },
                values: new object[] { 4, 2, "It took a little while, but I found love (found love)\r\nI thought you'd reply, you just thumbed up (thumbed up)\r\nI play a lot of shows but you don't come (don't come)\r\nI don't even mind, this is so dumb, so dumb", "Joe" });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "AlbumId", "Description", "Title" },
                values: new object[] { 5, 3, "I want to break my baby\r\nYou know she loves to fake it\r\nI want to break my baby\r\nYeah, hold her down, bring her down now", "Break my baby" });

            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "Id", "AlbumId", "Description", "Title" },
                values: new object[] { 6, 3, "You've got to stay skinny, don't you, girl?\r\nYou've got to stay pretty while you can\r\nYou've got to stay hungry for the fans\r\nOr they'll try to burn you all out\r\nThey'll try to burn you all out, yeah", "Skinny" });

            migrationBuilder.CreateIndex(
                name: "IX_Canciones_AlbumId",
                table: "Canciones",
                column: "AlbumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Canciones");

            migrationBuilder.DropTable(
                name: "Albumes");
        }
    }
}
