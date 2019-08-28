using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class Professor
    {
        public int IdProfessor { get; set; }
        public int IdMateria { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public virtual Materia IdMateriaNavigation { get; set; }
    }
}
