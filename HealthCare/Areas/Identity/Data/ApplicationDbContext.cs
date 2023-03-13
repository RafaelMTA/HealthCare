using HealthCare.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HealthCare.Models;
using Microsoft.CodeAnalysis;
using System.Security.Policy;
using System;

namespace HealthCare.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        //Set Many to Many relation between Doctor and Clinic tables
        builder.Entity<Clinic>()
            .HasMany(cd => cd.Doctors)
            .WithMany(c => c.Clinics)
            .UsingEntity<ClinicDoctor>(
                j => j
                .HasOne(cd => cd.Doctor)
                .WithMany(d => d.ClinicDoctors)
                .HasForeignKey(cd => cd.DoctorId),
                j => j
                .HasOne(cd => cd.Clinic)
                .WithMany(c => c.ClinicDoctors)
                .HasForeignKey(cd => cd.ClinicId),
                j =>
                {
                    j.HasKey(d => new { d.ClinicId, d.DoctorId });
                }
            );
        builder.Entity<Doctor>()
            .HasMany(cd => cd.Clinics)
            .WithMany(c => c.Doctors)
            .UsingEntity<ClinicDoctor>(
                j => j
                .HasOne(cd => cd.Clinic)
                .WithMany(d => d.ClinicDoctors)
                .HasForeignKey(cd => cd.ClinicId),
                j => j
                .HasOne(cd => cd.Doctor)
                .WithMany(c => c.ClinicDoctors)
                .HasForeignKey(cd => cd.DoctorId),
                j =>
                {
                    j.HasKey(d => new { d.ClinicId, d.DoctorId });
                }
            );

        //Seed default Data to Speciality Table
        builder.Entity<Speciality>().HasData(
            new { Id = 1, Name = "Urology", Description = "Urology" },
            new { Id = 2, Name = "Surgery", Description = "Surgery" },
            new { Id = 3, Name = "Pediatrics", Description = "Pediatrics" },
            new { Id = 4, Name = "Dermatology", Description = "Dermatology" },
            new { Id = 5, Name = "Orthopedics", Description = "Orthopedics" },
            new { Id = 6, Name = "Neurology", Description = "Neurology" },
            new { Id = 7, Name = "Cardiology", Description = "Cardiology" },
            new { Id = 8, Name = "Endocrinology", Description = "Endocrinology" }
        );

        //Seed default Data to Doctor Table
        builder.Entity<Doctor>().HasData(
           new { Id = 1, FirstName = "Krishna", LastName = "Baldilo", SpecialityId = 1 },
           new { Id = 2, FirstName = "Devrim", LastName = "Ehsan", SpecialityId = 2 },
           new { Id = 3, FirstName = "Gautstafr", LastName = "Frej", SpecialityId = 3 },
           new { Id = 4, FirstName = "Jolyon", LastName = "Yevdokim", SpecialityId = 4 },
           new { Id = 5, FirstName = "Stanislav", LastName = "Praxiteles", SpecialityId = 5 },
           new { Id = 6, FirstName = "Nagi", LastName = "Chester", SpecialityId = 6 },
           new { Id = 7, FirstName = "Cornelio", LastName = "Homer", SpecialityId = 7 },
           new { Id = 8, FirstName = "Meino", LastName = "Santeri", SpecialityId = 8 }
        );

        //Set default Data to Clinic Table
        builder.Entity<Clinic>(c =>
        {
            c.HasData(
                new
                {
                    Id = 1,
                    Name = "St. Lukes Medical Center",
                },
                new
                {
                    Id = 2,
                    Name = "West Gate Hospital",
                },
                new
                {
                    Id = 3,
                    Name = "Mervine Health Hospital",
                },
                new
                {
                    Id = 4,
                    Name = "Ashburton Medical Care",
                },
                new
                {
                    Id = 5,
                    Name = "Waikari Care Hospital",
                },
                new
                {
                    Id = 6,
                    Name = "Saint`s Past Hospital",
                }
            );

            c.OwnsOne(cl => cl.Address)
            .HasData(
                new
                {
                    ClinicId = 1,
                    Street = "62 Saint Lukes Road",
                    City = "Auckland",
                    Suburb = "Mount Albert",
                    State = "",
                    PostCode = "8791"
                },
                new
                {
                    ClinicId = 2,
                    Street = "Westgate Shopping Centre 15E Maki Street",
                    City = "Auckland",
                    Suburb = "Massey",
                    State = "",
                    PostCode = "0614"
                },
                new
                {
                    ClinicId = 3,
                    Street = "191 Maunu Road Woodhill",
                    City = "Auckland",
                    Suburb = "Whangarei",
                    State = "",
                    PostCode = "0110"
                },
                new
                {
                    ClinicId = 4,
                    Street = "67 Elizabeth Street Allenton",
                    City = "Christchurch",
                    Suburb = "Ashburton",
                    State = "",
                    PostCode = "7700"
                },
                new
                {
                    ClinicId = 5,
                    Street = "16 Littles Drive",
                    City = "Christchurch",
                    Suburb = "Waikari",
                    State = "",
                    PostCode = "7420"
                },
                new
                {
                    ClinicId = 6,
                    Street = "1 Cook Street",
                    City = "Christchurch",
                    Suburb = "Waipukurau",
                    State = "",
                    PostCode = "3340"
                }
            );
        });

        //Seed default Data to Many-to-Many ClinicDoctor Table
        builder.Entity<ClinicDoctor>().HasData(
                new { ClinicId = 1, DoctorId = 2 },
                new { ClinicId = 1, DoctorId = 4 },
                new { ClinicId = 2, DoctorId = 7 },
                new { ClinicId = 2, DoctorId = 4 },
                new { ClinicId = 2, DoctorId = 8 },
                new { ClinicId = 3, DoctorId = 4 },
                new { ClinicId = 3, DoctorId = 5 },
                new { ClinicId = 3, DoctorId = 1 },
                new { ClinicId = 3, DoctorId = 6 },
                new { ClinicId = 4, DoctorId = 2 },
                new { ClinicId = 4, DoctorId = 3 },
                new { ClinicId = 4, DoctorId = 1 },
                new { ClinicId = 4, DoctorId = 5 },
                new { ClinicId = 4, DoctorId = 8 },
                new { ClinicId = 4, DoctorId = 6 },
                new { ClinicId = 5, DoctorId = 2 },
                new { ClinicId = 5, DoctorId = 5 },
                new { ClinicId = 5, DoctorId = 4 },
                new { ClinicId = 5, DoctorId = 7 },
                new { ClinicId = 6, DoctorId = 1 },
                new { ClinicId = 6, DoctorId = 2 },
                new { ClinicId = 6, DoctorId = 5 },
                new { ClinicId = 6, DoctorId = 6 }
            );

        builder.Entity<Product>().HasData(    
            new
            {
                Id = 1,
                Name = "Health Insurance",
                Description = "Manage your health and have the security of being taken care of, we care for you.",
                ImageURL = "https://cdn-icons-png.flaticon.com/128/2382/2382443.png",
                Price = 20.00
            },
            new
            {
                Id = 2,
                Name = "Company Health Insurance",
                Description = "Give security to your workplace with our collection of health coverage solutions.",
                ImageURL = "https://t3.ftcdn.net/jpg/04/89/66/40/240_F_489664064_evclYQ3FXLs7d8OYc0RvCWT4vxEioFzg.jpg",
                Price = 50.00
            },
            new
            {
                Id = 3,
                Name = "Travel Insurance",
                Description = "Have a peace of mind when travelling, with our travel insurance you can focus on the fun.",
                ImageURL = "https://cdn-icons-png.flaticon.com/128/1085/1085493.png",
                Price = 70.00
            }
        );
    }

    public DbSet<Speciality> Specialities { get; set; } = default!;
    public DbSet<Clinic> Clinics { get; set; } = default!;
    public DbSet<Doctor> Doctors { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Insurance> Insurances { get; set; } = default!;
    public DbSet<Appointment> Appointments { get; set; } = default!;
    public DbSet<ClinicDoctor> ClinicDoctors { get; set; } = default!;
}
