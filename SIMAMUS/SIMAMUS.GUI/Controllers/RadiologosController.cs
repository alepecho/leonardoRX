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
    public class RadiologosController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: Radiologos
        public ActionResult Index()
        {
            var radiologo = db.Radiologo.Include(r => r.Usuario);
            return View(radiologo.ToList());
        }

        // GET: Radiologos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radiologo radiologo = db.Radiologo.Find(id);
            if (radiologo == null)
            {
                return HttpNotFound();
            }
            return View(radiologo);
        }

        // GET: Radiologos/Create
        public ActionResult Create()
        {
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna");
            return View();
        }

        // POST: Radiologos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoRadiologo,NombreUsuario,Activo")] Radiologo radiologo)
        {
            if (ModelState.IsValid)
            {
                db.Radiologo.Add(radiologo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", radiologo.NombreUsuario);
            return View(radiologo);
        }

        // GET: Radiologos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radiologo radiologo = db.Radiologo.Find(id);
            if (radiologo == null)
            {
                return HttpNotFound();
            }
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", radiologo.NombreUsuario);
            return View(radiologo);
        }

        // POST: Radiologos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoRadiologo,NombreUsuario,Activo")] Radiologo radiologo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(radiologo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", radiologo.NombreUsuario);
            return View(radiologo);
        }

        // GET: Radiologos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radiologo radiologo = db.Radiologo.Find(id);
            if (radiologo == null)
            {
                return HttpNotFound();
            }
            return View(radiologo);
        }

        // POST: Radiologos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Radiologo radiologo = db.Radiologo.Find(id);
            db.Radiologo.Remove(radiologo);
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
