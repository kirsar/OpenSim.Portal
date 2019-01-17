using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenSim.Portal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Presentations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AuthorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AuthorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Simulations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AuthorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerPresentation",
                columns: table => new
                {
                    ServerId = table.Column<int>(nullable: false),
                    PresentationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerPresentation", x => new { x.ServerId, x.PresentationId });
                    table.ForeignKey(
                        name: "FK_ServerPresentation_Presentations_PresentationId",
                        column: x => x.PresentationId,
                        principalTable: "Presentations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerPresentation_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerSimulation",
                columns: table => new
                {
                    ServerId = table.Column<int>(nullable: false),
                    SimulationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerSimulation", x => new { x.ServerId, x.SimulationId });
                    table.ForeignKey(
                        name: "FK_ServerSimulation_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerSimulation_Simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "Simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulationPresentation",
                columns: table => new
                {
                    SimulationId = table.Column<int>(nullable: false),
                    PresentationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationPresentation", x => new { x.SimulationId, x.PresentationId });
                    table.ForeignKey(
                        name: "FK_SimulationPresentation_Presentations_PresentationId",
                        column: x => x.PresentationId,
                        principalTable: "Presentations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimulationPresentation_Simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "Simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulationReference",
                columns: table => new
                {
                    SimulationId = table.Column<int>(nullable: false),
                    ReferenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationReference", x => new { x.SimulationId, x.ReferenceId });
                    table.ForeignKey(
                        name: "FK_SimulationReference_Simulations_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "Simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimulationReference_Simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "Simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerPresentation_PresentationId",
                table: "ServerPresentation",
                column: "PresentationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerSimulation_SimulationId",
                table: "ServerSimulation",
                column: "SimulationId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationPresentation_PresentationId",
                table: "SimulationPresentation",
                column: "PresentationId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationReference_ReferenceId",
                table: "SimulationReference",
                column: "ReferenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerPresentation");

            migrationBuilder.DropTable(
                name: "ServerSimulation");

            migrationBuilder.DropTable(
                name: "SimulationPresentation");

            migrationBuilder.DropTable(
                name: "SimulationReference");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "Presentations");

            migrationBuilder.DropTable(
                name: "Simulations");
        }
    }
}
