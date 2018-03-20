using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models{
    public class Viaturas{
        
        public Viaturas(){
            ListaDeMultas = new HashSet<Multas>();
        }

        [Key]
        public int ID { get; set; } //primary key

        //dados de uma Viatura
        public string Matricula { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Cor { get; set; }

        //dados do dono de uma viatura
        public string NomeDono { get; set; }

        public string MoradaDono { get; set; }

        public string CodPostalDono { get; set; }

        // referência às multas que um condutor 'recebe'
        public virtual ICollection <Multas> ListaDeMultas { get; set; }


    }
}