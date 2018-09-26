using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SIMAMUS.GUI.Models;

namespace SIMAMUS.GUI.Controllers
{
    public class UsuariosController : Controller
    {
        private SIMAMUSEntities db = new SIMAMUSEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        public ActionResult Login()
        {
            return View();
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(Usuario model, string returnUrl/*[Bind(Include = "NombreUsuario,Contrasenna")] Usuario usuario*/)
        {

            var dataItem = db.Usuario.Where(x => x.NombreUsuario == model.NombreUsuario && x.Contrasenna == model.Contrasenna).FirstOrDefault();

            if (dataItem != null && dataItem.Contrasenna == model.Contrasenna && dataItem.NombreUsuario == model.NombreUsuario)
            {
                if (dataItem.CambioContra)
                {
                    FormsAuthentication.SetAuthCookie(dataItem.NombreUsuario, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        if (dataItem.IdNivel == 1)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (dataItem.IdNivel == 2)
                        {
                            return RedirectToAction("Index2", "Home");
                        }
                        else if (dataItem.IdNivel == 3)
                        {
                            return RedirectToAction("Index3", "Home");
                        }
                        else if (dataItem.IdNivel == 4)
                        {
                            return RedirectToAction("Index2", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Login", "Usuarios");
                        }
                    }
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(dataItem.NombreUsuario, false);
                    return RedirectToAction("CambiarContra");
                }
            }
            else
            {
                //ViewBag.Mensaje = "Usuario o contraseña invalidos.";
                ModelState.AddModelError("", "Usuario o contraseña invalidos");
                return View();
            }
        }


        [Authorize]
        public ActionResult Salir()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Usuarios");
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [Authorize(Roles = "1")]
        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.IdNivel = new SelectList(db.NivelUsuario, "IdNivel", "Descripcion");
            ViewBag.Cedula = new SelectList(db.Persona, "Cedula", "Nombre");
            ViewBag.Contrasenna = GenerarContra();
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NombreUsuario,Contrasenna,IdNivel,Cedula")] Usuario usuario, string clave)
        {
            usuario.Contrasenna = clave;
            usuario.Activo = true;
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdNivel = new SelectList(db.NivelUsuario, "IdNivel", "Descripcion", usuario.IdNivel);
            ViewBag.Cedula = new SelectList(db.Persona, "Cedula", "Nombre", usuario.Cedula);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdNivel = new SelectList(db.NivelUsuario, "IdNivel", "Descripcion", usuario.IdNivel);
            ViewBag.Cedula = new SelectList(db.Persona, "Cedula", "Nombre", usuario.Cedula);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NombreUsuario,Contrasenna,IdNivel,Cedula")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdNivel = new SelectList(db.NivelUsuario, "IdNivel", "Descripcion", usuario.IdNivel);
            ViewBag.Cedula = new SelectList(db.Persona, "Cedula", "Nombre", usuario.Cedula);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CambiarContra()
        {
            if (User.Identity.Name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(User.Identity.Name);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarContra(string contraVieja, string contraNueva, string contraNueva2)
        {
            Usuario usuario = db.Usuario.Find(User.Identity.Name);
            if (usuario.Contrasenna == contraVieja && contraNueva == contraNueva2 && contraVieja != contraNueva && contraNueva.Length > 4 ) //ojo aqui, cambie el length a lo que consideren
            {
                usuario.Contrasenna = contraNueva;
                usuario.CambioContra = true;
                if (ModelState.IsValid)
                {
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }

                return View(usuario);
            }
            return RedirectToAction("CambiarContra");
        }


        private string GenerarContra()
        {
            int longitud = 6;
            Guid miGuid = Guid.NewGuid();
            string token = Convert.ToBase64String(miGuid.ToByteArray());
            token = token.Replace("=", "").Replace("+", "").Replace("/", "");
            token = token.Substring(0, longitud);
            return token;
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
