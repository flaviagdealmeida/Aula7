using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //validações
using System.Web.Mvc;
using Projeto.BLL;
using Projeto.Entities;

namespace Projeto.Presentation.Models
{
    public class ProdutoCadastroModel
    {

        [RegularExpression("^[A-Za-zÀ-Üà-ü0-9\\s]{6,150}$", ErrorMessage ="Por favor, informe um nome válido")]//permissao de caracteres para criação do texto
        [Required(ErrorMessage ="Por Favor, informe o nome do produto!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por Favor, informe o preco do produto!")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Por Favor, informe a quantidade do produto!")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Por Favor, selecione um Estoque!")]
        public int IdEstoque { get; set; }

        public List<SelectListItem> Estoques {
            get
            {
                List<SelectListItem> lista = new List<SelectListItem>();

                EstoqueBusiness business = new EstoqueBusiness();
                foreach (Estoque estoque in business.ConsultarEstoque())
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = estoque.IdEstoque.ToString();
                    item.Text = estoque.Nome;

                    lista.Add(item);
                }

                return lista;

            }
        }


    }
}