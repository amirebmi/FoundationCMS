using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundationCMS.Migrations
{
    public partial class InitialSchema4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventDonorId",
                table: "Donors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donors_EventDonorId",
                table: "Donors",
                column: "EventDonorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_EventDonors_EventDonorId",
                table: "Donors",
                column: "EventDonorId",
                principalTable: "EventDonors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donors_EventDonors_EventDonorId",
                table: "Donors");

            migrationBuilder.DropIndex(
                name: "IX_Donors_EventDonorId",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "EventDonorId",
                table: "Donors");
        }
    }
}
