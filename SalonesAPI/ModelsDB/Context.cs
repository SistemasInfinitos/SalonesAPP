using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SalonesAPI.ModelsDB
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Ciudade> Ciudades { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Edade> Edades { get; set; }
        public virtual DbSet<Motivo> Motivos { get; set; }
        public virtual DbSet<Paise> Paises { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Salone> Salones { get; set; }
        public virtual DbSet<ViewSolicitudesPorFecha> ViewSolicitudesPorFechas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WIN-DESARROLLO\\DEVSQLSERVER;Database=pruebas;user=simplexwebuser;Password=Ic3b3rg2021**;trustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Ciudade>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.ciudadNombre)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true);

                entity.HasOne(d => d.idDepartamentoNavigation)
                    .WithMany(p => p.Ciudades)
                    .HasForeignKey(d => d.idDepartamento)
                    .HasConstraintName("CiudadesDepartamentos");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DistritoDepartamento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Departamentos)
                    .HasForeignKey(d => d.IdPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DepartamentosPais");
            });

            modelBuilder.Entity<Edade>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.fechaActualizacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.fechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Motivo>(entity =>
            {
                entity.Property(e => e.estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.fechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.fechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.motivo1)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("motivo");
            });

            modelBuilder.Entity<Paise>(entity =>
            {
                entity.HasKey(e => e.id)
                    .HasName("PK_Pais")
                    .IsClustered(false);

                entity.HasIndex(e => e.paisCodigo, "UK_Pais")
                    .IsUnique();

                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.paisCodigo)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true);

                entity.Property(e => e.paisContinente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('America del Sur')");

                entity.Property(e => e.paisNombre)
                    .IsRequired()
                    .HasMaxLength(52)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasIndex(e => e.identificacion, "UQ_PersonasIdentida")
                    .IsUnique();

                entity.Property(e => e.correo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.fechaActualizacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.fechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.identificacion)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.primerApellido)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.primerNombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.segundoApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.segundoNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.telefono)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.idCiudadNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.idCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Personas");
            });

            modelBuilder.Entity<Salone>(entity =>
            {
                entity.Property(e => e.estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.fechaActualizacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.fechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.fechaEvento)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.observacion).IsRequired();

                entity.HasOne(d => d.idMotivoNavigation)
                    .WithMany(p => p.Salones)
                    .HasForeignKey(d => d.idMotivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Motivos");

                entity.HasOne(d => d.idPersonaClienteNavigation)
                    .WithMany(p => p.Salones)
                    .HasForeignKey(d => d.idPersonaCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salones");
            });

            modelBuilder.Entity<ViewSolicitudesPorFecha>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewSolicitudesPorFecha");

                entity.Property(e => e.DistritoDepartamento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ciudadNombre)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.correo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.fechaEvento).HasColumnType("datetime");

                entity.Property(e => e.fechaEventoTex)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.identificacion)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.motivo)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.observacion).IsRequired();

                entity.Property(e => e.paisNombre)
                    .IsRequired()
                    .HasMaxLength(52)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.primerApellido)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.primerNombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.segundoApellido)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.segundoNombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.telefono)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
