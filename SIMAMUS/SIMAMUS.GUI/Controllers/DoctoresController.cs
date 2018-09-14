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
    public class DoctoresController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: Doctores
        public ActionResult Index()
        {
            var medico = db.Medico.Include(m => m.Especialidad).Include(m => m.Usuario);
            return View(medico.ToList());
        }

        // GET: Doctores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medico.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: Doctores/Create
        public ActionResult Create()
        {
            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "NombreEspecialidad");
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna");
            return View();
        }

        // POST: Doctores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoMedico,IdEspecialidad,NombreUsuario,Activo")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Medico.Add(medico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "NombreEspecialidad", medico.IdEspecialidad);
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", medico.NombreUsuario);
            return View(medico);
        }

        // GET: Doctores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medico.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "NombreEspecialidad", medico.IdEspecialidad);
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", medico.NombreUsuario);
            return View(medico);
        }

        // POST: Doctores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoMedico,IdEspecialidad,NombreUsuario,Activo")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "NombreEspecialidad", medico.IdEspecialidad);
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", medico.NombreUsuario);
            return View(medico);
        }

        // GET: Doctores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medico.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Doctores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medico medico = db.Medico.Find(id);
            db.Medico.Remove(medico);
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
