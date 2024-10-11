using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentEmployeeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dept_Id",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Dept_Id",
                table: "Employees",
                column: "Dept_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_Dept_Id",
                table: "Employees",
                column: "Dept_Id",
                principalTable: "Departments",
                principalColumn: "Department_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_Dept_Id",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Dept_Id",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Dept_Id",
                table: "Employees");
        }
    }
}
