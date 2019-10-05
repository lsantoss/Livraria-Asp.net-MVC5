using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Livraria.App_Start
{
    public class Autenticacao : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        { 
            //throw new NotImplementedException();
        } 

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = HttpContext.Current.Session["Usuario"];

            if (user == null) {
                filterContext.Result = new RedirectResult("/Usuario/TelaLogar");
            }
        }
    }
 }