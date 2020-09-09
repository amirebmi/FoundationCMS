using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundationCMS.Migrations
{
    public partial class InitialSchema2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventDonorId",
                table: "Contributions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_EventDonorId",
                table: "Contributions",
                column: "EventDonorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_EventDonors_EventDonorId",
                table: "Contributions",
                column: "EventDonorId",
                principalTable: "EventDonors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_EventDonors_EventDonorId",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_EventDonorId",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "EventDonorId",
                table: "Contributions");
        }
    }
}
