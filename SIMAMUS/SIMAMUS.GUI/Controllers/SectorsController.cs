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
    public class SectorsController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: Sectors
        public ActionResult Index()
        {
            var sector = db.Sector.Include(s => s.CentroSalud);
            return View(sector.ToList());
        }

        // GET: Sectors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sector sector = db.Sector.Find(id);
            if (sector == null)
            {
                return HttpNotFound();
            }
            return View(sector);
        }

        // GET: Sectors/Create
        public ActionResult Create()
        {
            ViewBag.UnidadProgramatica = new SelectList(db.CentroSalud, "UnidadProgramatica", "NombreCentro");
            return View();
        }

        // POST: Sectors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoSector,UnidadProgramatica,Nombre")] Sector sector)
        {
            if (ModelState.IsValid)
            {
                db.Sector.Add(sector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UnidadProgramatica = new SelectList(db.CentroSalud, "UnidadProgramatica", "NombreCentro", sector.UnidadProgramatica);
            return View(sector);
        }

        // GET: Sectors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sector sector = db.Sector.Find(id);
            if (sector == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnidadProgramatica = new SelectList(db.CentroSalud, "UnidadProgramatica", "NombreCentro", sector.UnidadProgramatica);
            return View(sector);
        }

        // POST: Sectors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoSector,UnidadProgramatica,Nombre")] Sector sector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnidadProgramatica = new SelectList(db.CentroSalud, "UnidadProgramatica", "NombreCentro", sector.UnidadProgramatica);
            return View(sector);
        }

        // GET: Sectors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sector sector = db.Sector.Find(id);
            if (sector == null)
            {
                return HttpNotFound();
            }
            return View(sector);
        }

        // POST: Sectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sector sector = db.Sector.Find(id);
            db.Sector.Remove(sector);
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
