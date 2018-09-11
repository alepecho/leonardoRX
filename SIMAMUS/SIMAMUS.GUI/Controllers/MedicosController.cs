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
    public class MedicosController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: Medicos
        public ActionResult Index()
        {
            var medico = db.Medico.Include(m => m.Especialidad).Include(m => m.Usuario);
            return View(medico.ToList());
        }

        // GET: Medicos/Details/5
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

        // GET: Medicos/Create
        public ActionResult Create()
        {
            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "NombreEspecialidad");
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna");
            return View();
        }

        // POST: Medicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoMedico,IdEspecialidad,NombreUsuario")] Medico medico)
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

        // GET: Medicos/Edit/5
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

        // POST: Medicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoMedico,IdEspecialidad,NombreUsuario")] Medico medico)
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

        // GET: Medicos/Delete/5
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

        // POST: Medicos/Delete/5
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
