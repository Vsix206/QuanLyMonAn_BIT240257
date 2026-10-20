using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyMonAn_BIT240257.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BIT240257_DishCategories",
                columns: table => new
                {
                    BIT240257_CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BIT240257_CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BIT240257_Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIT240257_DishCategories", x => x.BIT240257_CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "BIT240257_Dishes",
                columns: table => new
                {
                    BIT240257_DishId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BIT240257_DishName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BIT240257_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BIT240257_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BIT240257_ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BIT240257_CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIT240257_Dishes", x => x.BIT240257_DishId);
                    table.ForeignKey(
                        name: "FK_BIT240257_Dishes_BIT240257_DishCategories_BIT240257_CategoryId",
                        column: x => x.BIT240257_CategoryId,
                        principalTable: "BIT240257_DishCategories",
                        principalColumn: "BIT240257_CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BIT240257_DishImages",
                columns: table => new
                {
                    BIT240257_ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BIT240257_DishId = table.Column<int>(type: "int", nullable: false),
                    BIT240257_ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIT240257_DishImages", x => x.BIT240257_ImageId);
                    table.ForeignKey(
                        name: "FK_BIT240257_DishImages_BIT240257_Dishes_BIT240257_DishId",
                        column: x => x.BIT240257_DishId,
                        principalTable: "BIT240257_Dishes",
                        principalColumn: "BIT240257_DishId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BIT240257_Dishes_BIT240257_CategoryId",
                table: "BIT240257_Dishes",
                column: "BIT240257_CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BIT240257_DishImages_BIT240257_DishId",
                table: "BIT240257_DishImages",
                column: "BIT240257_DishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BIT240257_DishImages");

            migrationBuilder.DropTable(
                name: "BIT240257_Dishes");

            migrationBuilder.DropTable(
                name: "BIT240257_DishCategories");
        }
    }
}
