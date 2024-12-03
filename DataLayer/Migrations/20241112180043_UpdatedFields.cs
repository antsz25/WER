using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE Attachments ADD Base64TMP varbinary(max)");
            migrationBuilder.Sql(@"UPDATE Attachments SET Base64TMP = Convert(varbinary, Base64)");
            migrationBuilder.Sql(@"ALTER TABLE Attachments DROP COLUMN Base64");
            migrationBuilder.Sql(@"EXEC sp_rename 'Attachments.Base64TMP', 'Base64', 'COLUMN'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE Attachments ADD Base64TMP nvarchar(max)");
            migrationBuilder.Sql(@"UPDATE Attachments SET Base64TMP = Convert(nvarchar, Base64)");
            migrationBuilder.Sql(@"ALTER TABLE Attachments DROP COLUMN Base64");
            migrationBuilder.Sql(@"EXEC sp_rename 'Attachments.Base64TMP', 'Base64', 'COLUMN'");
        }
    }
}
