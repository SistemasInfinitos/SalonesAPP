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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=WIN-DESARROLLO\\DEVSQLSERVER;Database=pruebas;user=simplexwebuser;Password=Ic3b3rg2021**;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Ciudade>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CiudadNombre)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("ciudadNombre")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true);

                entity.Property(e => e.CodigoDian).HasColumnName("codigoDian");

                entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Ciudades)
                    .HasForeignKey(d => d.IdDepartamento)
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
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaActualizacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCreacion")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Motivo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaActualizacion");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCreacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Motivo1)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("motivo");
            });

            modelBuilder.Entity<Paise>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_Pais")
                    .IsClustered(false);

                entity.HasIndex(e => e.PaisCodigo, "UK_Pais")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CodigoDian).HasColumnName("codigoDian");

                entity.Property(e => e.PaisCodigo)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("paisCodigo")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true);

                entity.Property(e => e.PaisContinente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("paisContinente")
                    .HasDefaultValueSql("('America del Sur')");

                entity.Property(e => e.PaisNombre)
                    .IsRequired()
                    .HasMaxLength(52)
                    .IsUnicode(false)
                    .HasColumnName("paisNombre")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasIndex(e => e.Identificacion, "UQ_PersonasIdentida")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("correo");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaActualizacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCreacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");

                entity.Property(e => e.Identificacion)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("identificacion");

                entity.Property(e => e.PrimerApellido)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("primerApellido");

                entity.Property(e => e.PrimerNombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("primerNombre");

                entity.Property(e => e.SegundoApellido)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("segundoApellido");

                entity.Property(e => e.SegundoNombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("segundoNombre");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdCiudadNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.IdCiudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Personas");
            });

            modelBuilder.Entity<Salone>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CantidadPersona).HasColumnName("cantidadPersona");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaEvento)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaEvento")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdMotivo).HasColumnName("idMotivo");

                entity.Property(e => e.IdPersonaCliente).HasColumnName("idPersonaCliente");

                entity.Property(e => e.Observacion)
                    .IsRequired()
                    .HasColumnName("observacion");

                entity.HasOne(d => d.IdMotivoNavigation)
                    .WithMany(p => p.Salones)
                    .HasForeignKey(d => d.IdMotivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Motivos");

                entity.HasOne(d => d.IdPersonaClienteNavigation)
                    .WithMany(p => p.Salones)
                    .HasForeignKey(d => d.IdPersonaCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Salones");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
