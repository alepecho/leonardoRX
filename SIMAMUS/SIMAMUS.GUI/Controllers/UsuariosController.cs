using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SIMAMUS.GUI.Models;

namespace SIMAMUS.GUI.Controllers
{
    public class UsuariosController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(Usuario model, string returnUrl/*[Bind(Include = "NombreUsuario,Contrasenna")] Usuario usuario*/)
        {

            var dataItem = db.Usuario.Where(x => x.NombreUsuario == model.NombreUsuario && x.Contrasenna == model.Contrasenna).FirstOrDefault();

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
                    if (dataItem.IdNivel == 1)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index2", "Home");
                    }
                }
            }
            else
            {
                //ViewBag.Mensaje = "Usuario o contraseña invalidos.";
                ModelState.AddModelError("", "Usuario o contraseña invalidos");
                return View();
            }
        }


        [Authorize]
        public ActionResult Salir()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Usuarios");
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.IdNivel = new SelectList(db.NivelUsuario, "IdNivel", "Descripcion");
            ViewBag.Cedula = new SelectList(db.Persona, "Cedula", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NombreUsuario,Contrasenna,IdNivel,Cedula")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdNivel = new SelectList(db.NivelUsuario, "IdNivel", "Descripcion", usuario.IdNivel);
            ViewBag.Cedula = new SelectList(db.Persona, "Cedula", "Nombre", usuario.Cedula);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdNivel = new SelectList(db.NivelUsuario, "IdNivel", "Descripcion", usuario.IdNivel);
            ViewBag.Cedula = new SelectList(db.Persona, "Cedula", "Nombre", usuario.Cedula);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NombreUsuario,Contrasenna,IdNivel,Cedula")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdNivel = new SelectList(db.NivelUsuario, "IdNivel", "Descripcion", usuario.IdNivel);
            ViewBag.Cedula = new SelectList(db.Persona, "Cedula", "Nombre", usuario.Cedula);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
