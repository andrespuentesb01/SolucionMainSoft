using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SlnMain.Domain;

namespace SlnMain.Infrastructure;

public partial class DbRentCarContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbRentCarContext()
    {
    }

    public DbRentCarContext(DbContextOptions<DbRentCarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Collect> Collects { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Disponibility> Disponibilities { get; set; }

    public virtual DbSet<Rent> Rents { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ANDRESP01-PO; DataBase=dbRentCar;Integrated Security=true; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.ToTable("car");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Branch)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("branch");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("model");
            entity.Property(e => e.Plate)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("plate");
            entity.Property(e => e.Year)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("year");
        });

        modelBuilder.Entity<Collect>(entity =>
        {
            entity.ToTable("collect");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.IdSite).HasColumnName("idSite");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.ToTable("delivery");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.IdSite).HasColumnName("idSite");
        });

        modelBuilder.Entity<Disponibility>(entity =>
        {
            entity.ToTable("disponibility");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCar).HasColumnName("idCar");
            entity.Property(e => e.IdCollect).HasColumnName("idCollect");
            entity.Property(e => e.IdDelivery).HasColumnName("idDelivery");
        });

        modelBuilder.Entity<Rent>(entity =>
        {
            entity.ToTable("rent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comments)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdCar).HasColumnName("idCar");
            entity.Property(e => e.IdCollect).HasColumnName("idCollect");
            entity.Property(e => e.IdDelivery).HasColumnName("idDelivery");
            entity.Property(e => e.IdStatus).HasColumnName("idStatus");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.ToTable("site");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NameStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nameStatus");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cc");
            entity.Property(e => e.DrivePermision)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("drivePermision");
            entity.Property(e => e.Lastname)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
