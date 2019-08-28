using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class Noticia
    {
        public int IdNoticia { get; set; }
        public int IdTurmaNoticia { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string CaminhoImagem { get; set; }

        public virtual TurmaNoticia IdTurmaNoticiaNavigation { get; set; }
    }
}
