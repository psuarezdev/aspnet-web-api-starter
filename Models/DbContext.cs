using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace AspNetWebApiStarter.Models;

public partial class DatabaseContext : DbContext
{
  public DatabaseContext() {}

  public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

  public virtual DbSet<User> Users { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlServer();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>(entity =>
    {
      entity.HasKey(e => e.Id).HasName("PK__User__3214EC0758FE291D");

      entity.ToTable("User");

      entity.Property(e => e.FirstName)
        .HasMaxLength(50)
        .IsUnicode(false);

      entity.Property(e => e.LastName)
        .HasMaxLength(50)
        .IsUnicode(false);

      entity.Property(e => e.Email)
        .IsUnicode(false)
        .HasMaxLength(255)
        .IsRequired();

      entity.HasIndex(e => e.Email)
        .IsUnique();

      entity.Property(e => e.Password)
        .IsUnicode(false)
        .IsRequired();
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
