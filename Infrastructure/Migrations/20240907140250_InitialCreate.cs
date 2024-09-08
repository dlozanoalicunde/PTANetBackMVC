using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HourlyImbalanceFee = table.Column<decimal>(type: "decimal(10,4)", nullable: true),
                    ImbalanceFee = table.Column<decimal>(type: "decimal(10,4)", nullable: true),
                    PeakLoadFee = table.Column<decimal>(type: "decimal(10,4)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimestampUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    VolumeFee = table.Column<decimal>(type: "decimal(10,4)", nullable: true),
                    WeeklyFee = table.Column<int>(type: "decimal(10,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fees");
        }
    }
}
