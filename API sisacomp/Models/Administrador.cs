using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class Administrador
    {
        public int IdAdministrador { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
    }
}
