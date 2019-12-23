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
        private readonly ClienteDAO _dao = new ClienteDAO();

        // GET: Home
        public ActionResult Index()
        {
            return View(_dao.RetornarTodos());
        }

        // GET: Home/BuscarPorNome
        public ActionResult BuscarPorNome()
        {
            string busca = Request.Form["CPBusca"].ToString();
            return View("Index", _dao.RetornarPorNome(busca));
        }

        // GET: Home/Details/5
        public PartialViewResult Details(int id)
        {
            return PartialView(_dao.RetornarPorId(id));
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
            Cliente cliente = new Cliente();
            UpdateModel(cliente);
            _dao.Inserir(cliente);

            TempData["success"] = "Cliente inserido com sucesso!";
            return RedirectToAction("Index");
        }

        // GET: Home/Edit/5
        public PartialViewResult Edit(int id)
        {
            return PartialView(_dao.RetornarPorId(id));
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Cliente cliente = new Cliente();
            UpdateModel(cliente);
            _dao.Alterar(cliente);

            TempData["success"] = "Cliente editado com sucesso!";
            return RedirectToAction("Index");
        }

        // GET: Home/Delete/5
        public PartialViewResult Delete(int id)
        {
            return PartialView(_dao.RetornarPorId(id));
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            _dao.Deletar(id);

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
            Email email = new Email();
            UpdateModel(email);
            email.To = "lucas.faria@viannasempre.com.br";
            email.From = "lucas.faria@viannasempre.com.br";
            
            MailMessage mail = new MailMessage();
            mail.To.Add(email.To);
            mail.From = new MailAddress(email.From);
            mail.Subject = email.Subject;
            string Body = email.Body;
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