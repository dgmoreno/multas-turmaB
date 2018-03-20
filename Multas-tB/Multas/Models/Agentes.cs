using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Agentes{
        public Agentes()
        {
            ListaDeMultas = new HashSet<Multas>();
        }

        [Key]
        public int ID { get; set; }//chave primária

        public string Nome { get; set; }

        public string Esquadra { get; set; }

        public string Fotografia { get; set; }

        // referência às multas que um Agente 'emite'
        public virtual ICollection<Multas> ListaDeMultas { get; set; }

    }
}