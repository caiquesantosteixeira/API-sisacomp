using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class Agenda
    {
        public int IdAgenda { get; set; }
        public int IdTurma { get; set; }
        public string Texto { get; set; }
        public string Titulo { get; set; }
        public DateTime Data { get; set; }

        public virtual Turma IdTurmaNavigation { get; set; }
    }
}
