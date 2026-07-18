using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_REST_API.Migrations
{
    /// <inheritdoc />
    public partial class FixMemberDateColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemebershipDate",
                table: "Members",
                newName: "MembershipDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MembershipDate",
                table: "Members",
                newName: "MemebershipDate");
        }
    }
}
