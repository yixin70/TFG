﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TFG.Models;

#nullable disable

namespace TFG.Migrations
{
    [DbContext(typeof(TFGContext))]
    [Migration("20240517214346_Log_MediaStory_Relation")]
    partial class Log_MediaStory_Relation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TFG.Models.InstagramLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsSuspicious")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("InstagramLog", (string)null);
                });

            modelBuilder.Entity("TFG.Models.InstagramMedia", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<long>("InstagramLogId")
                        .HasColumnType("bigint");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("InstagramLogId")
                        .IsUnique();

                    b.ToTable("InstagramMedia", (string)null);
                });

            modelBuilder.Entity("TFG.Models.InstagramStory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("InstagramLogId")
                        .HasColumnType("bigint");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("InstagramLogId")
                        .IsUnique();

                    b.ToTable("InstagramStory", (string)null);
                });

            modelBuilder.Entity("TFG.Models.InstagramMedia", b =>
                {
                    b.HasOne("TFG.Models.InstagramLog", "InstagramLog")
                        .WithOne("InstagramMedia")
                        .HasForeignKey("TFG.Models.InstagramMedia", "InstagramLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InstagramLog");
                });

            modelBuilder.Entity("TFG.Models.InstagramStory", b =>
                {
                    b.HasOne("TFG.Models.InstagramLog", "InstagramLog")
                        .WithOne("InstagramStory")
                        .HasForeignKey("TFG.Models.InstagramStory", "InstagramLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InstagramLog");
                });

            modelBuilder.Entity("TFG.Models.InstagramLog", b =>
                {
                    b.Navigation("InstagramMedia");

                    b.Navigation("InstagramStory");
                });
#pragma warning restore 612, 618
        }
    }
}
