using Livraria.App_Start;
using Livraria.DAOs;
using Livraria.Models;
using System;
using System.Web.Mvc;

namespace Livraria.Controllers
{
    [HandleError(View = "Error", ExceptionType = typeof(InvalidOperationException))]
    public class UsuarioController : Controller
    {
        private UsuarioDAO dao = new UsuarioDAO();

        // GET: Usuario
        [Autenticacao]
        public ActionResult Index()
        {
            return View(dao.RetornarTodos());
        }

        // GET: Usuario/BuscarPorNome
        [Autenticacao]
        public ActionResult BuscarPorNome()
        {
            string busca = Request.Form["CPBusca"].ToString();
            return View("Index", dao.RetornarPorNome(busca));
        }

        // GET: Usuario/Details/5
        [Autenticacao]
        public PartialViewResult Details(int id)
        {
            return PartialView(dao.RetornarPorId(id));
        }

        // GET: Usuario/Create
        [Autenticacao]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        // POST: Usuario/Create
        [Autenticacao]
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Usuario objeto = new Usuario();
            UpdateModel(objeto);
            dao.Inserir(objeto);
            TempData["success"] = "Usuário inserido com sucesso!";
            return RedirectToAction("Index");
        }

        // GET: Usuario/Edit/5
        [Autenticacao]
        public PartialViewResult Edit(int id)
        {
            return PartialView(dao.RetornarPorId(id));
        }

        // POST: Usuario/Edit/5
        [Autenticacao]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Usuario objeto = new Usuario();
            UpdateModel(objeto);
            dao.Alterar(objeto);
            TempData["success"] = "Usuário editado com sucesso!";
            return RedirectToAction("Index");
        }

        // GET: Usuario/Delete/5
        [Autenticacao]
        public PartialViewResult Delete(int id)
        {
            return PartialView(dao.RetornarPorId(id));
        }

        // POST: Usuario/Delete/5
        [Autenticacao]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            dao.Deletar(id);
            TempData["success"] = "Usuário apagado com sucesso!";
            return RedirectToAction("Index");
        }

        // GET: Usuario/Principal
        public ActionResult Principal()
        {
            return View();
        }

        // GET: Usuario/TelaLogar
        public ActionResult TelaLogar()
        {
            ViewBag.Priv = null;
            return View();
        }

        // POST: Usuario/Logar
        [HttpPost]
        public ActionResult Logar(FormCollection collection)
        {
            Usuario objeto = new Usuario();
            UpdateModel(objeto);

            Usuario usuario = dao.Logar(objeto);

            if (usuario != null)
            {
                Session["Usuario"] = usuario;                
                Session["Priv"] = usuario.Privilegio;

                DateTime validade = DateTime.Now;
                validade.AddHours(2);
                usuario.Validade = validade;
                dao.Alterar(usuario);

                TempData["info"] = "Bem-vindo "+ usuario.Login + "!";
                return RedirectToAction("Index", "Livro");
            }
            else
            {
                TempData["error"] = "Login e/ou senha incorreto!";
                return RedirectToAction("TelaLogar", "Usuario");
            }
        }

        // GET: Usuario/Deslogar
        [Autenticacao]
        public ActionResult Deslogar()
        {
            Session["Usuario"] = null;
            TempData["info"] = "Você foi deslgado!";
            return RedirectToAction("TelaLogar", "Usuario");
        }
    }
}
