using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class Estoque
    {
        public int IdEstoque { get; set; }
        public string Nome { get; set; }

        //Relacionamento de Associação muitos
        public List<Produto> Produtos { get; set; }


        public Estoque()
        {

        }

        public Estoque(int idEstoque, string nome)
        {
            IdEstoque = idEstoque;
            Nome = nome;
        }

        public override string ToString()
        {
            return $"Id Estoque: {IdEstoque}, Nome:{Nome}";
        }
    }
}
