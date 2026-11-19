using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEWebApp.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecallNotices",
                columns: table => new
                {
                    RECALL_NUMBER = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PRODUCT_CODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUCT_CODE_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    REGULATION_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUCT_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TRADE_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RECALL_CLASS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CENTER_CLASSIFICATION_DT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    POSTED_INTERNET_DT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TERMINATION_DT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FEI_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIRM_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MANUFACTURER_RECALL_REASON = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecallNotices", x => x.RECALL_NUMBER);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecallNotices");
        }
    }
}
