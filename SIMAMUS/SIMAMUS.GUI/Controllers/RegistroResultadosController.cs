using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIMAMUS.GUI.Models;
using SIMAMUS.GUI.ViewModels;

namespace SIMAMUS.GUI.Controllers
{
    public class RegistroResultadosController : Controller
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

        public ActionResult Reporte(int id)
        {
            var _resultado = db.pro_Reportes(id);

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reporte"), "Reporte3.rpt"));
            rd.SetDataSource(_resultado.ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Registro de resultado.pdf");
            }
            catch
            {
                throw;
            }
        }

        #region rol SuperAdministrador

        // GET: RegistroResultados
        [Authorize(Roles = "1")]
        public ActionResult Index(int? ced, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 20; // parámetro
            using (var bd = new SIMAMUSEntities())
            {
                Func<RegistroResultados, bool> predicado = x => !ced.HasValue || ced.Value == x.CedulaPaciente;

                var personas = bd.RegistroResultados.Where(x => x.CedulaPaciente == ced || ced == null).OrderByDescending(x => x.IdRegistro)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.RegistroResultados.Where(x => x.CedulaPaciente == ced || ced == null).Count();

                var modelo = new IndexViewModels();
                modelo.Registros = personas;
                modelo.PaginaActual = pagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["ced"] = ced;

                return View(modelo);
            }
        }

