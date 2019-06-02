using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projeto.BLL;
using Projeto.Entities;
using Projeto.Presentation.Models;
using System;
using System.Web.Mvc;
using System.Text;
using Projeto.Presentation.Reports;
using System.Diagnostics;

namespace Projeto.Presentation.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Cadastro()
        {
            return View(new ProdutoCadastroModel());
        }

        [HttpPost]
        public ActionResult Cadastro(ProdutoCadastroModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Produto produto = new Produto();
                    produto.Nome = model.Nome;
                    produto.Preco = model.Preco;
                    produto.Quantidade = model.Quantidade;
                    produto.IdEstoque = model.IdEstoque;

                    ProdutoBusiness business = new ProdutoBusiness();
                    business.CadastrarProduto(produto);

                    TempData["Mensagem"] = $"Produto ' {produto.Nome} ' , cadastrado com  sucesso.";
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;

                }
            }

            return View(new ProdutoCadastroModel());
        }

        public ActionResult Consulta()
        {
            List<ProdutoConsultaModel> lista = new List<ProdutoConsultaModel>();

            try
            {
                ProdutoBusiness business = new ProdutoBusiness();
                foreach (Produto produto in business.ConsultarProduto())
                {
                    ProdutoConsultaModel model = new ProdutoConsultaModel();
                    model.Estoque = new EstoqueConsultaModel();
                    model.IdProduto = produto.IdProduto;
                    model.Nome = produto.Nome;
                    model.Preco = produto.Preco;
                    model.Quantidade = produto.Quantidade;
                    model.Estoque.IdEstoque = produto.Estoque.IdEstoque;
                    model.Estoque.Nome = produto.Estoque.Nome;
                    model.Total = produto.Preco * produto.Quantidade;


                    lista.Add(model);
                }
            }
            catch (Exception e)
            {

                TempData["Mensagem"] = e.Message;
            }


            return View(lista);
        }


        public ActionResult Exclusao(int id)
        {
            try
            {
                ProdutoBusiness business = new ProdutoBusiness();
                business.ExcluirProduto(id);

                TempData["Mensagem"] = "Produto Excluído com sucesso";
            }
            catch(Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }


            return RedirectToAction("Consulta");
        }


        public ActionResult Edicao(int id)

        {
            ProdutoEdicaoModel model = new ProdutoEdicaoModel();
            try
            {
                ProdutoBusiness business = new ProdutoBusiness();
                Produto produto = business.ConsultarProdutoPorID(id);

               model.Nome = produto.Nome;
               model.Preco = produto.Preco;
               model.Quantidade = produto.Quantidade ;
               model.IdEstoque = produto.IdEstoque;

            }
            catch (Exception e)
            {

                TempData["Mensagem"] = e.Message;
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult Edicao(ProdutoEdicaoModel model)

        {
            if (ModelState.IsValid)
            {
                try
                {
                    Produto produto = new Produto();
                    produto.IdProduto = model.IdProduto;
                    produto.Nome = model.Nome;
                    produto.Preco = model.Preco;
                    produto.Quantidade = model.Quantidade;
                    produto.IdEstoque = model.IdEstoque;

                    ProdutoBusiness business = new ProdutoBusiness();
                    business.AtualizarProduto(produto);

                    ModelState.Clear();
                    TempData["Mensagem"] = $"Produto {produto.Nome} atualizado com sucesso";

                    return RedirectToAction("Consulta");
                }
                catch (Exception e)
                {

                    TempData["Mensagem"] = e.Message;

                }

            }
            return View(new ProdutoEdicaoModel());
        }


        public void Relatorio()
        {
            try
            {

                StringBuilder conteudo = new StringBuilder();
                conteudo.Append("<h2> Relatorio de Produtos </h2>");
                conteudo.Append($"Relatório gerado em: {DateTime.Now}");
                conteudo.Append("<br/><br/>");

                ProdutoBusiness business = new ProdutoBusiness();
                List<Produto> lista = business.ConsultarProduto();

                conteudo.Append("<table border='1' style='width=100%'>");

                conteudo.Append("<tr>");

                conteudo.Append("<th>Codigo</th>");
                conteudo.Append("<th>Nome</th>");
                conteudo.Append("<th>Preço</th>");
                conteudo.Append("<th>Quantidade</th>");
                conteudo.Append("<th>Estoque</th>");

                conteudo.Append("</tr>");


                foreach (Produto produto in lista)
                {
                    conteudo.Append("<tr>");

                    conteudo.Append($"<td>{produto.IdProduto}</td>");
                    conteudo.Append($"<td>{produto.Nome}</td>");
                    conteudo.Append($"<td>{produto.Preco}</td>");
                    conteudo.Append($"<td>{produto.Quantidade}</td>");
                    conteudo.Append($"<td>{produto.Estoque.Nome}</td>");

                    conteudo.Append("</tr>");
                }

                conteudo.Append("</table>");

                //converter PDF ...
                byte[] pdf = ReportUtil.GetPdfFile(conteudo.ToString());
                //download

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition",
                     "attachment; filename=relatorio.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(pdf);
                Response.End();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

    }






}