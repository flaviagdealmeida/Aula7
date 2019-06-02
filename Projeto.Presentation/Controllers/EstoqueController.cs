using Projeto.BLL;
using Projeto.Entities;
using Projeto.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web.Mvc;
using Projeto.Presentation.Reports;
using System.Web;

namespace Projeto.Presentation.Controllers
{
    public class EstoqueController : Controller
    {
        // GET: Estoque
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(EstoqueCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Estoque estoque = new Estoque();
                    estoque.Nome = model.Nome;

                    EstoqueBusiness business = new EstoqueBusiness();
                    business.CadastrarEstoque(estoque);

                    TempData["Mensagem"] = $"Estoque ' {estoque.Nome} ' , cadastrado com  sucesso.";
                    ModelState.Clear();

                    return RedirectToAction("Cadastro");

                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;

                }
            }


            return View();
        }


        public ActionResult Consulta()
        {
            List<EstoqueConsultaModel> lista = new List<EstoqueConsultaModel>();

            try
            {
                EstoqueBusiness business = new EstoqueBusiness();
                foreach (Estoque estoque in business.ConsultarEstoque())
                {
                    EstoqueConsultaModel model = new EstoqueConsultaModel();
                    model.IdEstoque = estoque.IdEstoque;
                    model.Nome = estoque.Nome;
                    model.QtdeProdutos = business.ObterQuantidadeDeProdutos(estoque.IdEstoque);

                    lista.Add(model);
                }
            }
            catch (Exception e)
            {

                TempData["Mensagem"] = e.Message;
            }


            return View(lista);
        }


        public ActionResult Edicao(int id)

        {
            EstoqueEdicaoModel model = new EstoqueEdicaoModel();
            try
            {
                EstoqueBusiness business = new EstoqueBusiness();
                Estoque estoque = business.ConsultarEstoquePorId(id);


                model.IdEstoque = estoque.IdEstoque;
                model.Nome = estoque.Nome;

            }
            catch (Exception e)
            {

                TempData["Mensagem"] = e.Message;
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult Edicao(EstoqueEdicaoModel model)

        {
            if (ModelState.IsValid)
            {
                try
                {
                    Estoque estoque = new Estoque();
                    estoque.IdEstoque = model.IdEstoque;
                    estoque.Nome = model.Nome;

                    EstoqueBusiness business = new EstoqueBusiness();
                    business.AtualizarEstoque(estoque);

                    TempData["Mensagem"] = "Estoque atualizado com sucesso";
                    return RedirectToAction("Consulta");
                }
                catch (Exception e)
                {

                    TempData["Mensagem"] = e.Message;

                }

            }
            return View();

        }

        public ActionResult Exclusao(int id)
        {
            try
            {
                EstoqueBusiness business = new EstoqueBusiness();
                business.ExcluirEstoque(id);

                TempData["Mensagem"] = "Estoque excluído com sucesso";
            }
            catch (Exception e)
            {

                TempData["Mensagem"] = e.Message;
            }
            return RedirectToAction("Consulta");
        }


        public void Relatorio()
        {
            try
            {

                StringBuilder conteudo = new StringBuilder();
                conteudo.Append("<h2> Relatorio de Estoques </h2>");
                conteudo.Append($"Relatório gerado em: {DateTime.Now}");
                conteudo.Append("<br/><br/>");

                EstoqueBusiness business = new EstoqueBusiness();
                List<Estoque> lista = business.ConsultarEstoque();

                conteudo.Append("<table border='1' style='width=100%'>");

                    conteudo.Append("<tr>");

                        conteudo.Append("<th>Codigo</th>");
                        conteudo.Append("<th>Nome do Estoque</th>");
                        conteudo.Append("<th>Quantidade de Produtos</th>");

                    conteudo.Append("</tr>");


                foreach (Estoque estoque in lista)
                {
                    conteudo.Append("<tr>");

                        conteudo.Append($"<td>{estoque.IdEstoque}</td>");
                        conteudo.Append($"<td>{estoque.Nome}</td>");
                        conteudo.Append($"<td>{business.ObterQuantidadeDeProdutos(estoque.IdEstoque)}</td>");

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