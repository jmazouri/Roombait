using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Roombait.Models;

namespace Roombait.Migrations.Residence
{
    [DbContext(typeof(ResidenceContext))]
    partial class ResidenceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Roombait.Models.Residence", b =>
                {
                    b.Property<int>("ResidenceID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ResidenceID");
                });
        }
    }
}
