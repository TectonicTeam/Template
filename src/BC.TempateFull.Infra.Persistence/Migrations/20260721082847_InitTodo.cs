using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BC.TempateFull.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitTodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FleetManagement");

            migrationBuilder.CreateTable(
                name: "Todos",
                schema: "FleetManagement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OptionType = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<float>(type: "real", nullable: false),
                    ConcurrencyToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                schema: "FleetManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TodoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItems_Todos_TodoId",
                        column: x => x.TodoId,
                        principalSchema: "FleetManagement",
                        principalTable: "Todos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_TodoId",
                schema: "FleetManagement",
                table: "TodoItems",
                column: "TodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Title",
                schema: "FleetManagement",
                table: "Todos",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems",
                schema: "FleetManagement");

            migrationBuilder.DropTable(
                name: "Todos",
                schema: "FleetManagement");
        }
    }
}
