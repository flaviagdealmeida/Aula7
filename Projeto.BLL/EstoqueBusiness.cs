using Projeto.DAL;
using Projeto.Entities;
using System;
using System.Collections.Generic;

namespace Projeto.BLL
{
    public class EstoqueBusiness
    {

        public void CadastrarEstoque(Estoque estoque)
        {
            EstoqueRepository repository = new EstoqueRepository();
            if (!repository.HasEstoque(estoque.Nome))
            {
                repository.Insert(estoque);
            }
            else
            {
                throw new Exception($"O Estoque ' {estoque.Nome} ' já existe no sistema");
            }


        }


        public void AtualizarEstoque(Estoque estoque)
        {
            EstoqueRepository repository = new EstoqueRepository();
            repository.Update(estoque);

        }


        public void ExcluirEstoque(int idEstoque)
        {
            EstoqueRepository repository = new EstoqueRepository();

            //verificar se contrm produtos

            int qtdProdutos = repository.CountProdutos(idEstoque);
            if (qtdProdutos == 0)
            {
                repository.Delete(idEstoque);
            }
            else
            {
                throw new Exception($"Não é possivel excluir o estoque pois o mesmo possui {qtdProdutos} produto(s)");
            }
        }

        public Estoque ConsultarEstoquePorId(int idEstoque)
        {
            EstoqueRepository repository = new EstoqueRepository();
            return repository.FindByID(idEstoque);

        }

        public List<Estoque> ConsultarEstoque()
        {
            EstoqueRepository repository = new EstoqueRepository();
            return repository.FindAll();

        }

        //obter qtde de produtos no estoque
        public int ObterQuantidadeDeProdutos(int idEstoque)
        {
            EstoqueRepository repository = new EstoqueRepository();
            return repository.CountProdutos(idEstoque);
        }



    }
}
