using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SlnPactia.Domain;

namespace SlnPactia.Infrastructure.DbContext;

public partial class DbPactiaContext : Microsoft.EntityFrameworkCore.DbContext
{

    public DbPactiaContext()
    {
    }

    public DbPactiaContext(DbContextOptions<DbPactiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ListOfTask> ListOfTasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ANDRESP01-PO; DataBase=DbPactia;Integrated Security=true; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ListOfTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ListOfTask");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.NameOfTask)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
