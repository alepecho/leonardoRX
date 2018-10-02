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
    public class RadiologoController : Controller
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

        #region rol SuperUsuario

        [Authorize(Roles ="1")]
        // GET: Radiologo
        public ActionResult Index()
        {
            var radiologo = db.Radiologo.Include(r => r.Usuario);
            
            return View(radiologo.ToList());
        }

        // GET: Radiologo/Details/5
        [Authorize(Roles = "1")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radiologo radiologo = db.Radiologo.Find(id);
            if (radiologo == null)
            {
                return HttpNotFound();
            }
            return View(radiologo);
        }

        // GET: Radiologo/Create
        [Authorize(Roles = "1")]
        public ActionResult Create()
        {
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "NombreUsuario");
            return View();
        }

        // POST: Radiologo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public ActionResult Create([Bind(Include = "CodigoRadiologo,NombreUsuario")] Radiologo radiologo)
        {
            if (ModelState.IsValid)
            {
                db.Radiologo.Add(radiologo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", radiologo.NombreUsuario);
            return View(radiologo);
        }

        // GET: Radiologo/Edit/5
        [Authorize(Roles = "1")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radiologo radiologo = db.Radiologo.Find(id);
            if (radiologo == null)
            {
                return HttpNotFound();
            }
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "NombreUsuario", radiologo.NombreUsuario);
            return View(radiologo);
        }

        // POST: Radiologo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public ActionResult Edit([Bind(Include = "CodigoRadiologo,NombreUsuario")] Radiologo radiologo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(radiologo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", radiologo.NombreUsuario);
            return View(radiologo);
        }

        // GET: Radiologo/Delete/5
        [Authorize(Roles = "1")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radiologo radiologo = db.Radiologo.Find(id);
            if (radiologo == null)
            {
                return HttpNotFound();
            }
            return View(radiologo);
        }

        // POST: Radiologo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public ActionResult DeleteConfirmed(int id)
        {
            Radiologo radiologo = db.Radiologo.Find(id);
            db.Radiologo.Remove(radiologo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region rol Administrador

        [Authorize(Roles = "1,2")]
        // GET: Radiologo
        public ActionResult IndexAdministrador()
        {
            var radiologo = db.Radiologo.Include(r => r.Usuario);
            return View(radiologo.ToList());
        }

        // GET: Radiologo/Details/5
        [Authorize(Roles = "1,2")]
        public ActionResult DetailsAdministrador(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radiologo radiologo = db.Radiologo.Find(id);
            if (radiologo == null)
            {
                return HttpNotFound();
            }
            return View(radiologo);
        }

        // GET: Radiologo/Create
        [Authorize(Roles = "1,2")]
        public ActionResult CreateAdministrador()
        {
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "NombreUsuario");
            return View();
        }

        // POST: Radiologo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public ActionResult CreateAdministrador([Bind(Include = "CodigoRadiologo,NombreUsuario")] Radiologo radiologo)
        {
            if (ModelState.IsValid)
            {
                db.Radiologo.Add(radiologo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", radiologo.NombreUsuario);
            return View(radiologo);
        }

        // GET: Radiologo/Edit/5
        [Authorize(Roles = "1,2")]
        public ActionResult EditAdministrador(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radiologo radiologo = db.Radiologo.Find(id);
            if (radiologo == null)
            {
                return HttpNotFound();
            }
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", radiologo.NombreUsuario);
            return View(radiologo);
        }

        // POST: Radiologo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public ActionResult EditAdministrador([Bind(Include = "CodigoRadiologo,NombreUsuario")] Radiologo radiologo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(radiologo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", radiologo.NombreUsuario);
            return View(radiologo);
        }

        // GET: Radiologo/Delete/5
        [Authorize(Roles = "1,2")]
        public ActionResult DeleteAdministrador(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radiologo radiologo = db.Radiologo.Find(id);
            if (radiologo == null)
            {
                return HttpNotFound();
            }
            return View(radiologo);
        }

        // POST: Radiologo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public ActionResult DeleteConfirmedAdministrador(int id)
        {
            Radiologo radiologo = db.Radiologo.Find(id);
            db.Radiologo.Remove(radiologo);
            db.SaveChanges();
            return RedirectToAction("IndexAdministrador");
        }

        #endregion

        #region rol Tecnico
        
        [Authorize(Roles = "1,3")]
        // GET: Radiologo
        public ActionResult IndexTecnico()
        {
            var radiologo = db.Radiologo.Include(r => r.Usuario);
            return View(radiologo.ToList());
        }

        // GET: Radiologo/Details/5
        [Authorize(Roles = "1,3")]
        public ActionResult DetailsTecnico(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radiologo radiologo = db.Radiologo.Find(id);
            if (radiologo == null)
            {
                return HttpNotFound();
            }
            return View(radiologo);
        }

        // GET: Radiologo/Create
        [Authorize(Roles = "1,3")]
        public ActionResult CreateTecnico()
        {
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "NombreUsuario");
            return View();
        }

        // POST: Radiologo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,3")]
        public ActionResult CreateTecnico([Bind(Include = "CodigoRadiologo,NombreUsuario")] Radiologo radiologo)
        {
            if (ModelState.IsValid)
            {
                db.Radiologo.Add(radiologo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", radiologo.NombreUsuario);
            return View(radiologo);
        }

        // GET: Radiologo/Edit/5
        [Authorize(Roles = "1,3")]
        public ActionResult EditTecnico(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Radiologo radiologo = db.Radiologo.Find(id);
            if (radiologo == null)
            {
                return HttpNotFound();
            }
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", radiologo.NombreUsuario);
            return View(radiologo);
        }

        // POST: Radiologo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,3")]
        public ActionResult EditTecnico([Bind(Include = "CodigoRadiologo,NombreUsuario")] Radiologo radiologo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(radiologo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", radiologo.NombreUsuario);
            return View(radiologo);
        }
        
        #endregion
    }
}
