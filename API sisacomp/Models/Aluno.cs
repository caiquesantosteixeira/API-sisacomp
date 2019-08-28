using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class Aluno
    {
        public Aluno()
        {
            Nota = new HashSet<Nota>();
            Reclamacao = new HashSet<Reclamacao>();
        }

        public int IdAluno { get; set; }
        public int IdTurma { get; set; }
        public int IdResponsavel { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public int Ativo { get; set; }

        public virtual Responsavel IdResponsavelNavigation { get; set; }
        public virtual Turma IdTurmaNavigation { get; set; }
        public virtual ICollection<Nota> Nota { get; set; }
        public virtual ICollection<Reclamacao> Reclamacao { get; set; }
    }
}
