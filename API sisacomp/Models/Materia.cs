using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class Materia
    {
        public Materia()
        {
            Nota = new HashSet<Nota>();
            Professor = new HashSet<Professor>();
            Prova = new HashSet<Prova>();
        }

        public int IdMateria { get; set; }
        public string Nome { get; set; }
        public int IdTurma { get; set; }
        public int Ativo { get; set; }
        public virtual Turma IdTurmaNavigation { get; set; }
        public virtual ICollection<Nota> Nota { get; set; }
        public virtual ICollection<Professor> Professor { get; set; }
        public virtual ICollection<Prova> Prova { get; set; }
    }
}
