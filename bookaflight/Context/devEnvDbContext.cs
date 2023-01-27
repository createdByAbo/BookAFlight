using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

using BookAFlight.Entities;
using BookAFlight.Models.Functions;

namespace BookAFlight.Context;

public partial class devEnvDbContext : DbContext
{
    public devEnvDbContext()
    {
    }

    public devEnvDbContext(DbContextOptions<devEnvDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Fleet> Fleets { get; set; } = null!;
    public virtual DbSet<Flight> Flights { get; set; } = null!;
    public virtual DbSet<PassangerDatum> PassangerData { get; set; } = null!;
    public virtual DbSet<Passenger> Passengers { get; set; } = null!;
    public virtual DbSet<Role> Roles { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("devDb"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fleet>(entity =>
        {
            entity.ToTable("Fleet");

            entity.Property(e => e.Brand)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.Property(e => e.Model)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.Property(e => e.Registry)
                .HasMaxLength(9)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasIndex(e => e.FlightCode, "UQ_FlightCode")
                .IsUnique();

            entity.HasIndex(e => e.Id, "UQ_Id")
                .IsUnique();

            entity.Property(e => e.BeetweenAproche)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.BeetweenAprocheDate).HasColumnType("datetime");

            entity.Property(e => e.BuisnessClassSeatPrice).HasColumnType("money");

            entity.Property(e => e.EconomicClassSeatPrice).HasColumnType("money");

            entity.Property(e => e.EndAirport)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.EndCity)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.EndDate).HasColumnType("datetime");

            entity.Property(e => e.FirstClassSeatPrice).HasColumnType("money");

            entity.Property(e => e.FlightCode)
                .HasMaxLength(9)
                .IsUnicode(false);

            entity.Property(e => e.RegistredBaggagePrice).HasColumnType("money");

            entity.Property(e => e.StartAirport)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.StartCity)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Aircraft)
                .WithMany(p => p.Flights)
                .HasForeignKey(d => d.AircraftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AircraftId");
        });

        modelBuilder.Entity<PassangerDatum>(entity =>
        {
            entity.Property(e => e.DateOfBirth).HasColumnType("date");

            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.Property(e => e.PeselNumber).HasColumnType("numeric(11, 0)");

            entity.Property(e => e.SecondName)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.Property(e => e.SurName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.Property(e => e.SeatType)
                .HasMaxLength(14)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => new { e.Id, e.Username, e.Email, e.PeselNumber, e.PhoneNumber }, "UC_User")
                .IsUnique();

            entity.Property(e => e.DateOfBirth).HasColumnType("date");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.PeselNumber)
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(9)
                .IsUnicode(false);

            entity.Property(e => e.RoleId).HasDefaultValueSql("((1))");

            entity.Property(e => e.SecondName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.Role)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
