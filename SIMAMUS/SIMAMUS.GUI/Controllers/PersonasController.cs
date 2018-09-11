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
    public class PersonasController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: Personas
        public ActionResult Index()
        {
            var persona = db.Persona.Include(p => p.Sector).Include(p => p.Sexo);
            return View(persona.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            ViewBag.CodigoSector = new SelectList(db.Sector, "CodigoSector", "Nombre");
            ViewBag.IdSexo = new SelectList(db.Sexo, "IdSexo", "Descripcion");
            return View();
        }

        // POST: Personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cedula,Nombre,ApellidoUno,ApellidoDos,FechaNacimiento,Telefono,Direccion,IdSexo,CodigoSector")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Persona.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoSector = new SelectList(db.Sector, "CodigoSector", "Nombre", persona.CodigoSector);
            ViewBag.IdSexo = new SelectList(db.Sexo, "IdSexo", "Descripcion", persona.IdSexo);
            return View(persona);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoSector = new SelectList(db.Sector, "CodigoSector", "Nombre", persona.CodigoSector);
            ViewBag.IdSexo = new SelectList(db.Sexo, "IdSexo", "Descripcion", persona.IdSexo);
            return View(persona);
        }

        // POST: Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cedula,Nombre,ApellidoUno,ApellidoDos,FechaNacimiento,Telefono,Direccion,IdSexo,CodigoSector")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoSector = new SelectList(db.Sector, "CodigoSector", "Nombre", persona.CodigoSector);
            ViewBag.IdSexo = new SelectList(db.Sexo, "IdSexo", "Descripcion", persona.IdSexo);
            return View(persona);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Persona.Find(id);
            db.Persona.Remove(persona);
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
