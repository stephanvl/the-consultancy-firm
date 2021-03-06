﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using TheConsultancyFirm.Data;

namespace TheConsultancyFirm.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180114204854_AddedVacancy")]
    partial class AddedVacancy
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("Enabled");

                    b.Property<DateTime?>("LastLogin");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Block", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<int?>("CaseId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("LastModified");

                    b.Property<int?>("NewsItemId");

                    b.Property<int>("Order");

                    b.Property<int?>("SolutionId");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.HasIndex("NewsItemId");

                    b.HasIndex("SolutionId");

                    b.ToTable("Blocks");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Block");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId")
                        .IsRequired();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Deleted");

                    b.Property<bool>("Enabled");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("PhotoPath");

                    b.Property<string>("SharingDescription");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.CaseTag", b =>
                {
                    b.Property<int>("CaseId");

                    b.Property<int>("TagId");

                    b.HasKey("CaseId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("CaseTag");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<string>("Mobile");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("Read");

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<bool>("Enabled");

                    b.Property<string>("Link")
                        .IsRequired();

                    b.Property<string>("LogoPath");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.CustomerSolution", b =>
                {
                    b.Property<int>("SolutionId");

                    b.Property<int>("CustomerId");

                    b.HasKey("SolutionId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerSolution");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Download", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmountOfDownloads");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("Enabled");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("LinkPath");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Downloads");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.DownloadTag", b =>
                {
                    b.Property<int>("DownloadId");

                    b.Property<int>("TagId");

                    b.HasKey("DownloadId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("DownloadTag");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.NewsItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Deleted");

                    b.Property<bool>("Enabled");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("PhotoPath");

                    b.Property<string>("SharingDescription");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("NewsItems");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.NewsItemTag", b =>
                {
                    b.Property<int>("NewsItemId");

                    b.Property<int>("TagId");

                    b.HasKey("NewsItemId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("NewsItemTag");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Newsletter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NewsletterIntroText")
                        .IsRequired();

                    b.Property<string>("NewsletterOtherNews");

                    b.Property<DateTime>("SentAt");

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("NewsLetters");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.NewsletterSubscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("NewsletterSubscription");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Slide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlockId");

                    b.Property<int>("Order");

                    b.Property<string>("PhotoPath");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.ToTable("Slide");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Solution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("Deleted");

                    b.Property<bool>("Enabled");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("PhotoPath");

                    b.Property<string>("SharingDescription")
                        .HasMaxLength(140);

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Solutions");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.SolutionTag", b =>
                {
                    b.Property<int>("SolutionId");

                    b.Property<int>("TagId");

                    b.HasKey("SolutionId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("SolutionTag");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<bool>("Enabled");

                    b.Property<string>("FunctionDescription");

                    b.Property<DateTime>("VacancySince");

                    b.HasKey("Id");

                    b.ToTable("Vacancy");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.CarouselBlock", b =>
                {
                    b.HasBaseType("TheConsultancyFirm.Models.Block");

                    b.Property<string>("LinkPath");

                    b.Property<string>("LinkText");

                    b.ToTable("CarouselBlock");

                    b.HasDiscriminator().HasValue("CarouselBlock");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.QuoteBlock", b =>
                {
                    b.HasBaseType("TheConsultancyFirm.Models.Block");

                    b.Property<string>("Author");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.ToTable("QuoteBlock");

                    b.HasDiscriminator().HasValue("QuoteBlock");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.SolutionAdvantagesBlock", b =>
                {
                    b.HasBaseType("TheConsultancyFirm.Models.Block");

                    b.Property<string>("PhotoPath");

                    b.Property<string>("Text")
                        .HasColumnName("SolutionAdvantagesBlock_Text");

                    b.ToTable("SolutionAdvantagesBlock");

                    b.HasDiscriminator().HasValue("SolutionAdvantagesBlock");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.TextBlock", b =>
                {
                    b.HasBaseType("TheConsultancyFirm.Models.Block");

                    b.Property<string>("Text")
                        .HasColumnName("TextBlock_Text");

                    b.ToTable("TextBlock");

                    b.HasDiscriminator().HasValue("TextBlock");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TheConsultancyFirm.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TheConsultancyFirm.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TheConsultancyFirm.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TheConsultancyFirm.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Block", b =>
                {
                    b.HasOne("TheConsultancyFirm.Models.Case")
                        .WithMany("Blocks")
                        .HasForeignKey("CaseId");

                    b.HasOne("TheConsultancyFirm.Models.NewsItem")
                        .WithMany("Blocks")
                        .HasForeignKey("NewsItemId");

                    b.HasOne("TheConsultancyFirm.Models.Solution", "Solution")
                        .WithMany("Blocks")
                        .HasForeignKey("SolutionId");
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Case", b =>
                {
                    b.HasOne("TheConsultancyFirm.Models.Customer", "Customer")
                        .WithMany("Cases")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.CaseTag", b =>
                {
                    b.HasOne("TheConsultancyFirm.Models.Case", "Case")
                        .WithMany("CaseTags")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TheConsultancyFirm.Models.Tag", "Tag")
                        .WithMany("CaseTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.CustomerSolution", b =>
                {
                    b.HasOne("TheConsultancyFirm.Models.Customer", "Customer")
                        .WithMany("CustomerSolutions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TheConsultancyFirm.Models.Solution", "Solution")
                        .WithMany("CustomerSolutions")
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.DownloadTag", b =>
                {
                    b.HasOne("TheConsultancyFirm.Models.Download", "Download")
                        .WithMany("DownloadTags")
                        .HasForeignKey("DownloadId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TheConsultancyFirm.Models.Tag", "Tag")
                        .WithMany("DownloadTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.NewsItemTag", b =>
                {
                    b.HasOne("TheConsultancyFirm.Models.NewsItem", "NewsItem")
                        .WithMany("NewsItemTags")
                        .HasForeignKey("NewsItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TheConsultancyFirm.Models.Tag", "Tag")
                        .WithMany("NewsItemTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.Slide", b =>
                {
                    b.HasOne("TheConsultancyFirm.Models.CarouselBlock", "Block")
                        .WithMany("Slides")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheConsultancyFirm.Models.SolutionTag", b =>
                {
                    b.HasOne("TheConsultancyFirm.Models.Solution", "Solution")
                        .WithMany("SolutionTags")
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TheConsultancyFirm.Models.Tag", "Tag")
                        .WithMany("SolutionTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
