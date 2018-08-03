﻿// <auto-generated />
using DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataBase.Migrations
{
    [DbContext(typeof(WorkHourDbContext))]
    partial class WorkHourDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Clients.Models.Domain.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<bool>("Arch");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("Email");

                    b.Property<Guid>("EmployerId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("ModDateTime");

                    b.Property<string>("Phone");

                    b.Property<byte[]>("Photo");

                    b.HasKey("Id");

                    b.HasIndex("EmployerId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Clients.Models.Domain.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<bool>("Arch");

                    b.Property<Guid?>("ClientId");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<Guid?>("EmployerId");

                    b.Property<DateTime>("ModDateTime");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Photo");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Users.Models.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Arch");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("Email");

                    b.Property<string>("Login");

                    b.Property<DateTime>("ModDateTime");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Works.Models.Domain.Employer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNumber");

                    b.Property<string>("Address");

                    b.Property<bool>("Arch");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("LocationCount");

                    b.Property<DateTime>("ModDateTime");

                    b.Property<byte[]>("Photo");

                    b.Property<Guid>("UserId");

                    b.Property<int>("WorkerCount");

                    b.HasKey("Id");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("Works.Models.Domain.Worker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNumber");

                    b.Property<string>("Address");

                    b.Property<bool>("Arch");

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<Guid>("EmployerId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("ModDateTime");

                    b.Property<byte[]>("Photo");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("Clients.Models.Domain.Client", b =>
                {
                    b.HasOne("Works.Models.Domain.Employer", "Emplyer")
                        .WithMany()
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Clients.Models.Domain.Location", b =>
                {
                    b.HasOne("Clients.Models.Domain.Client", "Client")
                        .WithMany("Locations")
                        .HasForeignKey("ClientId");
                });
#pragma warning restore 612, 618
        }
    }
}
