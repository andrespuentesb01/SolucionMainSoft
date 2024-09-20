using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SlnMain.Domain;

namespace SlnMain.Infrastructure;

public partial class DbCarvajalContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbCarvajalContext()
    {
    }

    public DbCarvajalContext(DbContextOptions<DbCarvajalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderHeader> OrderHeaders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ANDRESP01-PO;Database=dbCarvajal;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.ToTable("orderDetail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdHeader).HasColumnName("idHeader");
            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.IvaDetail)
                .HasColumnType("money")
                .HasColumnName("ivaDetail");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SubTotal)
                .HasColumnType("money")
                .HasColumnName("subTotal");
            entity.Property(e => e.TotalDetail)
                .HasColumnType("money")
                .HasColumnName("totalDetail");
        });

        modelBuilder.Entity<OrderHeader>(entity =>
        {
            entity.ToTable("orderHeader");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.IvaHeader)
                .HasColumnType("money")
                .HasColumnName("ivaHeader");
            entity.Property(e => e.SubTotal)
                .HasColumnType("money")
                .HasColumnName("subTotal");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IvaPercent).HasColumnName("ivaPercent");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblUsers");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
