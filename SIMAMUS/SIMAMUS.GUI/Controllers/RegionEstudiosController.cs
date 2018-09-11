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
    public class RegionEstudiosController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: RegionEstudios
        public ActionResult Index()
        {
            return View(db.RegionEstudio.ToList());
        }

        // GET: RegionEstudios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionEstudio regionEstudio = db.RegionEstudio.Find(id);
            if (regionEstudio == null)
            {
                return HttpNotFound();
            }
            return View(regionEstudio);
        }

        // GET: RegionEstudios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegionEstudios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoRegion,Nombre")] RegionEstudio regionEstudio)
        {
            if (ModelState.IsValid)
            {
                db.RegionEstudio.Add(regionEstudio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(regionEstudio);
        }

        // GET: RegionEstudios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionEstudio regionEstudio = db.RegionEstudio.Find(id);
            if (regionEstudio == null)
            {
                return HttpNotFound();
            }
            return View(regionEstudio);
        }

        // POST: RegionEstudios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoRegion,Nombre")] RegionEstudio regionEstudio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regionEstudio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(regionEstudio);
        }

        // GET: RegionEstudios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegionEstudio regionEstudio = db.RegionEstudio.Find(id);
            if (regionEstudio == null)
            {
                return HttpNotFound();
            }
            return View(regionEstudio);
        }

        // POST: RegionEstudios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegionEstudio regionEstudio = db.RegionEstudio.Find(id);
            db.RegionEstudio.Remove(regionEstudio);
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
