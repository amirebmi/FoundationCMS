using Microsoft.EntityFrameworkCore.Migrations;

namespace FoundationCMS.Migrations
{
    public partial class InitialSchema3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Donors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donors_EventId",
                table: "Donors",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_Events_EventId",
                table: "Donors",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donors_Events_EventId",
                table: "Donors");

            migrationBuilder.DropIndex(
                name: "IX_Donors_EventId",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Donors");

            migrationBuilder.AddColumn<int>(
                name: "EventDonorId",
                table: "Contributions",
                type: "int",
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
    }
}
