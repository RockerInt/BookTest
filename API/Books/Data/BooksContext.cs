using System;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Books.Data
{
    public partial class BooksContext : DbContext
    {
        public BooksContext()
        {
        }

        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autores> Autores { get; set; }
        public virtual DbSet<AutoresHasLibros> AutoresHasLibros { get; set; }
        public virtual DbSet<Editoriales> Editoriales { get; set; }
        public virtual DbSet<Libros> Libros { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Books;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Autores>(entity =>
            {
                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AutoresHasLibros>(entity =>
            {
                entity.HasKey(e => new { e.AutoresId, e.LibrosIsbn });

                entity.ToTable("Autores_has_Libros");

                entity.Property(e => e.AutoresId).HasColumnName("Autores_Id");

                entity.Property(e => e.LibrosIsbn).HasColumnName("Libros_ISBN");

                entity.HasOne(d => d.Autores)
                    .WithMany(p => p.AutoresHasLibros)
                    .HasForeignKey(d => d.AutoresId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autores_has_Libros_Autores");

                entity.HasOne(d => d.Libros)
                    .WithMany(p => p.AutoresHasLibros)
                    .HasForeignKey(d => d.LibrosIsbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autores_has_Libros_Libros");
            });

            modelBuilder.Entity<Editoriales>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Sede)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Libros>(entity =>
            {
                entity.HasKey(e => e.Isbn);

                entity.Property(e => e.Isbn).HasColumnName("ISBN");

                entity.Property(e => e.EditorialesId).HasColumnName("Editoriales_Id");

                entity.Property(e => e.NPaginas)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("N_Paginas");

                entity.Property(e => e.Sinopsis)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.HasOne(d => d.Editoriales)
                    .WithMany(p => p.Libros)
                    .HasForeignKey(d => d.EditorialesId)
                    .HasConstraintName("FK_Libros_Editoriales");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
