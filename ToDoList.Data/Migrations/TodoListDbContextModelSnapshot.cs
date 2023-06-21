﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoList.Data.Context;

#nullable disable

namespace ToDoList.Data.Migrations
{
    [DbContext(typeof(ToDoListDbContext))]
    partial class TodoListDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.7");

            modelBuilder.Entity("ToDoList.Data.ToDo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Done")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ToDoListId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("ToDoListId");

                    b.ToTable("ToDos");
                });

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

            modelBuilder.Entity("ToDoList.Data.ToDoListAccess", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ToDoListId")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessType")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "ToDoListId");

                    b.HasIndex("ToDoListId");

                    b.ToTable("ToDoListAccesses");
                });

            modelBuilder.Entity("ToDoList.Data.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ToDoList.Data.ToDo", b =>
                {
                    b.HasOne("ToDoList.Data.ToDo", "Parent")
                        .WithMany("ToDos")
                        .HasForeignKey("ParentId");

                    b.HasOne("ToDoList.Data.ToDoList", "ToDoList")
                        .WithMany("ToDos")
                        .HasForeignKey("ToDoListId");

                    b.Navigation("Parent");

                    b.Navigation("ToDoList");
                });

            modelBuilder.Entity("ToDoList.Data.ToDoListAccess", b =>
                {
                    b.HasOne("ToDoList.Data.ToDoList", "ToDoList")
                        .WithMany()
                        .HasForeignKey("ToDoListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToDoList.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ToDoList");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDoList.Data.ToDo", b =>
                {
                    b.Navigation("ToDos");
                });

            modelBuilder.Entity("ToDoList.Data.ToDoList", b =>
                {
                    b.Navigation("ToDos");
                });
#pragma warning restore 612, 618
        }
    }
}
