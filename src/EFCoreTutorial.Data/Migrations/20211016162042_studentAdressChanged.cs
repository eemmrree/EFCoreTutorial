using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreTutorial.Data.Migrations
{
    public partial class studentAdressChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "student_adress_id_fk",
                schema: "dbo",
                table: "student_adresses");

            migrationBuilder.DropIndex(
                name: "IX_student_adresses_student_id",
                schema: "dbo",
                table: "student_adresses");

            migrationBuilder.DropColumn(
                name: "student_id",
                schema: "dbo",
                table: "student_adresses");

            migrationBuilder.AddColumn<int>(
                name: "address_id",
                schema: "dbo",
                table: "students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_students_address_id",
                schema: "dbo",
                table: "students",
                column: "address_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "student_adress_student_id_fk",
                schema: "dbo",
                table: "students",
                column: "address_id",
                principalSchema: "dbo",
                principalTable: "student_adresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "student_adress_student_id_fk",
                schema: "dbo",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_address_id",
                schema: "dbo",
                table: "students");

            migrationBuilder.DropColumn(
                name: "address_id",
                schema: "dbo",
                table: "students");

            migrationBuilder.AddColumn<int>(
                name: "student_id",
                schema: "dbo",
                table: "student_adresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_student_adresses_student_id",
                schema: "dbo",
                table: "student_adresses",
                column: "student_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "student_adress_id_fk",
                schema: "dbo",
                table: "student_adresses",
                column: "student_id",
                principalSchema: "dbo",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
