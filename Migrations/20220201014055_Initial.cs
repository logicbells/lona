using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lona.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    HomeOwner = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    PaymentPlan = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TermlyAmountPayable = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalAmountPayable = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalStatus = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    ApprovalStatusActionByStaffId = table.Column<int>(type: "int", nullable: true),
                    ApprovalStatusDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loans_Staffers_ApprovalStatusActionByStaffId",
                        column: x => x.ApprovalStatusActionByStaffId,
                        principalTable: "Staffers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LoanPaymentList",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanId = table.Column<long>(type: "bigint", nullable: false),
                    ApplicantId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentDueDate = table.Column<DateTime>(type: "date", nullable: false),
                    TermlyAmountPayable = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanPaymentList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanPaymentList_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanPaymentList_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Applicants",
                columns: new[] { "Id", "Address", "DateOfBirth", "Email", "HomeOwner", "Name" },
                values: new object[,]
                {
                    { 1L, "Citec Estate, Abuja", new DateTime(1980, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "baba@yahoo.com", true, "Baba Usman" },
                    { 2L, "Suncity Estate, Abuja", new DateTime(1983, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "dele@gmail.com", false, "Dele Adeyemi" }
                });

            migrationBuilder.InsertData(
                table: "Staffers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "James Godbold" },
                    { 2, "Chinemere" }
                });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "Amount", "ApplicantId", "ApprovalStatus", "ApprovalStatusActionByStaffId", "ApprovalStatusDate", "DateCreated", "InterestRate", "PaymentPlan", "Term", "TermlyAmountPayable", "TotalAmountPayable" },
                values: new object[] { 1L, 100000m, 1L, "Approved", 1, new DateTime(2022, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 15m, "Monthly", 18, 6388.89m, 115000m });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "Amount", "ApplicantId", "ApprovalStatus", "ApprovalStatusActionByStaffId", "ApprovalStatusDate", "DateCreated", "InterestRate", "PaymentPlan", "Term", "TermlyAmountPayable", "TotalAmountPayable" },
                values: new object[] { 2L, 100000m, 2L, "Declined", 1, new DateTime(2022, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 15m, "Monthly", 18, 6388.89m, 115000m });

            migrationBuilder.InsertData(
                table: "LoanPaymentList",
                columns: new[] { "Id", "ApplicantId", "IsPaid", "LoanId", "PaymentDueDate", "TermlyAmountPayable" },
                values: new object[] { 1L, 1L, false, 1L, new DateTime(2022, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 6388.89m });

            migrationBuilder.InsertData(
                table: "LoanPaymentList",
                columns: new[] { "Id", "ApplicantId", "IsPaid", "LoanId", "PaymentDueDate", "TermlyAmountPayable" },
                values: new object[] { 2L, 2L, false, 2L, new DateTime(2022, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 6388.89m });

            migrationBuilder.CreateIndex(
                name: "IX_LoanPaymentList_ApplicantId",
                table: "LoanPaymentList",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanPaymentList_LoanId",
                table: "LoanPaymentList",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ApplicantId",
                table: "Loans",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ApprovalStatusActionByStaffId",
                table: "Loans",
                column: "ApprovalStatusActionByStaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanPaymentList");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.DropTable(
                name: "Staffers");
        }
    }
}
