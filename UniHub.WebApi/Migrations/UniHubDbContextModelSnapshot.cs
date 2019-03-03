﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniHub.WebApi.DataAccess;

namespace UniHub.WebApi.Migrations
{
    [DbContext(typeof(UniHubDbContext))]
    partial class UniHubDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(32)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnswerId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("PostId");

                    b.Property<int>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Credentional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(84);

                    b.HasKey("Id");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("FullTitle")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<string>("ShortTitle")
                        .IsRequired()
                        .HasMaxLength(7);

                    b.Property<int>("UniversityId");

                    b.HasKey("Id");

                    b.HasIndex("UniversityId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnswerId");

                    b.Property<int?>("CommentId");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<int>("PostId");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("CommentId");

                    b.HasIndex("PostId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Number");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("YearStart");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime>("GivenAt")
                        .HasColumnType("date");

                    b.Property<int>("GroupId");

                    b.Property<DateTime>("LastVisit");

                    b.Property<int>("PostLocationType");

                    b.Property<int>("PostValueType");

                    b.Property<int>("Semester");

                    b.Property<int>("SubjectId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(256)");

                    b.Property<int>("FacultyId");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("TeacherId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<int>("UniversityId");

                    b.HasKey("Id");

                    b.HasIndex("UniversityId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<int>("CityId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("FullTitle")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ShortTitle")
                        .IsRequired()
                        .HasMaxLength(7);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.UsersProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<int>("CredentionalId");

                    b.Property<int>("CurrencyCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<DateTime>("LastVisit");

                    b.Property<int>("RoleId");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CredentionalId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("PostId");

                    b.Property<int>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.AnswerVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerId");

                    b.Property<int>("UsersProfileId");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("UsersProfileId");

                    b.ToTable("AnswerVotes");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.PostVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PostId");

                    b.Property<int>("UsersProfileId");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UsersProfileId");

                    b.ToTable("PostVotes");
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.City", b =>
                {
                    b.HasOne("UniHub.WebApi.Model.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Comment", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Answer")
                        .WithMany("Comments")
                        .HasForeignKey("AnswerId");

                    b.HasOne("UniHub.WebApi.Model.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.Model.Entities.UsersProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Faculty", b =>
                {
                    b.HasOne("UniHub.WebApi.Model.Entities.University", "University")
                        .WithMany("Faculties")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.File", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Answer")
                        .WithMany("Files")
                        .HasForeignKey("AnswerId");

                    b.HasOne("UniHub.WebApi.Model.Entities.Comment")
                        .WithMany("Files")
                        .HasForeignKey("CommentId");

                    b.HasOne("UniHub.WebApi.Model.Entities.Post", "Post")
                        .WithMany("Files")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Post", b =>
                {
                    b.HasOne("UniHub.WebApi.Model.Entities.Group", "Group")
                        .WithMany("Posts")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.Model.Entities.Subject", "Subject")
                        .WithMany("Posts")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.Model.Entities.UsersProfile", "UserProfile")
                        .WithMany("Posts")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Subject", b =>
                {
                    b.HasOne("UniHub.WebApi.Model.Entities.Faculty", "Faculty")
                        .WithMany("Subjects")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.Model.Entities.Teacher", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.Teacher", b =>
                {
                    b.HasOne("UniHub.WebApi.Model.Entities.University", "University")
                        .WithMany()
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.University", b =>
                {
                    b.HasOne("UniHub.WebApi.Model.Entities.City", "City")
                        .WithMany("Universities")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.Model.Entities.UsersProfile", b =>
                {
                    b.HasOne("UniHub.WebApi.Model.Entities.Credentional", "Credentional")
                        .WithOne("UserProfile")
                        .HasForeignKey("UniHub.WebApi.Model.Entities.UsersProfile", "CredentionalId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.Model.Entities.Role", "Role")
                        .WithMany("UsersProfiles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Answer", b =>
                {
                    b.HasOne("UniHub.WebApi.Model.Entities.Post", "Post")
                        .WithMany("Answers")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.Model.Entities.UsersProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.AnswerVote", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Answer", "Answer")
                        .WithMany("Votes")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.Model.Entities.UsersProfile", "UsersProfile")
                        .WithMany()
                        .HasForeignKey("UsersProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.PostVote", b =>
                {
                    b.HasOne("UniHub.WebApi.Model.Entities.Post", "Post")
                        .WithMany("Votes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.Model.Entities.UsersProfile", "UsersProfile")
                        .WithMany("Votes")
                        .HasForeignKey("UsersProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
