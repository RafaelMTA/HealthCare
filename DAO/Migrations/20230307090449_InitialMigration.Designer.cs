﻿// <auto-generated />
using System;
using HealthCare.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HealthCare.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230307090449_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HealthCare.Areas.Identity.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("HealthCare.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClinicId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Created_By")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updated_By")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("UserId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("HealthCare.Models.Clinic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Created_By")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("Updated_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updated_By")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clinics");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "St. Lukes Medical Center"
                        },
                        new
                        {
                            Id = 2,
                            Name = "West Gate Hospital"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Mervine Health Hospital"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Ashburton Medical Care"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Waikari Care Hospital"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Saint`s Past Hospital"
                        });
                });

            modelBuilder.Entity("HealthCare.Models.ClinicDoctor", b =>
                {
                    b.Property<int?>("ClinicId")
                        .HasColumnType("int");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.HasKey("ClinicId", "DoctorId");

                    b.HasIndex("DoctorId");

                    b.ToTable("ClinicDoctors");

                    b.HasData(
                        new
                        {
                            ClinicId = 1,
                            DoctorId = 2
                        },
                        new
                        {
                            ClinicId = 1,
                            DoctorId = 4
                        },
                        new
                        {
                            ClinicId = 2,
                            DoctorId = 7
                        },
                        new
                        {
                            ClinicId = 2,
                            DoctorId = 4
                        },
                        new
                        {
                            ClinicId = 2,
                            DoctorId = 8
                        },
                        new
                        {
                            ClinicId = 3,
                            DoctorId = 4
                        },
                        new
                        {
                            ClinicId = 3,
                            DoctorId = 5
                        },
                        new
                        {
                            ClinicId = 3,
                            DoctorId = 1
                        },
                        new
                        {
                            ClinicId = 3,
                            DoctorId = 6
                        },
                        new
                        {
                            ClinicId = 4,
                            DoctorId = 2
                        },
                        new
                        {
                            ClinicId = 4,
                            DoctorId = 3
                        },
                        new
                        {
                            ClinicId = 4,
                            DoctorId = 1
                        },
                        new
                        {
                            ClinicId = 4,
                            DoctorId = 5
                        },
                        new
                        {
                            ClinicId = 4,
                            DoctorId = 8
                        },
                        new
                        {
                            ClinicId = 4,
                            DoctorId = 6
                        },
                        new
                        {
                            ClinicId = 5,
                            DoctorId = 2
                        },
                        new
                        {
                            ClinicId = 5,
                            DoctorId = 5
                        },
                        new
                        {
                            ClinicId = 5,
                            DoctorId = 4
                        },
                        new
                        {
                            ClinicId = 5,
                            DoctorId = 7
                        },
                        new
                        {
                            ClinicId = 6,
                            DoctorId = 1
                        },
                        new
                        {
                            ClinicId = 6,
                            DoctorId = 2
                        },
                        new
                        {
                            ClinicId = 6,
                            DoctorId = 5
                        },
                        new
                        {
                            ClinicId = 6,
                            DoctorId = 6
                        });
                });

            modelBuilder.Entity("HealthCare.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Created_By")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SpecialityId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updated_By")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SpecialityId");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Krishna",
                            LastName = "Baldilo",
                            SpecialityId = 1
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Devrim",
                            LastName = "Ehsan",
                            SpecialityId = 2
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Gautstafr",
                            LastName = "Frej",
                            SpecialityId = 3
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Jolyon",
                            LastName = "Yevdokim",
                            SpecialityId = 4
                        },
                        new
                        {
                            Id = 5,
                            FirstName = "Stanislav",
                            LastName = "Praxiteles",
                            SpecialityId = 5
                        },
                        new
                        {
                            Id = 6,
                            FirstName = "Nagi",
                            LastName = "Chester",
                            SpecialityId = 6
                        },
                        new
                        {
                            Id = 7,
                            FirstName = "Cornelio",
                            LastName = "Homer",
                            SpecialityId = 7
                        },
                        new
                        {
                            Id = 8,
                            FirstName = "Meino",
                            LastName = "Santeri",
                            SpecialityId = 8
                        });
                });

            modelBuilder.Entity("HealthCare.Models.Insurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Created_By")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Updated_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updated_By")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Insurances");
                });

            modelBuilder.Entity("HealthCare.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Created_By")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime?>("Updated_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updated_By")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Manage your health and have the security of being taken care of, we care for you.",
                            ImageURL = "https://cdn-icons-png.flaticon.com/128/2382/2382443.png",
                            Name = "Health Insurance",
                            Price = 20.0
                        },
                        new
                        {
                            Id = 2,
                            Description = "Give security to your workplace with our collection of health coverage solutions.",
                            ImageURL = "https://t3.ftcdn.net/jpg/04/89/66/40/240_F_489664064_evclYQ3FXLs7d8OYc0RvCWT4vxEioFzg.jpg",
                            Name = "Company Health Insurance",
                            Price = 50.0
                        },
                        new
                        {
                            Id = 3,
                            Description = "Have a peace of mind when travelling, with our travel insurance you can focus on the fun.",
                            ImageURL = "https://cdn-icons-png.flaticon.com/128/1085/1085493.png",
                            Name = "Travel Insurance",
                            Price = 70.0
                        });
                });

            modelBuilder.Entity("HealthCare.Models.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Created_By")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("Updated_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updated_By")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specialities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Urology",
                            Name = "Urology"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Surgery",
                            Name = "Surgery"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Pediatrics",
                            Name = "Pediatrics"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Dermatology",
                            Name = "Dermatology"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Orthopedics",
                            Name = "Orthopedics"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Neurology",
                            Name = "Neurology"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Cardiology",
                            Name = "Cardiology"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Endocrinology",
                            Name = "Endocrinology"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HealthCare.Models.Appointment", b =>
                {
                    b.HasOne("HealthCare.Models.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicId");

                    b.HasOne("HealthCare.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId");

                    b.HasOne("HealthCare.Areas.Identity.Data.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Clinic");

                    b.Navigation("Doctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HealthCare.Models.Clinic", b =>
                {
                    b.OwnsOne("HealthCare.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<int>("ClinicId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PostCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Suburb")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ClinicId");

                            b1.ToTable("Clinics");

                            b1.WithOwner()
                                .HasForeignKey("ClinicId");

                            b1.HasData(
                                new
                                {
                                    ClinicId = 1,
                                    City = "Auckland",
                                    PostCode = "8791",
                                    State = "",
                                    Street = "62 Saint Lukes Road",
                                    Suburb = "Mount Albert"
                                },
                                new
                                {
                                    ClinicId = 2,
                                    City = "Auckland",
                                    PostCode = "0614",
                                    State = "",
                                    Street = "Westgate Shopping Centre 15E Maki Street",
                                    Suburb = "Massey"
                                },
                                new
                                {
                                    ClinicId = 3,
                                    City = "Auckland",
                                    PostCode = "0110",
                                    State = "",
                                    Street = "191 Maunu Road Woodhill",
                                    Suburb = "Whangarei"
                                },
                                new
                                {
                                    ClinicId = 4,
                                    City = "Christchurch",
                                    PostCode = "7700",
                                    State = "",
                                    Street = "67 Elizabeth Street Allenton",
                                    Suburb = "Ashburton"
                                },
                                new
                                {
                                    ClinicId = 5,
                                    City = "Christchurch",
                                    PostCode = "7420",
                                    State = "",
                                    Street = "16 Littles Drive",
                                    Suburb = "Waikari"
                                },
                                new
                                {
                                    ClinicId = 6,
                                    City = "Christchurch",
                                    PostCode = "3340",
                                    State = "",
                                    Street = "1 Cook Street",
                                    Suburb = "Waipukurau"
                                });
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("HealthCare.Models.ClinicDoctor", b =>
                {
                    b.HasOne("HealthCare.Models.Clinic", "Clinic")
                        .WithMany("ClinicDoctors")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthCare.Models.Doctor", "Doctor")
                        .WithMany("ClinicDoctors")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("HealthCare.Models.Doctor", b =>
                {
                    b.HasOne("HealthCare.Models.Speciality", "Speciality")
                        .WithMany()
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("HealthCare.Models.Insurance", b =>
                {
                    b.HasOne("HealthCare.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("HealthCare.Areas.Identity.Data.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HealthCare.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HealthCare.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthCare.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HealthCare.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HealthCare.Models.Clinic", b =>
                {
                    b.Navigation("ClinicDoctors");
                });

            modelBuilder.Entity("HealthCare.Models.Doctor", b =>
                {
                    b.Navigation("ClinicDoctors");
                });
#pragma warning restore 612, 618
        }
    }
}