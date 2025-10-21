using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYM_MVC.Data.Migrations {

    /// <inheritdoc />
    public partial class changerelationworkoutplanmembertooneToMany : Migration {

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropIndex(
                name: "IX_Workouts_MemberId",
                table: "Workouts");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_MemberId",
                table: "Workouts",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropIndex(
                name: "IX_Workouts_MemberId",
                table: "Workouts");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_MemberId",
                table: "Workouts",
                column: "MemberId",
                unique: true,
                filter: "[MemberId] IS NOT NULL");
        }
    }
}