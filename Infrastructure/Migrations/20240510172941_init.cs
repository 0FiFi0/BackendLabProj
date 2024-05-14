using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    country_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ranking_system",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    system_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ranking_system", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "university",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    country_id = table.Column<int>(type: "int", nullable: true),
                    university_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_university", x => x.id);
                    table.ForeignKey(
                        name: "fk_uni_cnt",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ranking_criteria",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    ranking_system_id = table.Column<int>(type: "int", nullable: true),
                    criteria_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ranking_criteria", x => x.id);
                    table.ForeignKey(
                        name: "fk_rc_rs",
                        column: x => x.ranking_system_id,
                        principalTable: "ranking_system",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "university_year",
                columns: table => new
                {
                    university_id = table.Column<int>(type: "int", nullable: true),
                    year = table.Column<int>(type: "int", nullable: true),
                    num_students = table.Column<int>(type: "int", nullable: true),
                    student_staff_ratio = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    pct_international_students = table.Column<int>(type: "int", nullable: true),
                    pct_female_students = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_uy_uni",
                        column: x => x.university_id,
                        principalTable: "university",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "university_ranking_year",
                columns: table => new
                {
                    university_id = table.Column<int>(type: "int", nullable: true),
                    ranking_criteria_id = table.Column<int>(type: "int", nullable: true),
                    year = table.Column<int>(type: "int", nullable: true),
                    score = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_ury_rc",
                        column: x => x.ranking_criteria_id,
                        principalTable: "ranking_criteria",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_ury_uni",
                        column: x => x.university_id,
                        principalTable: "university",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ranking_criteria_ranking_system_id",
                table: "ranking_criteria",
                column: "ranking_system_id");

            migrationBuilder.CreateIndex(
                name: "IX_university_country_id",
                table: "university",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_university_ranking_year_ranking_criteria_id",
                table: "university_ranking_year",
                column: "ranking_criteria_id");

            migrationBuilder.CreateIndex(
                name: "IX_university_ranking_year_university_id",
                table: "university_ranking_year",
                column: "university_id");

            migrationBuilder.CreateIndex(
                name: "IX_university_year_university_id",
                table: "university_year",
                column: "university_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "university_ranking_year");

            migrationBuilder.DropTable(
                name: "university_year");

            migrationBuilder.DropTable(
                name: "ranking_criteria");

            migrationBuilder.DropTable(
                name: "university");

            migrationBuilder.DropTable(
                name: "ranking_system");

            migrationBuilder.DropTable(
                name: "country");
        }
    }
}
