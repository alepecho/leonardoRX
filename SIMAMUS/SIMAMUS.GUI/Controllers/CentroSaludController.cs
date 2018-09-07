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
    public class CentroSaludController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: CentroSalud
        public ActionResult Index()
        {
            return View(db.CentroSalud.ToList());
        }

        // GET: CentroSalud/Details/5
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

        // GET: CentroSalud/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CentroSalud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCentro,UP,NombreCentro")] CentroSalud centroSalud)
        {
            if (ModelState.IsValid)
            {
                db.CentroSalud.Add(centroSalud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(centroSalud);
        }

        // GET: CentroSalud/Edit/5
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

        // POST: CentroSalud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCentro,UP,NombreCentro")] CentroSalud centroSalud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(centroSalud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(centroSalud);
        }

        // GET: CentroSalud/Delete/5
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

        // POST: CentroSalud/Delete/5
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
