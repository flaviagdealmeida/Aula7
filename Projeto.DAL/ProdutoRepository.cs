using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using Projeto.Entities;

namespace Projeto.DAL
{
    public class ProdutoRepository
    {

        public string connectionString;

        public ProdutoRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["projeto"].ConnectionString;

        }

        public void Insert (Produto produto)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "insert into Produto(Nome, Preco, Quantidade, IdEstoque) values (@Nome, @Preco, @Quantidade, @IdEstoque)";

                conn.Execute(query, produto);
            }
        }

        public void Update(Produto produto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "update Produto set Nome =@Nome , Preco =  @Preco, Quantidade =  @Quantidade, IdEstoque = @IdEstoque where IdProduto = @IdProduto ";

                conn.Execute(query, produto);
            }
        }

        public void Delete(int idProduto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "delete from Produto where IdProduto = @IdProduto";

                conn.Execute(query, new { IdProduto = idProduto});
            }
        }


        public List<Produto> FindAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "select * from Produto p inner join Estoque e on e.IdEstoque = p.IdEstoque";

                //sintaxe inner join
                return conn.Query(query,(Produto p, Estoque e) =>
                {
                    p.Estoque = e; //mapeamento da associação 
                    return p;// tipo qie sera retornado pela query
                },
                splitOn:"IdEstoque" //chave strangeira
                    ).ToList();
            }
        }


        public Produto FindById(int idProduto)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "select * from Produto p " +
                                "inner join Estoque e " +
                                "on e.IdEstoque = p.IdEstoque " +
                                "where IdProduto = @IdProduto";

                //sintaxe inner join
                return conn.Query(query, (Produto p, Estoque e) =>
                {
                    p.Estoque = e; //mapeamento da associação 
                    return p;// tipo qie sera retornado pela query
                },
                new {IdProduto = idProduto },
                splitOn: "IdEstoque" //chave strangeira
                    ).SingleOrDefault();
            }
        }



    }
}
