using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class Responsavel
    {
        public Responsavel()
        {
            Aluno = new HashSet<Aluno>();
        }

        public int IdResponsavel { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public int Ativo { get; set; }
        public string Senha { get; set; }

        public virtual ICollection<Aluno> Aluno { get; set; }
    }
}
