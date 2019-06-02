using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //validações

namespace Projeto.Presentation.Models
{
    public class EstoqueEdicaoModel
    {


        public int IdEstoque { get; set; }

        [RegularExpression("^[A-Za-zÀ-Üà-ü0-9\\s]{6,150}$", ErrorMessage = "Por favor, informe um nome válido")]//permissao de caracteres para criação do texto
        [Required(ErrorMessage = "Por Favor, informe o nome do Produto!")]
        public string Nome { get; set; }


      
      





    }
}