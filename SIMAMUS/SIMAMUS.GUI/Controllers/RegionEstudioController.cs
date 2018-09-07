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
    public class RegionEstudioController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: RegionEstudio
        public ActionResult Index()
        {
            return View(db.RegionEstudio.ToList());
        }

        // GET: RegionEstudio/Details/5
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

        // GET: RegionEstudio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegionEstudio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRegion,CodRegion,Nombre")] RegionEstudio regionEstudio)
        {
            if (ModelState.IsValid)
            {
                db.RegionEstudio.Add(regionEstudio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(regionEstudio);
        }

        // GET: RegionEstudio/Edit/5
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

        // POST: RegionEstudio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRegion,CodRegion,Nombre")] RegionEstudio regionEstudio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regionEstudio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(regionEstudio);
        }

        // GET: RegionEstudio/Delete/5
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

        // POST: RegionEstudio/Delete/5
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