        // GET: RegistroResultados/Details/5
        [Authorize(Roles = "1")]
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
        [Authorize(Roles = "1")]
        public ActionResult Create(int id, string nombre, string idU)
        {
            Usuario usu = db.Usuario.Find(idU);

            ViewBag.nombrePersona = nombre;

            ViewBag.CodigoMedico = new SelectList(db.Medico, "CodigoMedico", "NombreUsuario");
            ViewBag.CedulaPaciente = id;
            ViewBag.CodigoRadiologo = new SelectList(db.Radiologo, "CodigoRadiologo", "NombreUsuario");
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "CodigoRegion", "Nombre");
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta");
            ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion");
            ViewBag.NombreUsuario = usu.NombreUsuario;
            return View();
        }

        // POST: RegistroResultados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public ActionResult Create([Bind(Include = "IdRegistro,fechaRegistro,fechaEstudio,Hallazgos,Conclusiones,CedulaPaciente,CodigoMedico,CodigoRadiologo,IdRegion,NombreUsuario,IdTipoConsulta,IdTipoExamen,UltimoUsuarioModificar")] RegistroResultados registroResultados, string IdUsuario, int IdPersona)
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
            ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion", registroResultados.IdTipoExamen);
            ViewBag.NombreUsuario = IdUsuario;
            ViewBag.CedulaPaciente = IdPersona;
            return View(registroResultados);
        }

        // GET: RegistroResultados/Edit/5
        [Authorize(Roles = "1")]
        public ActionResult Edit(int? id)
        {
            RegistroResultados reg = db.RegistroResultados.Where(x => x.IdRegistro == id).FirstOrDefault();

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


            ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion", registroResultados.IdTipoExamen);

            //ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion", registroResultados.IdTipoExamen);
            return View(registroResultados);
        }

        // POST: RegistroResultados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1")]
        public ActionResult Edit([Bind(Include = "IdRegistro,fechaRegistro,fechaEstudio,Hallazgos,Conclusiones,CedulaPaciente,CodigoMedico,CodigoRadiologo,IdRegion,NombreUsuario,IdTipoConsulta,IdTipoExamen")] RegistroResultados registroResultados)
        {
            registroResultados.UltimoUsuarioModificar = User.Identity.Name;
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
            ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion", registroResultados.IdTipoExamen);
            return View(registroResultados);
        }

        // GET: RegistroResultados/Delete/5
        [Authorize(Roles = "1")]
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
        [Authorize(Roles = "1")]
        public ActionResult DeleteConfirmed(int id)
        {
            RegistroResultados registroResultados = db.RegistroResultados.Find(id);
            db.RegistroResultados.Remove(registroResultados);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region rol Administrador

        // GET: RegistroResultados
        [Authorize(Roles = "1,2")]
        public ActionResult IndexAdministrador(int? ced, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 20; // parámetro
            using (var bd = new SIMAMUSEntities())
            {
                Func<RegistroResultados, bool> predicado = x => !ced.HasValue || ced.Value == x.CedulaPaciente;

                var personas = bd.RegistroResultados.Where(x => x.CedulaPaciente == ced || ced == null).OrderByDescending(x => x.IdRegistro)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.RegistroResultados.Where(x => x.CedulaPaciente == ced || ced == null).Count();

                var modelo = new IndexViewModels();
                modelo.Registros = personas;
                modelo.PaginaActual = pagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["ced"] = ced;

                return View(modelo);
            }
        }

        // GET: RegistroResultados/Details/5
        [Authorize(Roles = "1,2")]
        public ActionResult DetailsAdministrador(int? id)
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
        [Authorize(Roles = "1,2")]
        public ActionResult CreateAdministrador(int id, string nombre, string idU)
        {
            Usuario usu = db.Usuario.Find(idU);

            ViewBag.nombrePersona = nombre;

            ViewBag.CodigoMedico = new SelectList(db.Medico, "CodigoMedico", "NombreUsuario");
            ViewBag.CedulaPaciente = id;
            ViewBag.CodigoRadiologo = new SelectList(db.Radiologo, "CodigoRadiologo", "NombreUsuario");
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "CodigoRegion", "Nombre");
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta");
            ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion");
            ViewBag.NombreUsuario = usu.NombreUsuario;
            return View();
        }

        // POST: RegistroResultados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public ActionResult CreateAdministrador([Bind(Include = "IdRegistro,fechaRegistro,fechaEstudio,Hallazgos,Conclusiones,CedulaPaciente,CodigoMedico,CodigoRadiologo,IdRegion,NombreUsuario,IdTipoConsulta,IdTipoExamen")] RegistroResultados registroResultados, string IdUsuario, int IdPersona)
        {
            registroResultados.CedulaPaciente = IdPersona;
            registroResultados.NombreUsuario = IdUsuario;
            registroResultados.fechaRegistro = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.RegistroResultados.Add(registroResultados);
                db.SaveChanges();
                return RedirectToAction("IndeAdministrador");
            }

            ViewBag.CodigoMedico = new SelectList(db.Medico, "CodigoMedico", "NombreUsuario", registroResultados.CodigoMedico);
            ViewBag.CodigoRadiologo = new SelectList(db.Radiologo, "CodigoRadiologo", "NombreUsuario", registroResultados.CodigoRadiologo);
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "CodigoRegion", "Nombre", registroResultados.IdRegion);
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta", registroResultados.IdTipoConsulta);
            ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion", registroResultados.IdTipoExamen);
            ViewBag.NombreUsuario = IdUsuario;
            ViewBag.CedulaPaciente = IdPersona;

            return View(registroResultados);
        }

        // GET: RegistroResultados/Edit/5
        [Authorize(Roles = "1,2")]
        public ActionResult EditAdministrador(int? id)
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
            ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion", registroResultados.IdTipoExamen);
            return View(registroResultados);
        }

        // POST: RegistroResultados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public ActionResult EditAdministrador([Bind(Include = "IdRegistro,fechaRegistro,fechaEstudio,Hallazgos,Conclusiones,CedulaPaciente,CodigoMedico,CodigoRadiologo,IdRegion,NombreUsuario,IdTipoConsulta,IdTipoExamen")] RegistroResultados registroResultados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registroResultados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAdministrador");
            }
            ViewBag.CodigoMedico = new SelectList(db.Medico, "CodigoMedico", "NombreUsuario", registroResultados.CodigoMedico);
            ViewBag.CedulaPaciente = new SelectList(db.Persona, "Cedula", "Nombre", registroResultados.CedulaPaciente);
            ViewBag.CodigoRadiologo = new SelectList(db.Radiologo, "CodigoRadiologo", "NombreUsuario", registroResultados.CodigoRadiologo);
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "CodigoRegion", "Nombre", registroResultados.IdRegion);
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta", registroResultados.IdTipoConsulta);
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", registroResultados.NombreUsuario);
            ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion", registroResultados.IdTipoExamen);
            return View(registroResultados);
        }

        #endregion

        #region Tecnico

        [Authorize(Roles = "1,3")]
        public ActionResult IndexTecnico(int? ced, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 20; // parámetro
            using (var bd = new SIMAMUSEntities())
            {
                Func<RegistroResultados, bool> predicado = x => !ced.HasValue || ced.Value == x.CedulaPaciente;

                var personas = bd.RegistroResultados.Where(x => x.CedulaPaciente == ced || ced == null).OrderByDescending(x => x.IdRegistro)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.RegistroResultados.Where(x => x.CedulaPaciente == ced || ced == null).Count();

                var modelo = new IndexViewModels();
                modelo.Registros = personas;
                modelo.PaginaActual = pagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["ced"] = ced;

                return View(modelo);
            }
        }

        // GET: RegistroResultados/Details/5
        [Authorize(Roles = "1,3")]
        public ActionResult DetailsTecnico(int? id)
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
        [Authorize(Roles = "1,3")]
        public ActionResult CreateTecnico(int id, string nombre, string idU)
        {
            Usuario usu = db.Usuario.Find(idU);

            ViewBag.nombrePersona = nombre;

            ViewBag.CodigoMedico = new SelectList(db.Medico, "CodigoMedico", "NombreUsuario");
            ViewBag.CedulaPaciente = id;
            ViewBag.CodigoRadiologo = new SelectList(db.Radiologo, "CodigoRadiologo", "NombreUsuario");
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "CodigoRegion", "Nombre");
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta");
            ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion");
            ViewBag.NombreUsuario = usu.NombreUsuario;
            return View();
        }

        // POST: RegistroResultados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,3")]
        public ActionResult CreateTecnico([Bind(Include = "IdRegistro,fechaRegistro,fechaEstudio,Hallazgos,Conclusiones,CedulaPaciente,CodigoMedico,CodigoRadiologo,IdRegion,NombreUsuario,IdTipoConsulta,IdTipoExamen")] RegistroResultados registroResultados, string IdUsuario, int IdPersona)
        {
            registroResultados.CedulaPaciente = IdPersona;
            registroResultados.NombreUsuario = IdUsuario;
            registroResultados.fechaRegistro = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.RegistroResultados.Add(registroResultados);
                db.SaveChanges();
                return RedirectToAction("IndeAdministrador");
            }

            ViewBag.CodigoMedico = new SelectList(db.Medico, "CodigoMedico", "NombreUsuario", registroResultados.CodigoMedico);
            ViewBag.CodigoRadiologo = new SelectList(db.Radiologo, "CodigoRadiologo", "NombreUsuario", registroResultados.CodigoRadiologo);
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "CodigoRegion", "Nombre", registroResultados.IdRegion);
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta", registroResultados.IdTipoConsulta);
            ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion", registroResultados.IdTipoExamen);
            ViewBag.NombreUsuario = IdUsuario;
            ViewBag.CedulaPaciente = IdPersona;

            return View(registroResultados);
        }

        // GET: RegistroResultados/Edit/5
        [Authorize(Roles = "1,3")]
        public ActionResult EditTecnico(int? id)
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
            ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion", registroResultados.IdTipoExamen);
            return View(registroResultados);
        }

        // POST: RegistroResultados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,3")]
        public ActionResult EditTecnico([Bind(Include = "IdRegistro,fechaRegistro,fechaEstudio,Hallazgos,Conclusiones,CedulaPaciente,CodigoMedico,CodigoRadiologo,IdRegion,NombreUsuario,IdTipoConsulta,IdTipoExamen")] RegistroResultados registroResultados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registroResultados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAdministrador");
            }
            ViewBag.CodigoMedico = new SelectList(db.Medico, "CodigoMedico", "NombreUsuario", registroResultados.CodigoMedico);
            ViewBag.CedulaPaciente = new SelectList(db.Persona, "Cedula", "Nombre", registroResultados.CedulaPaciente);
            ViewBag.CodigoRadiologo = new SelectList(db.Radiologo, "CodigoRadiologo", "NombreUsuario", registroResultados.CodigoRadiologo);
            ViewBag.IdRegion = new SelectList(db.RegionEstudio, "CodigoRegion", "Nombre", registroResultados.IdRegion);
            ViewBag.IdTipoConsulta = new SelectList(db.TipoConsulta, "IdTipoConsulta", "NombreConsulta", registroResultados.IdTipoConsulta);
            ViewBag.NombreUsuario = new SelectList(db.Usuario, "NombreUsuario", "Contrasenna", registroResultados.NombreUsuario);
            ViewBag.IdTipoExamen = new SelectList(db.TipoExamen, "IdTipoExamen", "Descripcion", registroResultados.IdTipoExamen);
            return View(registroResultados);
        }
        #endregion

        #region Consultor

        [Authorize(Roles = "1,4")]
        public ActionResult IndexConsultor(int? ced, int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 20; // parámetro
            using (var bd = new SIMAMUSEntities())
            {
                Func<RegistroResultados, bool> predicado = x => !ced.HasValue || ced.Value == x.CedulaPaciente;

                var personas = bd.RegistroResultados.Where(x => x.CedulaPaciente == ced || ced == null).OrderByDescending(x => x.IdRegistro)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.RegistroResultados.Where(x => x.CedulaPaciente == ced || ced == null).Count();

                var modelo = new IndexViewModels();
                modelo.Registros = personas;
                modelo.PaginaActual = pagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;
                modelo.ValoresQueryString = new RouteValueDictionary();
                modelo.ValoresQueryString["ced"] = ced;

                return View(modelo);
            }
        }

        // GET: RegistroResultados/Details/5
        [Authorize(Roles = "1,4")]
        public ActionResult DetailsConsultor(int? id)
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

        
        #endregion
    }
}
