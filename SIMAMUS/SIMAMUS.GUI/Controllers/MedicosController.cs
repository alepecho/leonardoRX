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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        #region rol SuperAdministrador

        // GET: Medicos
        [Authorize(Roles = ("1"))]
        public ActionResult Index()
        {
            var medico = db.Medico.Include(m => m.Especialidad).Include(m => m.Usuario);
            return View(medico.ToList());
        }

        // GET: Medicos/Details/5
        [Authorize(Roles = ("1"))]
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
        [Authorize(Roles = ("1"))]
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
        [Authorize(Roles = ("1"))]
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
        [Authorize(Roles = ("1"))]
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
        [Authorize(Roles = ("1"))]
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
        [Authorize(Roles = ("1"))]
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
        [Authorize(Roles = ("1"))]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medico medico = db.Medico.Find(id);
            db.Medico.Remove(medico);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region rol Administrador

        // GET: Medicos
        [Authorize(Roles = ("1,2"))]
        public ActionResult IndexAdministrador()
        {
            var medico = db.Medico.Include(m => m.Especialidad).Include(m => m.Usuario);
            return View(medico.ToList());
        }

        // GET: Medicos/Details/5
        [Authorize(Roles = ("1,2"))]
        public ActionResult DetailsAdministrador(int? id)
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
        [Authorize(Roles = ("1,2"))]
        public ActionResult CreateAdministrador()
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
        [Authorize(Roles = ("1,2"))]
        public ActionResult CreateAdministrador([Bind(Include = "CodigoMedico,IdEspecialidad,NombreUsuario")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Medico.Add(medico);
                db.SaveChanges();
                return RedirectToAction("IndexAdministrador");
            }

            ViewBag.IdEspecialidad = new SelectList(db.Especialidad, "IdEspecialidad", "NombreEspecialidad", medico.IdEspecialidad);
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", medico.NombreUsuario);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        [Authorize(Roles = ("1,2"))]
        public ActionResult EditAdministrador(int? id)
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
        [Authorize(Roles = ("1,2"))]
        public ActionResult EditAdministrador([Bind(Include = "CodigoMedico,IdEspecialidad,NombreUsuario")] Medico medico)
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
        [Authorize(Roles = ("1,2"))]
        public ActionResult DeleteAdministrador(int? id)
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
        [HttpPost, ActionName("DeleteAdministrador")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("1,2"))]
        public ActionResult DeleteConfirmedAdministrador(int id)
        {
            Medico medico = db.Medico.Find(id);
            db.Medico.Remove(medico);
            db.SaveChanges();
            return RedirectToAction("IndexAdministrador");
        }

        #endregion

    }
}
