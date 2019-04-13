﻿// <auto-generated />
using LoginScreen.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoginScreen.Migrations
{
    [DbContext(typeof(LoginScreenContext))]
    [Migration("20190408074626_Usuario")]
    partial class Usuario
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LoginScreen.Models.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("confirmarSenha")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<string>("nome")
                        .IsRequired();

                    b.Property<string>("nomeDeUsuario")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<string>("senha")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("id");

                    b.ToTable("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
