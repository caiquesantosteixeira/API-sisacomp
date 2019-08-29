using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class MateriaTurma
    {
        public int Id { get; set; }
        public int IdMateria { get; set; }
        public int IdTurma { get; set; }

        public virtual Materia IdMateriaNavigation { get; set; }
        public virtual Turma IdTurmaNavigation { get; set; }
    }
}
