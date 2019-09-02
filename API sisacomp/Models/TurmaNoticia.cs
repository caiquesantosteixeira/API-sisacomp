using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class TurmaNoticia
    {
        public TurmaNoticia()
        {
        }

        public int IdTurmaNoticia { get; set; }
        public int IdTurma { get; set; }

        public virtual Turma IdTurmaNavigation { get; set; }
    }
}
