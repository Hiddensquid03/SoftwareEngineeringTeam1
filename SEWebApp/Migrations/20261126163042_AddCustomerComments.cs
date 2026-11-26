using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerComments",
                columns: table => new
                {
                    CustomerCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffReportId = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerComments", x => x.CustomerCommentId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerComments");
        }
    }
}
