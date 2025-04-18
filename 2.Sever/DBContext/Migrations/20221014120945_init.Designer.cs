﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyDBContext.Main;

#nullable disable

namespace DBContext.Main.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20221014120945_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MyDBContext.Main.Device", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<long>("DeviceTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("LatestData")
                        .HasColumnType("longtext");

                    b.Property<string>("LocationStr")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceTypeId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("MyDBContext.Main.Device_AutoControl", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<bool>("TimedControl")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TimedControlSetting")
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Device_AutoControls");
                });

            modelBuilder.Entity("MyDBContext.Main.Device_DataPoint", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<long>("StreamId")
                        .HasColumnType("bigint");

                    b.Property<long>("Time")
                        .HasColumnType("bigint");

                    b.Property<float>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Device_DataPoints");
                });

            modelBuilder.Entity("MyDBContext.Main.Device_Type", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<string>("DataPoints")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Script")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Device_Types");
                });

            modelBuilder.Entity("MyDBContext.Main.History", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Data")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Historys");
                });

            modelBuilder.Entity("MyDBContext.Main.Internal_Mail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Context")
                        .HasColumnType("longtext");

                    b.Property<long>("LastEMailTime")
                        .HasColumnType("bigint");

                    b.Property<bool>("Readed")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("ReceiverId")
                        .HasColumnType("bigint");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint");

                    b.Property<long>("Time")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Internal_Mails");
                });

            modelBuilder.Entity("MyDBContext.Main.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<string>("EMail")
                        .HasColumnType("longtext");

                    b.Property<long>("LastLogin")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Pass")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            EMail = "2432114474@qq.com",
                            LastLogin = 0L,
                            Name = "admin",
                            Pass = "123",
                            Phone = "15850798245",
                            Status = (byte)0
                        });
                });

            modelBuilder.Entity("MyDBContext.Main.User_Device", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<bool>("PControl")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("PData")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("PStatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("User_Device_GroupId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "DeviceId");

                    b.HasIndex("DeviceId");

                    b.HasIndex("User_Device_GroupId");

                    b.ToTable("User_Devices");
                });

            modelBuilder.Entity("MyDBContext.Main.User_Device_Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("User_Device_Groups");
                });

            modelBuilder.Entity("MyDBContext.Main.User_Op_Audit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("AuditorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Data")
                        .HasColumnType("longtext");

                    b.Property<string>("Op")
                        .HasColumnType("longtext");

                    b.Property<long>("SponsorId")
                        .HasColumnType("bigint");

                    b.Property<long>("Time")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("User_Op_Audits");
                });

            modelBuilder.Entity("MyDBContext.Main.User_SF", b =>
                {
                    b.Property<long>("FatherId")
                        .HasColumnType("bigint");

                    b.Property<long>("SonId")
                        .HasColumnType("bigint");

                    b.HasKey("FatherId", "SonId");

                    b.HasIndex("SonId", "FatherId");

                    b.ToTable("User_SFs");
                });

            modelBuilder.Entity("MyDBContext.Main.Device", b =>
                {
                    b.HasOne("MyDBContext.Main.Device_Type", "DeviceType")
                        .WithMany()
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeviceType");
                });

            modelBuilder.Entity("MyDBContext.Main.Device_AutoControl", b =>
                {
                    b.HasOne("MyDBContext.Main.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId");

                    b.Navigation("Device");
                });

            modelBuilder.Entity("MyDBContext.Main.User", b =>
                {
                    b.HasOne("MyDBContext.Main.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("MyDBContext.Main.User_Device", b =>
                {
                    b.HasOne("MyDBContext.Main.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDBContext.Main.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDBContext.Main.User_Device_Group", "User_Device_Group")
                        .WithMany()
                        .HasForeignKey("User_Device_GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("User");

                    b.Navigation("User_Device_Group");
                });

            modelBuilder.Entity("MyDBContext.Main.User_Device_Group", b =>
                {
                    b.HasOne("MyDBContext.Main.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("MyDBContext.Main.User_SF", b =>
                {
                    b.HasOne("MyDBContext.Main.User", "Father")
                        .WithMany()
                        .HasForeignKey("FatherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDBContext.Main.User", "Son")
                        .WithMany()
                        .HasForeignKey("SonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Father");

                    b.Navigation("Son");
                });
#pragma warning restore 612, 618
        }
    }
}
