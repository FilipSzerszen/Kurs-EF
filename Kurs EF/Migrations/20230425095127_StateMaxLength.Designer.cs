﻿// <auto-generated />
using System;
using Kurs_EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kurs_EF.Migrations
{
    [DbContext(typeof(MyBoardsContext))]
    [Migration("20230425095127_StateMaxLength")]
    partial class StateMaxLength
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Kurs_EF.Entities.Adress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("Kurs_EF.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkItemId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Kurs_EF.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Kurs_EF.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Kurs_EF.Entities.WorkItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Area")
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("AuthorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IterationPath")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Iteration_Path");

                    b.Property<int>("Priority")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorID");

                    b.HasIndex("StateId");

                    b.ToTable("WorkItems");

                    b.HasDiscriminator<string>("Discriminator").HasValue("WorkItem");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Kurs_EF.Entities.WorkItemState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("WorkItemStates");
                });

            modelBuilder.Entity("Kurs_EF.Entities.WorkitemTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("WorkItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublicationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("TagId", "WorkItemId");

                    b.HasIndex("WorkItemId");

                    b.ToTable("WorkitemTag");
                });

            modelBuilder.Entity("Kurs_EF.Entities.Epic", b =>
                {
                    b.HasBaseType("Kurs_EF.Entities.WorkItem");

                    b.Property<DateTime?>("EndDate")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Epic");
                });

            modelBuilder.Entity("Kurs_EF.Entities.Issue", b =>
                {
                    b.HasBaseType("Kurs_EF.Entities.WorkItem");

                    b.Property<decimal>("Effort")
                        .HasColumnType("decimal(5,2)");

                    b.HasDiscriminator().HasValue("Issue");
                });

            modelBuilder.Entity("Kurs_EF.Entities.Task", b =>
                {
                    b.HasBaseType("Kurs_EF.Entities.WorkItem");

                    b.Property<string>("Acctivity")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("RemainingWork")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.HasDiscriminator().HasValue("Task");
                });

            modelBuilder.Entity("Kurs_EF.Entities.Adress", b =>
                {
                    b.HasOne("Kurs_EF.Entities.User", "User")
                        .WithOne("Adress")
                        .HasForeignKey("Kurs_EF.Entities.Adress", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Kurs_EF.Entities.Comment", b =>
                {
                    b.HasOne("Kurs_EF.Entities.WorkItem", "WorkItem")
                        .WithMany("Comments")
                        .HasForeignKey("WorkItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkItem");
                });

            modelBuilder.Entity("Kurs_EF.Entities.WorkItem", b =>
                {
                    b.HasOne("Kurs_EF.Entities.User", "Author")
                        .WithMany("WorkItems")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kurs_EF.Entities.WorkItemState", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("State");
                });

            modelBuilder.Entity("Kurs_EF.Entities.WorkitemTag", b =>
                {
                    b.HasOne("Kurs_EF.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kurs_EF.Entities.WorkItem", "WorkItem")
                        .WithMany()
                        .HasForeignKey("WorkItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("WorkItem");
                });

            modelBuilder.Entity("Kurs_EF.Entities.User", b =>
                {
                    b.Navigation("Adress");

                    b.Navigation("WorkItems");
                });

            modelBuilder.Entity("Kurs_EF.Entities.WorkItem", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
