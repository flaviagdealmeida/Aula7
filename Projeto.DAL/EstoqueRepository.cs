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
    public class EstoqueRepository
    {

        private string connectionString;


        public EstoqueRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["projeto"].ConnectionString;
        }


        public void Insert(Estoque estoque)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "insert into Estoque(Nome) values(@Nome)";
                conn.Execute(query, estoque);
            }
        }

        public void Update(Estoque estoque)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "update Estoque set Nome = @Nome where IdEstoque = @IdEstoque";
                conn.Execute(query, estoque);
            }
        }

        public void Delete(int idEstoque)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "delete from Estoque where IdEstoque = @IdEstoque";
                conn.Execute(query, new { IdEstoque = idEstoque });
                conn.Execute(query);

            }
        }


        public List<Estoque> FindAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "select * from Estoque";
                return conn.Query<Estoque>(query).ToList();
               

            }
        }


        public Estoque FindByID(int idEstoque)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "select * from Estoque where IdEstoque = @IdEstoque";
                return conn.QuerySingleOrDefault<Estoque> (query, new { IdEstoque = idEstoque });
           
            }
        }


        public bool HasEstoque(string nome)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "select count (Nome) from Estoque where Nome = @Nome";

                return conn.QuerySingleOrDefault<int>(query, new { Nome = nome }) > 0;
                
            }
        }

    public int CountProdutos(int idEstoque)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "select count (*) from Produto where IdEstoque = @IdEstoque";

                return conn.QuerySingleOrDefault<int>(query, new { IdEstoque = idEstoque });

            }
        }


    }
}
