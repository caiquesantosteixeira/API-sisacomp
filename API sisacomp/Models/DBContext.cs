using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_sisacomp.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Agenda> Agenda { get; set; }
        public virtual DbSet<Aluno> Aluno { get; set; }
        public virtual DbSet<Materia> Materia { get; set; }
        public virtual DbSet<Nota> Nota { get; set; }
        public virtual DbSet<Noticia> Noticia { get; set; }
        public virtual DbSet<Professor> Professor { get; set; }
        public virtual DbSet<Prova> Prova { get; set; }
        public virtual DbSet<Reclamacao> Reclamacao { get; set; }
        public virtual DbSet<Responsavel> Responsavel { get; set; }
        public virtual DbSet<Turma> Turma { get; set; }
        public virtual DbSet<TurmaNoticia> TurmaNoticia { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-7CAR991;Initial Catalog=sisacomp;User Id=sa;Password=#jamsoftsistemas1310;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.IdAdministrador)
                    .HasName("PK__administ__2B3E34A867DF7264");

                entity.ToTable("administrador");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Agenda>(entity =>
            {
                entity.HasKey(e => e.IdAgenda)
                    .HasName("PK__Agenda__FACC499E147646CA");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("datetime");

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTurmaNavigation)
                    .WithMany(p => p.Agenda)
                    .HasForeignKey(d => d.IdTurma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("turma_agenda");
            });

            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasKey(e => e.IdAluno)
                    .HasName("PK__Aluno__8092FCB31B21FBDB");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.DataNascimento).HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Ativo)
                     .IsRequired()
                     .IsUnicode(false);

                entity.HasOne(d => d.IdResponsavelNavigation)
                    .WithMany(p => p.Aluno)
                    .HasForeignKey(d => d.IdResponsavel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AlunoPai");

                entity.HasOne(d => d.IdTurmaNavigation)
                    .WithMany(p => p.Aluno)
                    .HasForeignKey(d => d.IdTurma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AlunoTurma");
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasKey(e => e.IdMateria)
                    .HasName("PK__Materia__EC17467034B46C0A");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ativo)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTurmaNavigation)
                    .WithMany(p => p.Materia)
                    .HasForeignKey(d => d.IdTurma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("turma_materia");
            });

            modelBuilder.Entity<Nota>(entity =>
            {
                entity.HasKey(e => e.IdNota)
                    .HasName("PK__Nota__4B2ACFF244246BA8");

                entity.Property(e => e.Nota1)
                    .HasColumnName("Nota")
                    .HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdAlunoNavigation)
                    .WithMany(p => p.Nota)
                    .HasForeignKey(d => d.IdAluno)
                    .HasConstraintName("ProvaAluno");

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.Nota)
                    .HasForeignKey(d => d.IdMateria)
                    .HasConstraintName("ProvaMate");

                entity.HasOne(d => d.IdProvaNavigation)
                    .WithMany(p => p.Nota)
                    .HasForeignKey(d => d.IdProva)
                    .HasConstraintName("Provaa");
            });

            modelBuilder.Entity<Noticia>(entity =>
            {
                entity.HasKey(e => e.IdNoticia)
                    .HasName("PK__Noticia__A6C949A8B416D517");

                entity.Property(e => e.CaminhoImagem)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTurmaNoticiaNavigation)
                    .WithMany(p => p.Noticia)
                    .HasForeignKey(d => d.IdTurmaNoticia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("NoticiaTurmaNoticia");
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(e => e.IdProfessor)
                    .HasName("PK__Professo__9D84BE1BB9F1461D");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.Professor)
                    .HasForeignKey(d => d.IdMateria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("professor_materia");
            });

            modelBuilder.Entity<Prova>(entity =>
            {
                entity.HasKey(e => e.IdProva)
                    .HasName("PK__Prova__C36300682DE3DC57");

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.Prova)
                    .HasForeignKey(d => d.IdMateria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProvaMateria");
            });

            modelBuilder.Entity<Reclamacao>(entity =>
            {
                entity.HasKey(e => e.IdReclamacao)
                    .HasName("PK__Reclamac__DF87F2464D6A18C7");

                entity.Property(e => e.Texto)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAlunoNavigation)
                    .WithMany(p => p.Reclamacao)
                    .HasForeignKey(d => d.IdAluno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ra");
            });

            modelBuilder.Entity<Responsavel>(entity =>
            {
                entity.HasKey(e => e.IdResponsavel)
                    .HasName("PK__responsa__CDF1DCAD0886AD8D");

                entity.ToTable("responsavel");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Endereco)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Turma>(entity =>
            {
                entity.HasKey(e => e.IdTurma)
                    .HasName("PK__Turma__C1ECFFC98B63908B");

                entity.Property(e => e.Letra)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
                entity.Property(e => e.Ativo)
                     .IsRequired()
                     .IsUnicode(false);
            });

            modelBuilder.Entity<TurmaNoticia>(entity =>
            {
                entity.HasKey(e => e.IdTurmaNoticia)
                    .HasName("PK__TurmaNot__795419A7C9BD1DE0");

                entity.HasOne(d => d.IdTurmaNavigation)
                    .WithMany(p => p.TurmaNoticia)
                    .HasForeignKey(d => d.IdTurma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("turmaTurmaNotinicia");
            });
        }
    }
}
