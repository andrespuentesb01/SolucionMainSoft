using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaVuelos.Entity;

namespace SistemaVuelos.DAL.DbContext;

public partial class DbNewShoreContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbNewShoreContext()
    {
    }

    public DbNewShoreContext(DbContextOptions<DbNewShoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblFlight> TblFlights { get; set; }

    public virtual DbSet<TblJourney> TblJourneys { get; set; }

    public virtual DbSet<TblTransport> TblTransports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblFlight>(entity =>
        {
            entity.Property(e => e.Destination)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Origin)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblJourney>(entity =>
        {
            entity.ToTable("TblJourney");

            entity.Property(e => e.Date).HasColumnType("date");
        });

        modelBuilder.Entity<TblTransport>(entity =>
        {
            entity.ToTable("TblTransport");

            entity.Property(e => e.FlightCarrier)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
