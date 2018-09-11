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
    public class TipoConsultaController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: TipoConsulta
        public ActionResult Index()
        {
            return View(db.TipoConsulta.ToList());
        }

        // GET: TipoConsulta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoConsulta tipoConsulta = db.TipoConsulta.Find(id);
            if (tipoConsulta == null)
            {
                return HttpNotFound();
            }
            return View(tipoConsulta);
        }

        // GET: TipoConsulta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoConsulta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipoConsulta,NombreConsulta")] TipoConsulta tipoConsulta)
        {
            if (ModelState.IsValid)
            {
                db.TipoConsulta.Add(tipoConsulta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoConsulta);
        }

        // GET: TipoConsulta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoConsulta tipoConsulta = db.TipoConsulta.Find(id);
            if (tipoConsulta == null)
            {
                return HttpNotFound();
            }
            return View(tipoConsulta);
        }

        // POST: TipoConsulta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoConsulta,NombreConsulta")] TipoConsulta tipoConsulta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoConsulta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoConsulta);
        }

        // GET: TipoConsulta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoConsulta tipoConsulta = db.TipoConsulta.Find(id);
            if (tipoConsulta == null)
            {
                return HttpNotFound();
            }
            return View(tipoConsulta);
        }

        // POST: TipoConsulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoConsulta tipoConsulta = db.TipoConsulta.Find(id);
            db.TipoConsulta.Remove(tipoConsulta);
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
