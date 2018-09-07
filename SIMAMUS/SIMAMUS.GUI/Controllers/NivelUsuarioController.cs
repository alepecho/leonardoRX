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
    public class NivelUsuarioController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: NivelUsuario
        public ActionResult Index()
        {
            return View(db.NivelUsuario.ToList());
        }

        // GET: NivelUsuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelUsuario nivelUsuario = db.NivelUsuario.Find(id);
            if (nivelUsuario == null)
            {
                return HttpNotFound();
            }
            return View(nivelUsuario);
        }

        // GET: NivelUsuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NivelUsuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNivel,Descripcion")] NivelUsuario nivelUsuario)
        {
            if (ModelState.IsValid)
            {
                db.NivelUsuario.Add(nivelUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nivelUsuario);
        }

        // GET: NivelUsuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelUsuario nivelUsuario = db.NivelUsuario.Find(id);
            if (nivelUsuario == null)
            {
                return HttpNotFound();
            }
            return View(nivelUsuario);
        }

        // POST: NivelUsuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNivel,Descripcion")] NivelUsuario nivelUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nivelUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nivelUsuario);
        }

        // GET: NivelUsuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NivelUsuario nivelUsuario = db.NivelUsuario.Find(id);
            if (nivelUsuario == null)
            {
                return HttpNotFound();
            }
            return View(nivelUsuario);
        }

        // POST: NivelUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NivelUsuario nivelUsuario = db.NivelUsuario.Find(id);
            db.NivelUsuario.Remove(nivelUsuario);
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
