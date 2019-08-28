using System;
using System.Collections.Generic;

namespace API_sisacomp.Models
{
    public partial class Prova
    {
        public Prova()
        {
            Nota = new HashSet<Nota>();
        }

        public int IdProva { get; set; }
        public int Bimestre { get; set; }
        public int IdMateria { get; set; }

        public virtual Materia IdMateriaNavigation { get; set; }
        public virtual ICollection<Nota> Nota { get; set; }
    }
}
