using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIMAMUS.GUI.Models;

namespace SIMAMUS.GUI.Controllers
{
    public class UsuarioController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "NombreUsuario,Contrasenna")] Usuario usuario)
        {
            List<Usuario> iUsuarios = db.Usuario.ToList();

            foreach (Usuario usu in iUsuarios)
            {
                if (usu.NombreUsuario == usuario.NombreUsuario && usu.Contrasenna == usuario.Contrasenna)
                {
                    System.Web.HttpContext.Current.Session["usuario"] = usu.IdUsuario;
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("IndexError", "Usuario");
        }

        public ActionResult IndexError()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndexError([Bind(Include = "NombreUsuario,Contrasenna")] Usuario usuario)
        {
            List<Usuario> iUsuarios = db.Usuario.ToList();

            foreach (Usuario usu in iUsuarios)
            {
                if (usu.NombreUsuario == usuario.NombreUsuario && usu.Contrasenna == usuario.Contrasenna)
                {
                    System.Web.HttpContext.Current.Session["usuario"] = usu.IdUsuario;
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(usuario);
        }


        // GET: Usuario/Details/5
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

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.IdNivel = new SelectList(db.NivelUsuario, "IdNivel", "Descripcion");
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre");
            return View();
        }

        // POST: Usuario/Create
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

        // GET: Usuario/Edit/5
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

        // POST: Usuario/Edit/5
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

        // GET: Usuario/Delete/5
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

        // POST: Usuario/Delete/5
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
    }
}
