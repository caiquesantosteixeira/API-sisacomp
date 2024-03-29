﻿using System;
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
        public virtual DbSet<MateriaTurma> MateriaTurma { get; set; }
        public virtual DbSet<Nota> Nota { get; set; }
        public virtual DbSet<Noticia> Noticia { get; set; }
        public virtual DbSet<Professor> Professor { get; set; }
        public virtual DbSet<ProfessorMateria> ProfessorMateria { get; set; }
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
                    .HasName("PK__administ__2B3E34A8DB883792");

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
                    .HasName("PK__Agenda__FACC499E486DD526");

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
                    .HasName("PK__Aluno__8092FCB3C5616E66");

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
                    .HasName("PK__Materia__EC17467060CD4679");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MateriaTurma>(entity =>
            {
                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.MateriaTurma)
                    .HasForeignKey(d => d.IdMateria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("turm_materia");

                entity.HasOne(d => d.IdTurmaNavigation)
                    .WithMany(p => p.MateriaTurma)
                    .HasForeignKey(d => d.IdTurma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("turma_materia");
            });

            modelBuilder.Entity<Nota>(entity =>
            {
                entity.HasKey(e => e.IdNota)
                    .HasName("PK__Nota__4B2ACFF2908A7878");

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
                    .HasName("PK__Noticia__A6C949A89642AE5C");

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

            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(e => e.IdProfessor)
                    .HasName("PK__Professo__9D84BE1BA56D90A8");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProfessorMateria>(entity =>
            {
                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.ProfessorMateria)
                    .HasForeignKey(d => d.IdMateria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("professor_materia");

                entity.HasOne(d => d.IdProfessorNavigation)
                    .WithMany(p => p.ProfessorMateria)
                    .HasForeignKey(d => d.IdProfessor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("prof");
            });

            modelBuilder.Entity<Prova>(entity =>
            {
                entity.HasKey(e => e.IdProva)
                    .HasName("PK__Prova__C36300682A77917D");

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.Prova)
                    .HasForeignKey(d => d.IdMateria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProvaMateria");
            });

            modelBuilder.Entity<Reclamacao>(entity =>
            {
                entity.HasKey(e => e.IdReclamacao)
                    .HasName("PK__Reclamac__DF87F24644E2190E");

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
                    .HasName("PK__responsa__CDF1DCADF658A885");

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
                    .HasName("PK__Turma__C1ECFFC9EC8405F7");

                entity.Property(e => e.Letra)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TurmaNoticia>(entity =>
            {
                entity.HasKey(e => e.IdTurmaNoticia)
                    .HasName("PK__TurmaNot__795419A731AC2463");

                entity.HasOne(d => d.IdTurmaNavigation)
                    .WithMany(p => p.TurmaNoticia)
                    .HasForeignKey(d => d.IdTurma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("turmaTurmaNotinicia");
            });
        }
    }
}
