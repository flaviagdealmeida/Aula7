using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //validações

namespace Projeto.Presentation.Models
{
    public class EstoqueConsultaModel
    {

        public int IdEstoque { get; set; }
        public string Nome { get; set; }
        public int QtdeProdutos { get; set; }
    }
}