using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class Reclamacao
    {
        public int IdReclamacao { get; set; }
        public int IdAluno { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }

        public virtual Aluno IdAlunoNavigation { get; set; }
    }
}
