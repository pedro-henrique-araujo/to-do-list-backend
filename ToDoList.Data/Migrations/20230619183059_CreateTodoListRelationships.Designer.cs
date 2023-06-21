﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoList.Data.Context;

#nullable disable

namespace ToDoList.Data.Migrations
{
    [DbContext(typeof(ToDoListDbContext))]
    [Migration("20230619183059_CreateTodoListRelationships")]
    partial class CreateTodoListRelationships
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.7");

            modelBuilder.Entity("ToDoList.Data.ToDoList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ToDoLists");
                });

            modelBuilder.Entity("ToDoList.Data.ToDoListRelationship", b =>
                {
                    b.Property<Guid>("ParentListId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ChildListId")
                        .HasColumnType("TEXT");

                    b.HasKey("ParentListId", "ChildListId");

                    b.HasIndex("ChildListId");

                    b.ToTable("ToDoListRelationships");
                });

            modelBuilder.Entity("ToDoList.Data.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ToDoList.Data.ToDoListRelationship", b =>
                {
                    b.HasOne("ToDoList.Data.ToDoList", "ChildList")
                        .WithMany()
                        .HasForeignKey("ChildListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToDoList.Data.ToDoList", "ParentList")
                        .WithMany()
                        .HasForeignKey("ParentListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChildList");

                    b.Navigation("ParentList");
                });
#pragma warning restore 612, 618
        }
    }
}
