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
    public class TipoConsultasController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: TipoConsultas
        public ActionResult Index()
        {
            return View(db.TipoConsulta.ToList());
        }

        // GET: TipoConsultas/Details/5
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

        // GET: TipoConsultas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoConsultas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: TipoConsultas/Edit/5
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

        // POST: TipoConsultas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: TipoConsultas/Delete/5
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

        // POST: TipoConsultas/Delete/5
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
