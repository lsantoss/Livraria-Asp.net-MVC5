using Livraria.App_Start;
using Livraria.DAOs;
using Livraria.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Livraria.Controllers
{
    [Autenticacao]
    [HandleError(View = "Error", ExceptionType = typeof(InvalidOperationException))]
    public class LivroController : Controller
    {
        private LivroDAO dao = new LivroDAO();

        // GET: Livro
        public ActionResult Index()
        {
            return View(dao.RetornarTodos());
        }

        // GET: Livro/BuscarPorNome
        public ActionResult BuscarPorNome()
        {
            string busca = Request.Form["CPBusca"].ToString();
            return View("Index", dao.RetornarPorNome(busca));
        }

        // GET: Livro/Details/5
        public PartialViewResult Details(int id)
        {
            return PartialView(dao.RetornarPorId(id));
        }

        // GET: Livro/Create
        public PartialViewResult Create()
        {
            return PartialView();
        }

        // POST: Livro/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase arquivo)
        {
            Livro objeto = new Livro();
            UpdateModel(objeto);

            if (arquivo != null && arquivo.ContentLength > 0)
            {
                var fileName = Path.GetFileName(arquivo.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Imagens/"), fileName);
                arquivo.SaveAs(path);
                objeto.Imagem = arquivo.FileName;
            }
            else
            {
                var fileName = "Sem Capa.jpg";
                objeto.Imagem = fileName;
            }

            dao.Inserir(objeto);
            
            TempData["success"] = "Livro inserido com sucesso!";
            return RedirectToAction("Index");
        }

        // GET: Livro/Edit/5
        public PartialViewResult Edit(int id)
        {
            return PartialView(dao.RetornarPorId(id));
        }

        // POST: Livro/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, HttpPostedFileBase arquivo)
        {
            Livro objeto = new Livro();
            UpdateModel(objeto);

            if (arquivo != null && arquivo.ContentLength > 0)
            {
                var fileName = Path.GetFileName(arquivo.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Imagens/"), fileName);
                arquivo.SaveAs(path);
                objeto.Imagem = arquivo.FileName;
            }
            else
            {
                var fileName = "Sem Capa.jpg";
                objeto.Imagem = fileName;
            }

            dao.Alterar(objeto);

            TempData["success"] = "Livro editado com sucesso!";
            return RedirectToAction("Index");
        }

        // GET: Livro/Delete/5
        public PartialViewResult Delete(int id)
        {
            return PartialView(dao.RetornarPorId(id));
        }

        // POST: Livro/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            dao.Deletar(id);
            TempData["success"] = "Livro apagado com sucesso!";
            return RedirectToAction("Index");
        }
    }
}
