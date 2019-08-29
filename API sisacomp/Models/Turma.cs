using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class Turma
    {
        public Turma()
        {
            Agenda = new HashSet<Agenda>();
            Aluno = new HashSet<Aluno>();
            MateriaTurma = new HashSet<MateriaTurma>();
            TurmaNoticia = new HashSet<TurmaNoticia>();
        }

        public int IdTurma { get; set; }
        public int Serie { get; set; }
        public string Letra { get; set; }
        public int Turno { get; set; }
        public int Ativo { get; set; }
        public int TipoSerie { get; set; }

        public virtual ICollection<Agenda> Agenda { get; set; }
        public virtual ICollection<Aluno> Aluno { get; set; }
        public virtual ICollection<MateriaTurma> MateriaTurma { get; set; }
        public virtual ICollection<TurmaNoticia> TurmaNoticia { get; set; }
    }
}
