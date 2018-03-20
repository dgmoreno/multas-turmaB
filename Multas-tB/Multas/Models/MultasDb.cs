using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Multas.Models{
    //classe especial que representa uma base de dados
    public class MultasDb : DbContext {

        //construtor que indica qual a base de dados a utilizar
        public MultasDb():base("name=MultasDBConnectionString")
        {

        }

        //descrever os nomes das tabelas na Base de Dados
        
        public virtual DbSet<Multas> Multas { get; set; }//cria tabela multas

        public virtual DbSet<Condutores> Condutores { get; set; } //cria tabela condutores

        public virtual DbSet<Agentes> Agentes { get; set; }//cria tabela agentes

        public virtual DbSet<Viaturas> Viaturas { get; set; }//cria tabela viaturas




    }
}