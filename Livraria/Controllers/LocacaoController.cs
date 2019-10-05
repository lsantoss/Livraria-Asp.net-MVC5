using Livraria.App_Start;
using Livraria.DAOs;
using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Livraria.Controllers
{
    [Autenticacao]
    [HandleError(View = "Error", ExceptionType = typeof(InvalidOperationException))]
    public class LocacaoController : Controller
    {
        private LocacaoDAO dao = new LocacaoDAO();

        // GET: Locacao
        public ActionResult Index()
        {
            return View(dao.RetornarTodos());
        }

        // GET: Locacao/BuscarPorNome
        public ActionResult BuscarPorNome()
        {
            string busca = Request.Form["CPBusca"].ToString();
            return View("Index", dao.RetornarPorNome(busca));
        }

        // GET: Locacao/BuscarPorNomeAlugados
        public ActionResult BuscarPorNomeAlugados()
        {
            string busca = Request.Form["CPBusca"].ToString();
            return View("ListaAlugados", dao.RetornarPorNomeAlugados(busca));
        }

        // GET: Locacao/BuscarPorNomeEntregues
        public ActionResult BuscarPorNomeEntregues()
        {
            string busca = Request.Form["CPBusca"].ToString();
            return View("ListaEntregues", dao.RetornarPorNomeEntregues(busca));
        }

        // GET: Locacao/ListaAlugados
        public ActionResult ListaAlugados()
        {
            return View(dao.ListaAlugados());
        }

        // GET: Locacao/ListaEntregues
        public ActionResult ListaEntregues()
        {
            return View(dao.ListaEntregues());
        }

        // GET: Locacao/Details/5
        public PartialViewResult Details(int id)
        {
            Locacao locacao = dao.RetornarPorId(id);
            ViewBag.DataE = locacao.Entrega;
            ViewBag.Preco = String.Format("{0:n}", locacao.Preco);
            return PartialView(dao.RetornarPorId(id));
        }

        // GET: Locacao/Create
        public PartialViewResult Create()
        {
            ClienteDAO cliDAO = new ClienteDAO();
            IEnumerable<Cliente> clientes = cliDAO.RetornarTodos();
            ViewBag.Clientes = new SelectList(clientes,"Id","Nome");

            LivroDAO livroDAO = new LivroDAO();
            IEnumerable<Livro> livros = livroDAO.RetornarNaoAlugados();
            ViewBag.Livros = new SelectList(livros,"Id", "Nome");

            return PartialView();
        }

        // POST: Locacao/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Locacao objeto = new Locacao();
            UpdateModel(objeto);

            DateTime dataC = Convert.ToDateTime(Request.Form["CPData"].ToString());
            objeto.Data = dataC;

            dao.Inserir(objeto);

            TempData["success"] = "Locação inserida com sucesso!";
            return RedirectToAction("Index");
        }
        

        // GET: Locacao/Delete/5
        public PartialViewResult Delete(int id)
        {
            Locacao locacao = dao.RetornarPorId(id);
            ViewBag.DataE = locacao.Entrega;
            ViewBag.Preco = String.Format("{0:n}", locacao.Preco);
            return PartialView(dao.RetornarPorId(id));
        }

        // POST: Locacao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            dao.Deletar(id);
            TempData["success"] = "Locação apagada com sucesso!";
            return RedirectToAction("Index");

        }

        // GET: Locacao/Finalizar/5
        public PartialViewResult Finalizar(int id)
        {
            return PartialView(dao.RetornarPorId(id));
        }

        // POST: Locacao/Edit/5
        [HttpPost]
        public ActionResult Finalizar(int id, FormCollection collection)
        {
            Locacao objeto = new Locacao();
            UpdateModel(objeto);

            var data = Request.Form["CPData"].ToString();
            DateTime dataC = Convert.ToDateTime(data);
            objeto.Entrega = dataC;

            dao.Finalizar(objeto);

            TempData["success"] = "Locação finalizada com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
