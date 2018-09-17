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
    public class SexoesController : Controller
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

        #region rol superUsuario
        // GET: Sexoes
        [Authorize(Roles = "1")]
        public ActionResult Index()
        {
            return View(db.Sexo.ToList());
        }

        // GET: Sexoes/Details/5
        [Authorize(Roles = "1")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sexo sexo = db.Sexo.Find(id);
            if (sexo == null)
            {
                return HttpNotFound();
            }
            return View(sexo);
        }

        // GET: Sexoes/Create
        [Authorize(Roles = "1")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sexoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public ActionResult Create([Bind(Include = "IdSexo,Descripcion")] Sexo sexo)
        {
            if (ModelState.IsValid)
            {
                db.Sexo.Add(sexo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sexo);
        }

        // GET: Sexoes/Edit/5
        [Authorize(Roles = "1")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sexo sexo = db.Sexo.Find(id);
            if (sexo == null)
            {
                return HttpNotFound();
            }
            return View(sexo);
        }

        // POST: Sexoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public ActionResult Edit([Bind(Include = "IdSexo,Descripcion")] Sexo sexo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sexo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sexo);
        }

        // GET: Sexoes/Delete/5
        [Authorize(Roles = "1")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sexo sexo = db.Sexo.Find(id);
            if (sexo == null)
            {
                return HttpNotFound();
            }
            return View(sexo);
        }

        // POST: Sexoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public ActionResult DeleteConfirmed(int id)
        {
            Sexo sexo = db.Sexo.Find(id);
            db.Sexo.Remove(sexo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region rol Administrador
        
        // GET: Sexoes
        [Authorize(Roles = "1,2")]
        public ActionResult IndexAdministrador()
        {
            return View(db.Sexo.ToList());
        }

        // GET: Sexoes/Details/5
        [Authorize(Roles = "1,2")]
        public ActionResult DetailsAdministrador(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sexo sexo = db.Sexo.Find(id);
            if (sexo == null)
            {
                return HttpNotFound();
            }
            return View(sexo);
        }

        // GET: Sexoes/Create
        [Authorize(Roles = "1,2")]
        public ActionResult CreateAdministrador()
        {
            return View();
        }

        // POST: Sexoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public ActionResult CreateAdministrador([Bind(Include = "IdSexo,Descripcion")] Sexo sexo)
        {
            if (ModelState.IsValid)
            {
                db.Sexo.Add(sexo);
                db.SaveChanges();
                return RedirectToAction("IndexAdministrador");
            }

            return View(sexo);
        }

        // GET: Sexoes/Edit/5
        [Authorize(Roles = "1,2")]
        public ActionResult EditAdministrador(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sexo sexo = db.Sexo.Find(id);
            if (sexo == null)
            {
                return HttpNotFound();
            }
            return View(sexo);
        }

        // POST: Sexoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public ActionResult EditAdministrador([Bind(Include = "IdSexo,Descripcion")] Sexo sexo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sexo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Indexdministrador");
            }
            return View(sexo);
        }

        #endregion

    }
}
