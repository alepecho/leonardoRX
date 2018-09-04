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
    public class RegistroResultadosController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: RegistroResultados
        public ActionResult Index()
        {
            var registroResultados = db.RegistroResultados.Include(r => r.Medico).Include(r => r.Persona).Include(r => r.Radiologo).Include(r => r.RegionEstudio).Include(r => r.TipoConsulta).Include(r => r.Usuario);
            return View(registroResultados.ToList());
        }

        // GET: RegistroResultados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroResultados registroResultados = db.RegistroResultados.Find(id);
            if (registroResultados == null)
            {
                return HttpNotFound();
            }
            return View(registroResultados);
        }

        // GET: RegistroResultados/Create
        public ActionResult Create()
        {
            ViewBag.IdMedico = new SelectList(db.Medico, "IdMedico", "IdMedico");
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre");
            ViewBag.IdRadiologo = new SelectList(db.Radiologo, "IdRadiologo", "IdRadiologo");
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "IdRegion", "Nombre");
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "NombreUsuario");
            return View();
        }

        // POST: RegistroResultados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRegistro,fechaRegistro,fechaEstudio,Hallazgos,Conclusiones,IdPersona,IdMedico,IdRadiologo,IdRegion,IdUsuario,IdTipoConsulta")] RegistroResultados registroResultados)
        {
            if (ModelState.IsValid)
            {
                db.RegistroResultados.Add(registroResultados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMedico = new SelectList(db.Medico, "IdMedico", "IdMedico", registroResultados.IdMedico);
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", registroResultados.IdPersona);
            ViewBag.IdRadiologo = new SelectList(db.Radiologo, "IdRadiologo", "IdRadiologo", registroResultados.IdRadiologo);
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "IdRegion", "Nombre", registroResultados.IdRegion);
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta", registroResultados.IdTipoConsulta);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "NombreUsuario", registroResultados.IdUsuario);
            return View(registroResultados);
        }

        // GET: RegistroResultados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroResultados registroResultados = db.RegistroResultados.Find(id);
            if (registroResultados == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMedico = new SelectList(db.Medico, "IdMedico", "IdMedico", registroResultados.IdMedico);
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", registroResultados.IdPersona);
            ViewBag.IdRadiologo = new SelectList(db.Radiologo, "IdRadiologo", "IdRadiologo", registroResultados.IdRadiologo);
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "IdRegion", "Nombre", registroResultados.IdRegion);
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta", registroResultados.IdTipoConsulta);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "NombreUsuario", registroResultados.IdUsuario);
            return View(registroResultados);
        }

        // POST: RegistroResultados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRegistro,fechaRegistro,fechaEstudio,Hallazgos,Conclusiones,IdPersona,IdMedico,IdRadiologo,IdRegion,IdUsuario,IdTipoConsulta")] RegistroResultados registroResultados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registroResultados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMedico = new SelectList(db.Medico, "IdMedico", "IdMedico", registroResultados.IdMedico);
            ViewBag.IdPersona = new SelectList(db.Persona, "IdPersona", "Nombre", registroResultados.IdPersona);
            ViewBag.IdRadiologo = new SelectList(db.Radiologo, "IdRadiologo", "IdRadiologo", registroResultados.IdRadiologo);
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "IdRegion", "Nombre", registroResultados.IdRegion);
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta", registroResultados.IdTipoConsulta);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "NombreUsuario", registroResultados.IdUsuario);
            return View(registroResultados);
        }

        // GET: RegistroResultados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroResultados registroResultados = db.RegistroResultados.Find(id);
            if (registroResultados == null)
            {
                return HttpNotFound();
            }
            return View(registroResultados);
        }

        // POST: RegistroResultados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegistroResultados registroResultados = db.RegistroResultados.Find(id);
            db.RegistroResultados.Remove(registroResultados);
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
