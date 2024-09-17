using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDBMS.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IMDbLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<bool>(type: "bit", nullable: false),
                    Adventure = table.Column<bool>(type: "bit", nullable: false),
                    Comedy = table.Column<bool>(type: "bit", nullable: false),
                    Drama = table.Column<bool>(type: "bit", nullable: false),
                    Romance = table.Column<bool>(type: "bit", nullable: false),
                    Thriller = table.Column<bool>(type: "bit", nullable: false),
                    ScienceFiction = table.Column<bool>(type: "bit", nullable: false),
                    Animation = table.Column<bool>(type: "bit", nullable: false),
                    Fantasy = table.Column<bool>(type: "bit", nullable: false),
                    Horror = table.Column<bool>(type: "bit", nullable: false),
                    Musical = table.Column<bool>(type: "bit", nullable: false),
                    Mystery = table.Column<bool>(type: "bit", nullable: false),
                    Documentary = table.Column<bool>(type: "bit", nullable: false),
                    War = table.Column<bool>(type: "bit", nullable: false),
                    Crime = table.Column<bool>(type: "bit", nullable: false),
                    Western = table.Column<bool>(type: "bit", nullable: false),
                    FilmNoir = table.Column<bool>(type: "bit", nullable: false),
                    Childrens = table.Column<bool>(type: "bit", nullable: false),
                    Other = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    MovieID = table.Column<int>(type: "int", nullable: false),
                    RatingID = table.Column<int>(type: "int", nullable: false),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.UserID, x.MovieID });
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieID",
                table: "Ratings",
                column: "MovieID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
