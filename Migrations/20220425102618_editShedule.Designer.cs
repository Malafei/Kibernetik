﻿// <auto-generated />
using System;
using Kibernetik.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kibernetik.Migrations
{
    [DbContext(typeof(AppEFContext))]
    [Migration("20220425102618_editShedule")]
    partial class editShedule
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Kibernetik.Data.DataNews.News", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("tblNews");
                });

            modelBuilder.Entity("Kibernetik.Data.DataShedule.Lesson", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("classroom")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("date")
                        .HasColumnType("TEXT");

                    b.Property<string>("name_lesson")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int?>("sheduleid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("teacher")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("time")
                        .HasColumnType("TEXT");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("sheduleid");

                    b.ToTable("tblLesson");
                });

            modelBuilder.Entity("Kibernetik.Data.DataShedule.Shedule", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name_group")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("tblShedules");
                });

            modelBuilder.Entity("Kibernetik.Data.DataUser.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<bool>("email_verified")
                        .HasColumnType("INTEGER");

                    b.Property<string>("login")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("remember_token")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("tblUsers");
                });

            modelBuilder.Entity("Kibernetik.Data.DataShedule.Lesson", b =>
                {
                    b.HasOne("Kibernetik.Data.DataShedule.Shedule", "shedule")
                        .WithMany("lessons")
                        .HasForeignKey("sheduleid");

                    b.Navigation("shedule");
                });

            modelBuilder.Entity("Kibernetik.Data.DataShedule.Shedule", b =>
                {
                    b.Navigation("lessons");
                });
#pragma warning restore 612, 618
        }
    }
}
