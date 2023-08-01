using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIMCC.Migrations
{
    public partial class setDefaultRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[] { new Guid("4887ec13-b482-47b3-9b24-08db91a71770"), new DateTime(2023, 7, 31, 19, 56, 12, 845, DateTimeKind.Local).AddTicks(5951), new DateTime(2023, 7, 31, 19, 56, 12, 845, DateTimeKind.Local).AddTicks(5962), "Employee" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("4887ec13-b482-47b3-9b24-08db91a71770"));
        }
    }
}
