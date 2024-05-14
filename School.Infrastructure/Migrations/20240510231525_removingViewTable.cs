using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace School.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removingViewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DeptDesc", "DeptLocation", "DeptManager", "DeptName", "ManagerHiredate" },
                values: new object[,]
                {
                    { 1, "Department of Computer Science", "Building A", 1, "Computer Science", new DateOnly(2020, 1, 15) },
                    { 2, "Department of Mathematics", "Building B", 2, "Mathematics", new DateOnly(2020, 1, 15) }
                });
        }
    }
}
