using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Roombait.Migrations.Residence
{
    public partial class ResidenceMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Residence",
                columns: table => new
                {
                    ResidenceID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:Serial", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residence", x => x.ResidenceID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Residence");
        }
    }
}
