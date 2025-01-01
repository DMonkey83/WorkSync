﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectService.Data;

#nullable disable

namespace ProjectService.Data.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    [Migration("20241220210240_updated DBContext with board1")]
    partial class updatedDBContextwithboard1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjectService.Entities.Board", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BoardName")
                        .HasColumnType("text");

                    b.Property<int>("BoardType")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("ProjectService.Entities.Component", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ComponentName")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("LeadUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("ProjectService.Entities.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AssigneeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("BoardId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ComponentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IssueKey")
                        .HasColumnType("text");

                    b.Property<Guid>("IssueTypeId")
                        .HasColumnType("uuid");

                    b.Property<int>("OriginalEstimate")
                        .HasColumnType("integer");

                    b.Property<Guid>("PriorityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<int>("RemainingEstimate")
                        .HasColumnType("integer");

                    b.Property<Guid>("ReporterId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SprintId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uuid");

                    b.Property<string>("Summary")
                        .HasColumnType("text");

                    b.Property<int>("TimeSpent")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("ComponentId");

                    b.HasIndex("IssueTypeId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SprintId");

                    b.HasIndex("StatusId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CommentText")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("IssueComments");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueCustomField", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FieldName")
                        .HasColumnType("text");

                    b.Property<string>("FieldValue")
                        .HasColumnType("text");

                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("IssueCustomFields");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ChangedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("FieldName")
                        .HasColumnType("text");

                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<string>("NewValue")
                        .HasColumnType("text");

                    b.Property<string>("OldValue")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("IssueHistories");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueLabel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<string>("LabelName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("IssueLabels");
                });

            modelBuilder.Entity("ProjectService.Entities.IssuePriority", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PriorityName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IssuePriorities");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueSequence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("LastNumber")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("IssueSequences");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("StatusName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IssueStatuses");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("IssueTypeName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IssueTypes");
                });

            modelBuilder.Entity("ProjectService.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("LeadUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ProjectKey")
                        .HasColumnType("text");

                    b.Property<string>("ProjectName")
                        .HasColumnType("text");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectKey")
                        .IsUnique();

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectService.Entities.Sprint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Goal")
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("SprintName")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Sprints");
                });

            modelBuilder.Entity("ProjectService.Entities.Component", b =>
                {
                    b.HasOne("ProjectService.Entities.Project", "Project")
                        .WithMany("Components")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectService.Entities.Issue", b =>
                {
                    b.HasOne("ProjectService.Entities.Board", null)
                        .WithMany("Issues")
                        .HasForeignKey("BoardId");

                    b.HasOne("ProjectService.Entities.Component", "Component")
                        .WithMany("Issues")
                        .HasForeignKey("ComponentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectService.Entities.IssueType", "IssueType")
                        .WithMany("Issues")
                        .HasForeignKey("IssueTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectService.Entities.IssuePriority", "IssuePriority")
                        .WithMany("Issues")
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectService.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectService.Entities.Sprint", "Sprint")
                        .WithMany("Issues")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectService.Entities.IssueStatus", "IssueStatus")
                        .WithMany("Issues")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");

                    b.Navigation("IssuePriority");

                    b.Navigation("IssueStatus");

                    b.Navigation("IssueType");

                    b.Navigation("Project");

                    b.Navigation("Sprint");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueComment", b =>
                {
                    b.HasOne("ProjectService.Entities.Issue", "Issue")
                        .WithMany("IssueComments")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueCustomField", b =>
                {
                    b.HasOne("ProjectService.Entities.Issue", "Issue")
                        .WithMany("IssueCustomFields")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueHistory", b =>
                {
                    b.HasOne("ProjectService.Entities.Issue", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueLabel", b =>
                {
                    b.HasOne("ProjectService.Entities.Issue", "Issue")
                        .WithMany("IssueLabels")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("ProjectService.Entities.Sprint", b =>
                {
                    b.HasOne("ProjectService.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectService.Entities.Board", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("ProjectService.Entities.Component", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("ProjectService.Entities.Issue", b =>
                {
                    b.Navigation("IssueComments");

                    b.Navigation("IssueCustomFields");

                    b.Navigation("IssueLabels");
                });

            modelBuilder.Entity("ProjectService.Entities.IssuePriority", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueStatus", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("ProjectService.Entities.IssueType", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("ProjectService.Entities.Project", b =>
                {
                    b.Navigation("Components");
                });

            modelBuilder.Entity("ProjectService.Entities.Sprint", b =>
                {
                    b.Navigation("Issues");
                });
#pragma warning restore 612, 618
        }
    }
}
