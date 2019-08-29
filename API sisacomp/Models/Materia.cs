using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_sisacomp.Models
{
    public partial class Materia
    {
        public Materia()
        {
            MateriaTurma = new HashSet<MateriaTurma>();
            Nota = new HashSet<Nota>();
            ProfessorMateria = new HashSet<ProfessorMateria>();
            Prova = new HashSet<Prova>();
        }

        public int IdMateria { get; set; }
        public string Nome { get; set; }
        public int Ativo { get; set; }

        [NotMapped]
        public int IdTurma { get; set; }

        [NotMapped]
        public int IdMateriaTurma { get; set; }

        [NotMapped]
        public ProfessorMateria IdProfessorMateria { get; set; }

        public virtual ICollection<MateriaTurma> MateriaTurma { get; set; }
        public virtual ICollection<Nota> Nota { get; set; }
        public virtual ICollection<ProfessorMateria> ProfessorMateria { get; set; }
        public virtual ICollection<Prova> Prova { get; set; }
    }
}
