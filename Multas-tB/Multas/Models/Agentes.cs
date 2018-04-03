using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Agentes {
        public Agentes()
        {
            ListaDeMultas = new HashSet<Multas>();
        }

        [Key]
        public int ID { get; set; }//chave primária

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório")] // Anotador que faz o campo ser de preenchimento obrigatório
        [RegularExpression("[A-ZÁÍÉÂ][a-záéíóúàèìòùâêîôûäëïöüãõç]+(( | '|-| dos | da | de | e | d')[A-ZÁÍÉÂ][a-záéíóúàèìòùâêîôûäëïöüãõç]+){1,3}",
            ErrorMessage = "O {0} apenas pode conter letras e espaços em branco. Cada palavra começa em Maiúscula, deguida de minúscula")] // Expressão regular(é um filtro) - filtro que vai validar nomes
        public string Nome { get; set; }

        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório")]
        [RegularExpression("[A-ZÁÍÉÂ]*[a-záéíóúàèìòùâêîôûäëïöüãõç -]*", 
            ErrorMessage = "A {0} apenas pode conter letras e espaços em branco. Cada palavra começa em Maiúscula, deguida de minúscula")]
        public string Esquadra { get; set; }

        public string Fotografia { get; set; }

        // referência às multas que um Agente 'emite'
        public virtual ICollection<Multas> ListaDeMultas { get; set; }

    }
}