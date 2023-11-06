using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WhoWantsToBeAMillionaire_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.question_id);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    score_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.score_id);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    answer_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    answer_content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => new { x.answer_id, x.question_id });
                    table.ForeignKey(
                        name: "FK_Answers_Questions_question_id",
                        column: x => x.question_id,
                        principalTable: "Questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    score_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_Users_Scores_score_id",
                        column: x => x.score_id,
                        principalTable: "Scores",
                        principalColumn: "score_id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false),
                    role_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.role_id);
                    table.ForeignKey(
                        name: "FK_Roles_Users_role_id",
                        column: x => x.role_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "question_id", "question_content" },
                values: new object[,]
                {
                    { 1, "Manchester is ?" },
                    { 2, "Những thông tin này nói về ai ? BMW, showroom, Nhật Bản, Du học sinh, giấy phép lái xe B2." }
                });

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "score_id", "score", "user_id" },
                values: new object[,]
                {
                    { 1, 10, 1 },
                    { 2, 11, 2 },
                    { 3, 30, 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "user_id", "name", "password", "role_id", "score_id", "username" },
                values: new object[,]
                {
                    { 1, "Dang Nguyen Khanh Vu", "vu1234!", 1, null, "vu" },
                    { 2, "Le Anh Quan", "quan1234!", 2, null, "quanla" },
                    { 3, "Huynh Hieu Thai", "thai1234!", 2, null, "thaihh" },
                    { 4, "Pham Anh Quan", "quanfpt1234!", 2, null, "quanpa" }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "answer_id", "question_id", "answer_content", "isCorrect" },
                values: new object[,]
                {
                    { "A", 1, "Red", true },
                    { "A", 2, "Lê Quân", false },
                    { "B", 1, "Not Blue", false },
                    { "B", 2, "Lê Anh Quân", false },
                    { "C", 1, "A", false },
                    { "C", 2, "Lê Quang", true },
                    { "D", 1, "C", false },
                    { "D", 2, "Lê Quâng", false }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "role_id", "role_name" },
                values: new object[,]
                {
                    { 1, "Adminstrator" },
                    { 2, "Player" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_question_id",
                table: "Answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_score_id",
                table: "Users",
                column: "score_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Scores");
        }
    }
}
