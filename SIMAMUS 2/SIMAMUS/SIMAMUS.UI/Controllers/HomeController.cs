using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SIMAMUS.UI.Models;

namespace SIMAMUS.UI.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usu, string returnUrl)
        {
            SIMAMUSEntities db = new SIMAMUSEntities();
            var dataItem = db.Usuario.Where(x => x.NombreUsuario == usu.NombreUsuario && x.Contrasenna == usu.Contrasenna).First();
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.NombreUsuario, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuario o contraseña invalido.");
                return View();
            }
        }
    }
}