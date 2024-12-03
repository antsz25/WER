using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationshipEnterpriseWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Works_EnterpriseId",
                table: "Works");

            migrationBuilder.CreateIndex(
                name: "IX_Works_EnterpriseId",
                table: "Works",
                column: "EnterpriseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Works_EnterpriseId",
                table: "Works");

            migrationBuilder.CreateIndex(
                name: "IX_Works_EnterpriseId",
                table: "Works",
                column: "EnterpriseId",
                unique: true);
        }
    }
}
