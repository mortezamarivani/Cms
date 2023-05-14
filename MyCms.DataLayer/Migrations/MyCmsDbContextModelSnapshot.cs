﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyCms.DataLayer.Context;

namespace MyCms.DataLayer.Migrations
{
    [DbContext(typeof(MyCmsDbContext))]
    partial class MyCmsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyCms.DomainClasses.About.About", b =>
                {
                    b.Property<int>("AboutID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AboutDescription")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("AboutHeader")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("AboutTitle")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Address")
                        .HasMaxLength(400);

                    b.Property<int>("CreatedDate");

                    b.Property<int>("CreatorUserID");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("FacebookAddress")
                        .HasMaxLength(100);

                    b.Property<string>("HomeDesc")
                        .HasMaxLength(500);

                    b.Property<string>("InstagramID")
                        .HasMaxLength(50);

                    b.Property<string>("LinkdinAddress")
                        .HasMaxLength(100);

                    b.Property<string>("Mobile")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<bool>("Status");

                    b.Property<string>("TelPhon")
                        .HasMaxLength(100);

                    b.Property<string>("TwitterAddress")
                        .HasMaxLength(100);

                    b.HasKey("AboutID");

                    b.ToTable("About");
                });

            modelBuilder.Entity("MyCms.DomainClasses.Gallery.Gallery", b =>
                {
                    b.Property<int>("GalleryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedDate");

                    b.Property<int>("CreatorUserID");

                    b.Property<string>("GalleryDesc")
                        .HasMaxLength(200);

                    b.Property<string>("GalleryName")
                        .HasMaxLength(200);

                    b.Property<bool>("Status");

                    b.HasKey("GalleryID");

                    b.ToTable("Gallery");
                });

            modelBuilder.Entity("MyCms.DomainClasses.Page.Page", b =>
                {
                    b.Property<int>("PageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("GroupID");

                    b.Property<string>("ImageName");

                    b.Property<string>("PageTags");

                    b.Property<string>("PageText")
                        .IsRequired();

                    b.Property<string>("PageTitle")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.Property<int>("PageVisit");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<bool>("ShowInSlider");

                    b.HasKey("PageID");

                    b.HasIndex("GroupID");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("MyCms.DomainClasses.PageGroup.PageGroup", b =>
                {
                    b.Property<int>("GroupID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("GroupID");

                    b.ToTable("PageGroups");
                });

            modelBuilder.Entity("MyCms.DomainClasses.ReciveInfo.ReciveInfo", b =>
                {
                    b.Property<int>("ReciveInfoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedDate");

                    b.Property<string>("ReciveMessage")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("SenderEmail")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("SenderName")
                        .HasMaxLength(100);

                    b.Property<bool>("Status");

                    b.HasKey("ReciveInfoID");

                    b.ToTable("ReciveInfo");
                });

            modelBuilder.Entity("MyCms.DomainClasses.Skills.Skills", b =>
                {
                    b.Property<int>("SkillsID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BootstarpClassName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Progress");

                    b.Property<string>("SkillsDescription")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("SkillsTitle")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.Property<bool>("Status");

                    b.HasKey("SkillsID");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("MyCms.DomainClasses.UserX.UserX", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Family")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("Status");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("UserID");

                    b.ToTable("UserX");
                });

            modelBuilder.Entity("MyCms.DomainClasses.Page.Page", b =>
                {
                    b.HasOne("MyCms.DomainClasses.PageGroup.PageGroup", "PageGroup")
                        .WithMany("Pages")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
