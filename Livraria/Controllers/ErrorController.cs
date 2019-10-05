using System.Web.Mvc;

namespace Livraria.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error/Padrao
        public ActionResult Padrao()
        {
            TempData["error"] = "Ocorreu um erro inesperado!";
            return View();
        }

        // GET: Error/NotFound
        public ActionResult NotFound()
        {
            TempData["error"] = "404 - Página não encontrada!";
            return View();
        }
    }
}
