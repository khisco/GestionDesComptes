﻿// <auto-generated />
using System;
using GestionBank.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestionBank.Migrations
{
    [DbContext(typeof(GestionContext))]
    [Migration("20190301103107_intialcreate")]
    partial class intialcreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestionBank.Data.Entities.Client", b =>
                {
                    b.Property<int>("Clientid")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("email");

                    b.Property<string>("nom");

                    b.Property<string>("prenom");

                    b.Property<int>("sex");

                    b.HasKey("Clientid");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("GestionBank.Data.Entities.Compte", b =>
                {
                    b.Property<int>("CompteId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Clientid");

                    b.Property<DateTime>("DateCreation");

                    b.Property<DateTime>("DateExpiration");

                    b.Property<int>("Type");

                    b.HasKey("CompteId");

                    b.HasIndex("Clientid");

                    b.ToTable("Comptes");
                });

            modelBuilder.Entity("GestionBank.Data.Entities.Compte", b =>
                {
                    b.HasOne("GestionBank.Data.Entities.Client", "Client")
                        .WithMany("Comptes")
                        .HasForeignKey("Clientid");
                });
#pragma warning restore 612, 618
        }
    }
}
