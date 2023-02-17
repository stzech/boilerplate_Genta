using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoilerPlate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFaqHeadAndContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaqHead",
                columns: table => new
                {
                    FqHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FqActiveContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedByName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDtServer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedByName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedDtServer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DeletedByName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DeletedByDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByDtServer = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqHead", x => x.FqHeadId);
                });

            migrationBuilder.CreateTable(
                name: "FaqContent",
                columns: table => new
                {
                    FqContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FqTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FqContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FqHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaqHeadFqHeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedByName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDtServer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedByName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastUpdatedDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedDtServer = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DeletedByName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DeletedByDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedByDtServer = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqContent", x => x.FqContentId);
                    table.ForeignKey(
                        name: "FK_FaqContent_FaqHead_FaqHeadFqHeadId",
                        column: x => x.FaqHeadFqHeadId,
                        principalTable: "FaqHead",
                        principalColumn: "FqHeadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaqContent_CreatedDt",
                table: "FaqContent",
                column: "CreatedDt");

            migrationBuilder.CreateIndex(
                name: "IX_FaqContent_CreatedDtServer",
                table: "FaqContent",
                column: "CreatedDtServer");

            migrationBuilder.CreateIndex(
                name: "IX_FaqContent_DeletedByDt",
                table: "FaqContent",
                column: "DeletedByDt");

            migrationBuilder.CreateIndex(
                name: "IX_FaqContent_FaqHeadFqHeadId",
                table: "FaqContent",
                column: "FaqHeadFqHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_FaqContent_FqHeadId",
                table: "FaqContent",
                column: "FqHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_FaqContent_FqTitle",
                table: "FaqContent",
                column: "FqTitle");

            migrationBuilder.CreateIndex(
                name: "IX_FaqContent_LastUpdatedDt",
                table: "FaqContent",
                column: "LastUpdatedDt");

            migrationBuilder.CreateIndex(
                name: "IX_FaqContent_LastUpdatedDtServer",
                table: "FaqContent",
                column: "LastUpdatedDtServer");

            migrationBuilder.CreateIndex(
                name: "IX_FaqHead_CreatedDt",
                table: "FaqHead",
                column: "CreatedDt");

            migrationBuilder.CreateIndex(
                name: "IX_FaqHead_CreatedDtServer",
                table: "FaqHead",
                column: "CreatedDtServer");

            migrationBuilder.CreateIndex(
                name: "IX_FaqHead_DeletedByDt",
                table: "FaqHead",
                column: "DeletedByDt");

            migrationBuilder.CreateIndex(
                name: "IX_FaqHead_FqActiveContentId",
                table: "FaqHead",
                column: "FqActiveContentId");

            migrationBuilder.CreateIndex(
                name: "IX_FaqHead_FqHeadId",
                table: "FaqHead",
                column: "FqHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_FaqHead_LastUpdatedDt",
                table: "FaqHead",
                column: "LastUpdatedDt");

            migrationBuilder.CreateIndex(
                name: "IX_FaqHead_LastUpdatedDtServer",
                table: "FaqHead",
                column: "LastUpdatedDtServer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaqContent");

            migrationBuilder.DropTable(
                name: "FaqHead");
        }
    }
}
