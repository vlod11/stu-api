﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniHub.WebApi.DataAccess;

namespace UniHub.WebApi.Migrations
{
    [DbContext(typeof(UniHubDbContext))]
    [Migration("20190401063805_BaseEntityChange")]
    partial class BaseEntityChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedAt");

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

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnswerId");

                    b.Property<DateTime>("CreatedAt");

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

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Country", b =>
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

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Credentional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(84);

                    b.HasKey("Id");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description");

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

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnswerId");

                    b.Property<int?>("CommentId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<int>("FileTypeId");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<int>("PostId");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("CommentId");

                    b.HasIndex("FileTypeId");

                    b.HasIndex("PostId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.FileType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("FileTypes");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("Number");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("YearStart");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("GivenAt");

                    b.Property<int>("GroupId");

                    b.Property<DateTime>("LastVisit");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("PointsCount");

                    b.Property<int>("PostLocationTypeId");

                    b.Property<int>("PostValueTypeId");

                    b.Property<int>("Semester");

                    b.Property<int>("SubjectId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("PostLocationTypeId");

                    b.HasIndex("PostValueTypeId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.PostAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActionTypeId");

                    b.Property<int>("PostId");

                    b.Property<int>("UsersProfileId");

                    b.HasKey("Id");

                    b.HasIndex("ActionTypeId");

                    b.HasIndex("PostId");

                    b.HasIndex("UsersProfileId");

                    b.ToTable("PostActions");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.PostActionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PostId");

                    b.Property<int?>("UsersProfileId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UsersProfileId");

                    b.ToTable("PostActionTypes");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.PostLocationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("PostLocationTypes");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.PostValueType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("PostValueTypes");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.RoleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("RoleTypes");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description");

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

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Teacher", b =>
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

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("UniversityId");

                    b.HasKey("Id");

                    b.HasIndex("UniversityId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<int>("CityId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description");

                    b.Property<string>("FullTitle")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<string>("ShortTitle")
                        .IsRequired()
                        .HasMaxLength(7);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.UsersProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("CredentionalId");

                    b.Property<int>("CurrencyCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<DateTime>("LastVisit");

                    b.Property<DateTime>("ModifiedAt");

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
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Post", "Post")
                        .WithMany("Answers")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.UsersProfile", "UserProfile")
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

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.UsersProfile", "UsersProfile")
                        .WithMany()
                        .HasForeignKey("UsersProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.City", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Comment", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Answer")
                        .WithMany("Comments")
                        .HasForeignKey("AnswerId");

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.UsersProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Faculty", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.University", "University")
                        .WithMany("Faculties")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.File", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Answer")
                        .WithMany("Files")
                        .HasForeignKey("AnswerId");

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Comment")
                        .WithMany("Files")
                        .HasForeignKey("CommentId");

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.FileType", "Type")
                        .WithMany("Files")
                        .HasForeignKey("FileTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Post", "Post")
                        .WithMany("Files")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Post", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Group", "Group")
                        .WithMany("Posts")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.PostLocationType", "PostLocationType")
                        .WithMany("Posts")
                        .HasForeignKey("PostLocationTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.PostValueType", "PostValueType")
                        .WithMany("Posts")
                        .HasForeignKey("PostValueTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Subject", "Subject")
                        .WithMany("Posts")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.UsersProfile", "UserProfile")
                        .WithMany("Posts")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.PostAction", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.PostActionType", "ActionType")
                        .WithMany("PostActions")
                        .HasForeignKey("ActionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.UsersProfile", "UsersProfile")
                        .WithMany()
                        .HasForeignKey("UsersProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.PostActionType", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Post")
                        .WithMany("Votes")
                        .HasForeignKey("PostId");

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.UsersProfile")
                        .WithMany("Votes")
                        .HasForeignKey("UsersProfileId");
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Subject", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Faculty", "Faculty")
                        .WithMany("Subjects")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Teacher", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.Teacher", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.University", "University")
                        .WithMany()
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.University", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.City", "City")
                        .WithMany("Universities")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniHub.WebApi.ModelLayer.Entities.UsersProfile", b =>
                {
                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.Credentional", "Credentional")
                        .WithOne("UserProfile")
                        .HasForeignKey("UniHub.WebApi.ModelLayer.Entities.UsersProfile", "CredentionalId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniHub.WebApi.ModelLayer.Entities.RoleType", "Role")
                        .WithMany("UsersProfiles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
