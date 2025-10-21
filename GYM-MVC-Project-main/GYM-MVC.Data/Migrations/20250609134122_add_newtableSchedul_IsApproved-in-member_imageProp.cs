using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GYM_MVC.Data.Migrations {

    /// <inheritdoc />
    public partial class add_newtableSchedul_IsApprovedinmember_imageProp : Migration {

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<string>(
                name: "GeneralInfo",
                table: "Workouts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InjuryInfo",
                table: "Workouts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropColumn(
                name: "GeneralInfo",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "InjuryInfo",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Members");
        }
    }
}