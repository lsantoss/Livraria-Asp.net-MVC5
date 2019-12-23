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
        private readonly LocacaoDAO _dao = new LocacaoDAO();
        private readonly ClienteDAO _cliDAO = new ClienteDAO();
        private readonly LivroDAO _livroDAO = new LivroDAO();

        // GET: Locacao
        public ActionResult Index()
        {
            return View(_dao.RetornarTodos());
        }

        // GET: Locacao/BuscarPorNome
        public ActionResult BuscarPorNome()
        {
            string busca = Request.Form["CPBusca"].ToString();
            return View("Index", _dao.RetornarPorNome(busca));
        }

        // GET: Locacao/BuscarPorNomeAlugados
        public ActionResult BuscarPorNomeAlugados()
        {
            string busca = Request.Form["CPBusca"].ToString();
            return View("ListaAlugados", _dao.RetornarPorNomeAlugados(busca));
        }

        // GET: Locacao/BuscarPorNomeEntregues
        public ActionResult BuscarPorNomeEntregues()
        {
            string busca = Request.Form["CPBusca"].ToString();
            return View("ListaEntregues", _dao.RetornarPorNomeEntregues(busca));
        }

        // GET: Locacao/ListaAlugados
        public ActionResult ListaAlugados()
        {
            return View(_dao.ListaAlugados());
        }

        // GET: Locacao/ListaEntregues
        public ActionResult ListaEntregues()
        {
            return View(_dao.ListaEntregues());
        }

        // GET: Locacao/Details/5
        public PartialViewResult Details(int id)
        {
            Locacao locacao = _dao.RetornarPorId(id);
            ViewBag.DataE = locacao.Entrega;
            ViewBag.Preco = String.Format("{0:n}", locacao.Preco);
            return PartialView(_dao.RetornarPorId(id));
        }

        // GET: Locacao/Create
        public PartialViewResult Create()
        {
            IEnumerable<Cliente> clientes = _cliDAO.RetornarTodos();
            ViewBag.Clientes = new SelectList(clientes,"Id","Nome");

            IEnumerable<Livro> livros = _livroDAO.RetornarNaoAlugados();
            ViewBag.Livros = new SelectList(livros,"Id", "Nome");

            return PartialView();
        }

        // POST: Locacao/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Locacao locacao = new Locacao();
            UpdateModel(locacao);

            DateTime dataC = Convert.ToDateTime(Request.Form["CPData"].ToString());
            locacao.Data = dataC;

            _dao.Inserir(locacao);

            TempData["success"] = "Locação inserida com sucesso!";
            return RedirectToAction("Index");
        }
        

        // GET: Locacao/Delete/5
        public PartialViewResult Delete(int id)
        {
            Locacao locacao = _dao.RetornarPorId(id);
            ViewBag.DataE = locacao.Entrega;
            ViewBag.Preco = String.Format("{0:n}", locacao.Preco);
            return PartialView(_dao.RetornarPorId(id));
        }

        // POST: Locacao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            _dao.Deletar(id);

            TempData["success"] = "Locação apagada com sucesso!";
            return RedirectToAction("Index");

        }

        // GET: Locacao/Finalizar/5
        public PartialViewResult Finalizar(int id)
        {
            return PartialView(_dao.RetornarPorId(id));
        }

        // POST: Locacao/Edit/5
        [HttpPost]
        public ActionResult Finalizar(int id, FormCollection collection)
        {
            Locacao locacao = new Locacao();
            UpdateModel(locacao);

            var data = Request.Form["CPData"].ToString();
            DateTime dataC = Convert.ToDateTime(data);
            locacao.Entrega = dataC;

            _dao.Finalizar(locacao);

            TempData["success"] = "Locação finalizada com sucesso!";
            return RedirectToAction("Index");
        }
    }
}