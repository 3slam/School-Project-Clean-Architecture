using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
            table: "Departments",
           columns: new[] { "DepartmentId", "DeptName", "DeptDesc", "DeptLocation", "DeptManager", "ManagerHiredate" },
            values: new object[] { 1, "Computer Science", "Department of Computer Science", "Building A", 1, new DateTime(2020, 1, 15) });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DeptName", "DeptDesc", "DeptLocation", "DeptManager", "ManagerHiredate" },
                values: new object[] { 2, "Mathematics", "Department of Mathematics", "Building B", 2, new DateTime(2019, 8, 20) });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
