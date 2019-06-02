using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public int IdEstoque { get; set; } //para utilizar fins de insert e update da tabela

        //associação ter 1
        public Estoque Estoque { get; set; }


        public Produto()
        {
                
        }

        public Produto(int idProduto, string nome, decimal preco, int quantidade, int idEstoque)
        {
            IdProduto = idProduto;
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
            IdEstoque = idEstoque;
        }


        public override string ToString()
        {
            return $"Id Produto: {IdProduto}, Nome: {Nome}, Preço: {Preco}, Quantidade: {Quantidade}, Id Estoque: {IdEstoque}";
        }





    }
}
