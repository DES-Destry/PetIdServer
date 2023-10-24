﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PetIdServer.Infrastructure.Database;

#nullable disable

namespace PetIdServer.Infrastructure.Database.Migrations
{
    [DbContext(typeof(PetIdContext))]
    partial class PetIdContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("pet")
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PetIdServer.Infrastructure.Database.Models.AdminModel", b =>
                {
                    b.Property<string>("Username")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("username");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<DateTime?>("PasswordLastChangedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("password_last_changed_at");

                    b.HasKey("Username");

                    b.ToTable("admins", "pet");

                    b.HasData(
                        new
                        {
                            Username = "Andrey.Kirik",
                            CreatedAt = new DateTime(2023, 10, 3, 13, 8, 12, 716, DateTimeKind.Utc).AddTicks(3360)
                        });
                });

            modelBuilder.Entity("PetIdServer.Infrastructure.Database.Models.OwnerContactModel", b =>
                {
                    b.Property<string>("OwnerId")
                        .HasColumnType("text")
                        .HasColumnName("owner_id");

                    b.Property<string>("ContactType")
                        .HasColumnType("text")
                        .HasColumnName("contact_type");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("contact");

                    b.HasKey("OwnerId", "ContactType");

                    b.ToTable("owners_contacts", "pet");
                });

            modelBuilder.Entity("PetIdServer.Infrastructure.Database.Models.OwnerModel", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("Description")
                        .HasMaxLength(4096)
                        .HasColumnType("character varying(4096)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Email");

                    b.ToTable("owners", "pet");
                });

            modelBuilder.Entity("PetIdServer.Infrastructure.Database.Models.PetModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasMaxLength(4096)
                        .HasColumnType("character varying(4096)")
                        .HasColumnName("description");

                    b.Property<bool>("IsCastrated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_castrated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("name");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("owner_id");

                    b.Property<string>("Photo")
                        .HasColumnType("text")
                        .HasColumnName("photo");

                    b.Property<bool>("Sex")
                        .HasColumnType("boolean")
                        .HasColumnName("sex");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("pets", "pet");
                });

            modelBuilder.Entity("PetIdServer.Infrastructure.Database.Models.TagModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("PetId")
                        .HasColumnType("uuid")
                        .HasColumnName("pet_id");

                    b.Property<string>("SecurityCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("security_code");

                    b.HasKey("Id");

                    b.HasIndex("PetId");

                    b.ToTable("tags", "pet");
                });

            modelBuilder.Entity("PetIdServer.Infrastructure.Database.Models.OwnerContactModel", b =>
                {
                    b.HasOne("PetIdServer.Infrastructure.Database.Models.OwnerModel", "Owner")
                        .WithMany("Contacts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PetIdServer.Infrastructure.Database.Models.PetModel", b =>
                {
                    b.HasOne("PetIdServer.Infrastructure.Database.Models.OwnerModel", "Owner")
                        .WithMany("Pets")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("PetIdServer.Infrastructure.Database.Models.TagModel", b =>
                {
                    b.HasOne("PetIdServer.Infrastructure.Database.Models.PetModel", "Pet")
                        .WithMany("Tags")
                        .HasForeignKey("PetId");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("PetIdServer.Infrastructure.Database.Models.OwnerModel", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("Pets");
                });

            modelBuilder.Entity("PetIdServer.Infrastructure.Database.Models.PetModel", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
