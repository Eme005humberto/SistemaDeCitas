using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaDeCitas.Models;

public partial class SistemaCitasContext : DbContext
{
    public SistemaCitasContext()
    {
    }

    public SistemaCitasContext(DbContextOptions<SistemaCitasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCita> TblCitas { get; set; }

    public virtual DbSet<TblPaciente> TblPacientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__TblCitas__394B0202CC125557");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblPaciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__TblPacie__C93DB49BB9AF67FD");

            entity.Property(e => e.FechaCita).HasColumnType("date");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCitaNavigation).WithMany(p => p.TblPacientes)
                .HasForeignKey(d => d.IdCita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TblPacien__IdCit__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
