using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class Transaction_Group_Representant_And_File_Cascading_Deleting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Files_FileId",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "JsonGroupRepresentant",
                table: "TransactionGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Files_FileId",
                table: "Transactions",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Files_FileId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "JsonGroupRepresentant",
                table: "TransactionGroups");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Files_FileId",
                table: "Transactions",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "FileId");
        }
    }
}
