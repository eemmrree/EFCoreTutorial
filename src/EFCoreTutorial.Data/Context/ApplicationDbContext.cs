using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreTutorial.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorial.Data.Context
{
    public class ApplicationDbContext : DbContext
    {

        // >Could not execute because the specified command or file was not found.< hata çözümü--> dotnet tool install --global dotnet-ef
        //Terminal -> dotnet ef migrations add initMig
        // reverting/geri dönme işlemi -> dotnet ef database update (migrationismi)
        // hangi sql scriptleri çalıştığını görmek için -> dotnet ef migrations script
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //make the configurations
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-H6JU5SG\\SQLEXPRESS;Initial Catalog=efcore;Integrated Security=True");
            }

            //var list = Courses.Where(i => i.Name == "English").Select(i => i.Name).ToList();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");


            //modelBuilder.Entity<Student>().Property(i => i.Id)
            //    .HasColumnName("Id")
            //    .HasColumnType("int")
            //    .UseIdentityColumn()
            //    .IsRequired();
            //modelBuilder.Entity<Student>().Property(i => i.FirstName)
            //    .HasColumnName("First_Name")
            //    .HasColumnType("nvarchar")
            //    .HasMaxLength(100);



            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("students");


                entity.Property(i => i.Id).HasColumnName("id").HasColumnType("int").UseIdentityColumn().IsRequired();
                entity.Property(i => i.FirstName).HasColumnName("first_name").HasColumnType("nvarchar").HasMaxLength(250);
                entity.Property(i => i.LastName).HasColumnName("last_name").HasColumnType("nvarchar").HasMaxLength(250);
                entity.Property(i => i.BirthDate).HasColumnName("birth_date");
                entity.Property(i => i.Number).HasColumnName("number");
                entity.Property(i => i.AddressesId).HasColumnName("address_id");
                entity.HasMany(i => i.Books)
                    .WithOne(i => i.Student)
                    .HasForeignKey(i => i.StudentId)
                    .HasConstraintName("student_book_id_fk");


            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("teachers");

                entity.Property(i => i.Id).HasColumnName("id").UseIdentityColumn();
                entity.Property(i => i.FirstName).HasColumnName("first_name").HasColumnType("nvarchar").HasMaxLength(100);
                entity.Property(i => i.LastName).HasColumnName("last_name").HasColumnType("nvarchar").HasMaxLength(100);
                entity.Property(i => i.BirthDate).HasColumnName("birth_date");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("courses");

                entity.Property(i => i.Id).HasColumnName("id").UseIdentityColumn();
                entity.Property(i => i.Name).HasColumnName("name").HasColumnType("nvarchar").HasMaxLength(100);
                entity.Property(i => i.IsActive).HasColumnName("is_active");
            });
            modelBuilder.Entity<StudentAddress>(entity =>
            {
                entity.ToTable("student_adresses");

                entity.Property(i => i.Id).HasColumnName("id").UseIdentityColumn();
                entity.Property(i => i.City).HasColumnName("city").HasMaxLength(50);
                entity.Property(i => i.District).HasColumnName("district").HasMaxLength(100);
                entity.Property(i => i.Country).HasColumnName("country").HasMaxLength(50);
                entity.Property(i => i.FullAddress).HasColumnName("fullAddress").HasMaxLength(300);

                entity.HasOne(i => i.Student)
                    .WithOne(i => i.Address)
                    .HasForeignKey<Student>(i => i.AddressesId)
                    .HasConstraintName("student_adress_student_id_fk");


            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
