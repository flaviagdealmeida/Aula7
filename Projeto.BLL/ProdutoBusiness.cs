using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities;
using Projeto.DAL;

namespace Projeto.BLL
{
    public class ProdutoBusiness
    {

        public void CadastrarProduto(Produto produto)
        {
            ProdutoRepository repository = new ProdutoRepository();
            repository.Insert(produto);

        }


        public void AtualizarProduto(Produto produto)
        {
            ProdutoRepository repository = new ProdutoRepository();
            repository.Update(produto);

        }


        public void ExcluirProduto(int idProduto)
        {
            ProdutoRepository repository = new ProdutoRepository();
            repository.Delete(idProduto);

        }


        public List<Produto>ConsultarProduto()
        {
            ProdutoRepository repository = new ProdutoRepository();
            return repository.FindAll();

        }

        public Produto ConsultarProdutoPorID(int idProduto)
        {
            ProdutoRepository repository = new ProdutoRepository();
            return repository.FindById(idProduto);

        }


    }
}
