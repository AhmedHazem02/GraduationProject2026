using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G_P2026.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCvFileToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CvUrl",
                table: "AspNetUsers",
                newName: "CvFileName");

            migrationBuilder.AddColumn<byte[]>(
                name: "CvFile",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvFile",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CvFileName",
                table: "AspNetUsers",
                newName: "CvUrl");
        }
    }
}
