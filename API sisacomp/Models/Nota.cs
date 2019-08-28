using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class Nota
    {
        public int IdNota { get; set; }
        public int? IdAluno { get; set; }
        public int? IdMateria { get; set; }
        public int? IdProva { get; set; }
        public decimal Nota1 { get; set; }

        public virtual Aluno IdAlunoNavigation { get; set; }
        public virtual Materia IdMateriaNavigation { get; set; }
        public virtual Prova IdProvaNavigation { get; set; }
    }
}
