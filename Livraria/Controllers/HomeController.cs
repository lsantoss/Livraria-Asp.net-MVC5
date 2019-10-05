using Livraria.App_Start;
using Livraria.DAOs;
using Livraria.Models;
using System;
using System.Net.Mail;
using System.Web.Mvc;

namespace Livraria.Controllers
{
    [Autenticacao]
    [HandleError(View = "Error", ExceptionType = typeof(InvalidOperationException))]
    public class HomeController : Controller
    {
        private ClienteDAO dao = new ClienteDAO();

        // GET: Home
        public ActionResult Index()
        {
            return View(dao.RetornarTodos());
        }

        // GET: Home/BuscarPorNome
        public ActionResult BuscarPorNome()
        {
            string busca = Request.Form["CPBusca"].ToString();
            return View("Index", dao.RetornarPorNome(busca));
        }

        // GET: Home/Details/5
        public PartialViewResult Details(int id)
        {
            return PartialView(dao.RetornarPorId(id));
        }

        // GET: Home/Create
        public PartialViewResult Create()
        {
            return PartialView();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            Cliente objeto = new Cliente();
            UpdateModel(objeto);
            dao.Inserir(objeto);
            TempData["success"] = "Cliente inserido com sucesso!";
            return RedirectToAction("Index");
        }

        // GET: Home/Edit/5
        public PartialViewResult Edit(int id)
        {
            return PartialView(dao.RetornarPorId(id));
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Cliente objeto = new Cliente();
            UpdateModel(objeto);
            dao.Alterar(objeto);
            TempData["success"] = "Cliente editado com sucesso!";
            return RedirectToAction("Index");
        }

        // GET: Home/Delete/5
        public PartialViewResult Delete(int id)
        {
            return PartialView(dao.RetornarPorId(id));
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            dao.Deletar(id);
            TempData["success"] = "Cliente apgado com sucesso!";
            return RedirectToAction("Index");
        }

        // GET: /Home/Email
        public ActionResult Email()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Email(FormCollection collection)
        {
            Email objeto = new Email();
            UpdateModel(objeto);
            objeto.To = "lucas.faria@viannasempre.com.br";
            objeto.From = "lucas.faria@viannasempre.com.br";
            
            MailMessage mail = new MailMessage();
            mail.To.Add(objeto.To);
            mail.From = new MailAddress(objeto.From);
            mail.Subject = objeto.Subject;
            string Body = objeto.Body;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential ("lucas.faria@viannasempre.com.br", "");
            smtp.EnableSsl = true;
            smtp.Send(mail);

            TempData["info"] = "Email enviado com sucesso!";
            return RedirectToAction("Index", "Livro");
        }
    }
}
