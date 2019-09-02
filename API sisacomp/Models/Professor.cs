using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_sisacomp.Models
{
    public partial class Professor
    {
        public Professor()
        {
            ProfessorMateria = new HashSet<ProfessorMateria>();
        }

        public int IdProfessor { get; set; }
        public int IdMateria { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public int Ativo { get; set; }
        [NotMapped]
        public Turma Turma { get; set; }
        [NotMapped]
        public List<Materia> Materia { get; set; }

        public virtual ICollection<ProfessorMateria> ProfessorMateria { get; set; }
    }
}
