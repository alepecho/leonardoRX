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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        #region rol SuperAdministrador

        // GET: RegionEstudios
        [Authorize(Roles ="1")]
        public ActionResult Index()
        {
            return View(db.RegionEstudio.ToList());
        }

        // GET: RegionEstudios/Details/5
        [Authorize(Roles = "1")]
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
        [Authorize(Roles = "1")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegionEstudios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
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
        [Authorize(Roles = "1")]
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
        [Authorize(Roles = "1")]
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
        [Authorize(Roles = "1")]
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
        [Authorize(Roles = "1")]
        public ActionResult DeleteConfirmed(int id)
        {
            RegionEstudio regionEstudio = db.RegionEstudio.Find(id);
            db.RegionEstudio.Remove(regionEstudio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region rol Administrador


        // GET: RegionEstudios
        [Authorize(Roles = "1,2")]
        public ActionResult IndexAdministrador()
        {
            return View(db.RegionEstudio.ToList());
        }

        // GET: RegionEstudios/Details/5
        [Authorize(Roles = "1,2")]
        public ActionResult DetailsAdministrador(int? id)
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
        [Authorize(Roles = "1,2")]
        public ActionResult CreateAdministrador()
        {
            return View();
        }

        // POST: RegionEstudios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public ActionResult CreateAdministrador([Bind(Include = "CodigoRegion,Nombre")] RegionEstudio regionEstudio)
        {
            if (ModelState.IsValid)
            {
                db.RegionEstudio.Add(regionEstudio);
                db.SaveChanges();
                return RedirectToAction("IndexAdministrador");
            }

            return View(regionEstudio);
        }

        // GET: RegionEstudios/Edit/5
        [Authorize(Roles = "1,2")]
        public ActionResult EditAdministrador(int? id)
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
        [Authorize(Roles = "1,2")]
        public ActionResult EditAdministrador([Bind(Include = "CodigoRegion,Nombre")] RegionEstudio regionEstudio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regionEstudio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAdministrador");
            }
            return View(regionEstudio);
        }

        #endregion
    }
}
