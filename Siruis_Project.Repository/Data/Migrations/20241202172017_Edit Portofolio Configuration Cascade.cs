using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siruis_Project.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditPortofolioConfigurationCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portos_Clients_CLient_Id",
                table: "Portos");

            migrationBuilder.DropForeignKey(
                name: "FK_Portos_Industries_Industry_Id",
                table: "Portos");

            migrationBuilder.AlterColumn<string>(
                name: "Govern",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Portos_Clients_CLient_Id",
                table: "Portos",
                column: "CLient_Id",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Portos_Industries_Industry_Id",
                table: "Portos",
                column: "Industry_Id",
                principalTable: "Industries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portos_Clients_CLient_Id",
                table: "Portos");

            migrationBuilder.DropForeignKey(
                name: "FK_Portos_Industries_Industry_Id",
                table: "Portos");

            migrationBuilder.AlterColumn<string>(
                name: "Govern",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Portos_Clients_CLient_Id",
                table: "Portos",
                column: "CLient_Id",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Portos_Industries_Industry_Id",
                table: "Portos",
                column: "Industry_Id",
                principalTable: "Industries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
