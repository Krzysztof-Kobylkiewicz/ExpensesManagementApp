using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class TransactionGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Files",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransactionGroups",
                columns: table => new
                {
                    TransactionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionGroupSum = table.Column<double>(type: "float", nullable: false),
                    TransactionGroupExpensesSum = table.Column<double>(type: "float", nullable: false),
                    TransactionGroupIncomeSum = table.Column<double>(type: "float", nullable: false),
                    UpoloadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionGroups", x => x.TransactionGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AccountingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationNumber = table.Column<int>(type: "int", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransactionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "FileId");
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionGroups_TransactionGroupId",
                        column: x => x.TransactionGroupId,
                        principalTable: "TransactionGroups",
                        principalColumn: "TransactionGroupId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FileId",
                table: "Transactions",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionGroupId",
                table: "Transactions",
                column: "TransactionGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionGroups");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Files");

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    OperationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    OperationNumber = table.Column<int>(type: "int", nullable: false),
                    OperationTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "FileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_FileId",
                table: "Expenses",
                column: "FileId");
        }
    }
}
