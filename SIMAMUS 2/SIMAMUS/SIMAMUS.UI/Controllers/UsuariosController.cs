using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SIMAMUS.UI.Models;

namespace SIMAMUS.UI.Controllers
{
    public class UsuariosController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuario = db.Usuario.Include(u => u.NivelUsuario).Include(u => u.Persona);
            return View(usuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
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
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,NombreUsuario,Contrasenna,IdNivel,IdPersona")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdNivel = new SelectList(db.NivelUsuario, "IdNivel", "Descripcion", usuario.IdNivel);
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", usuario.IdPersona);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", usuario.IdPersona);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,NombreUsuario,Contrasenna,IdNivel,IdPersona")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdNivel = new SelectList(db.NivelUsuario, "IdNivel", "Descripcion", usuario.IdNivel);
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", usuario.IdPersona);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
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

        public ActionResult Login()
        {
            return View();
        }
        /*
        [HttpPost]
        public ActionResult Login()
        {
            var dataItem = db.Usuario.Where(x => x.NombreUsuario == usuario.NombreUsuario && x.Contrasenna == usuario.Contrasenna).First();
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.NombreUsuario, false);
                if (Url.IsLocalUrl(returnURL) && returnURL.Length > 1 && returnURL.StartsWith("/")
                    && !returnURL.StartsWith("//") && !returnURL.StartsWith("/\\"))
                {
                    return Redirect(returnURL);
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
        */
    }
}
