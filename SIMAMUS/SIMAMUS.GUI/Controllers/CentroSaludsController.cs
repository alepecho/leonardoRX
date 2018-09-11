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
    public class CentroSaludsController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: CentroSaluds
        public ActionResult Index()
        {
            return View(db.CentroSalud.ToList());
        }

        // GET: CentroSaluds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentroSalud centroSalud = db.CentroSalud.Find(id);
            if (centroSalud == null)
            {
                return HttpNotFound();
            }
            return View(centroSalud);
        }

        // GET: CentroSaluds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CentroSaluds/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UnidadProgramatica,NombreCentro")] CentroSalud centroSalud)
        {
            if (ModelState.IsValid)
            {
                db.CentroSalud.Add(centroSalud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(centroSalud);
        }

        // GET: CentroSaluds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentroSalud centroSalud = db.CentroSalud.Find(id);
            if (centroSalud == null)
            {
                return HttpNotFound();
            }
            return View(centroSalud);
        }

        // POST: CentroSaluds/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UnidadProgramatica,NombreCentro")] CentroSalud centroSalud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(centroSalud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(centroSalud);
        }

        // GET: CentroSaluds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentroSalud centroSalud = db.CentroSalud.Find(id);
            if (centroSalud == null)
            {
                return HttpNotFound();
            }
            return View(centroSalud);
        }

        // POST: CentroSaluds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CentroSalud centroSalud = db.CentroSalud.Find(id);
            db.CentroSalud.Remove(centroSalud);
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
