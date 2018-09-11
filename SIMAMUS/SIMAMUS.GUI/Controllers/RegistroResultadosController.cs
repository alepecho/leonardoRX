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
        public ActionResult Create(int id, string nombre, string idU)
        {
            Usuario usu = db.Usuario.Find(idU);

            ViewBag.nombrePersona = nombre;

            ViewBag.CodigoMedico = new SelectList(db.Medico, "CodigoMedico", "NombreUsuario");
            ViewBag.CedulaPaciente = id;
            ViewBag.CodigoRadiologo = new SelectList(db.Radiologo, "CodigoRadiologo", "NombreUsuario");
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "CodigoRegion", "Nombre");
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta");
            ViewBag.NombreUsuario = usu.NombreUsuario;
            return View();
        }  

        // POST: RegistroResultados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRegistro,fechaRegistro,fechaEstudio,Hallazgos,Conclusiones,CedulaPaciente,CodigoMedico,CodigoRadiologo,IdRegion,NombreUsuario,IdTipoConsulta")] RegistroResultados registroResultados, string IdUsuario, int IdPersona)
        {
            registroResultados.CedulaPaciente = IdPersona;
            registroResultados.NombreUsuario = IdUsuario;
            registroResultados.fechaRegistro = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.RegistroResultados.Add(registroResultados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoMedico = new SelectList(db.Medico, "CodigoMedico", "NombreUsuario", registroResultados.CodigoMedico);
            ViewBag.CodigoRadiologo = new SelectList(db.Radiologo, "CodigoRadiologo", "NombreUsuario", registroResultados.CodigoRadiologo);
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "CodigoRegion", "Nombre", registroResultados.IdRegion);
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta", registroResultados.IdTipoConsulta);
            ViewBag.NombreUsuario = IdUsuario;
            ViewBag.CedulaPaciente = IdPersona;
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
            ViewBag.CodigoMedico = new SelectList(db.Medico, "CodigoMedico", "NombreUsuario", registroResultados.CodigoMedico);
            ViewBag.CedulaPaciente = new SelectList(db.Persona, "Cedula", "Cedula", registroResultados.CedulaPaciente);
            ViewBag.CodigoRadiologo = new SelectList(db.Radiologo, "CodigoRadiologo", "NombreUsuario", registroResultados.CodigoRadiologo);
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "CodigoRegion", "Nombre", registroResultados.IdRegion);
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta", registroResultados.IdTipoConsulta);
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", registroResultados.NombreUsuario);
            return View(registroResultados);
        }

        // POST: RegistroResultados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRegistro,fechaRegistro,fechaEstudio,Hallazgos,Conclusiones,CedulaPaciente,CodigoMedico,CodigoRadiologo,IdRegion,NombreUsuario,IdTipoConsulta")] RegistroResultados registroResultados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registroResultados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoMedico = new SelectList(db.Medico, "CodigoMedico", "NombreUsuario", registroResultados.CodigoMedico);
            ViewBag.CedulaPaciente = new SelectList(db.Persona, "Cedula", "Nombre", registroResultados.CedulaPaciente);
            ViewBag.CodigoRadiologo = new SelectList(db.Radiologo, "CodigoRadiologo", "NombreUsuario", registroResultados.CodigoRadiologo);
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "CodigoRegion", "Nombre", registroResultados.IdRegion);
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta", registroResultados.IdTipoConsulta);
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", registroResultados.NombreUsuario);
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

        public ActionResult Consulta(string SearchValue)
        {
            if (SearchValue == null || SearchValue.Equals(""))
            {
                return View();
            }
            else
            {
                int id = Convert.ToInt32(SearchValue);
                return View(db.Persona.Where(x => x.Cedula == id || SearchValue == null).ToList());
            }
        }
    }
}
