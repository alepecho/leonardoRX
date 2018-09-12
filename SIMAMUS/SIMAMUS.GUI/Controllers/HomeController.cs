using SIMAMUS.GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIMAMUS.GUI.Controllers
{
    public class HomeController : Controller
    {

        private SIMAMUSEntities db = new SIMAMUSEntities();

        public ActionResult Menu()
        {
            if (User.Identity.IsAuthenticated)
            {
                string nombreUsr = User.Identity.Name;
                Usuario usr = db.Usuario.Where(x => x.NombreUsuario == nombreUsr).FirstOrDefault();
                switch (usr.IdNivel)
                {
                    case 1:
                        return RedirectToAction("Index", "Home");
                    case 2:
                        return RedirectToAction("Index2", "Home");
                    case 3:
                        return RedirectToAction("Index3", "Home");
                    case 4:
                        return RedirectToAction("Deasdfasdf","RegistroR"); //TODO
                    default:
                        return RedirectToAction("Login", "Usuarios");
                }
            }
            else
            {
                return RedirectToAction("Index", "Usuarios");
            }
        }

        [Authorize(Roles = "1")]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "2")]
        public ActionResult Index2()
        {
           return View();
        }

        [Authorize(Roles = "3")]
        public ActionResult Index3()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}