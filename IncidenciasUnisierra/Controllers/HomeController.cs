using IncidenciasUnisierra.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace IncidenciasUnisierra.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var IdUsuarioActual = User.Identity.GetUserId();

                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                    var rol = roleManager.Create(new IdentityRole("JefeMantenimiento"));
                    var rol2 = roleManager.Create(new IdentityRole("Supervisores"));
                    var rol3 = roleManager.Create(new IdentityRole("Manuales"));


                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                    var resultado = userManager.AddToRole(IdUsuarioActual, "JefeMantenimiento");
                }
            }

            return View();
        }
        //Incidencias
        [Authorize(Roles = "JefeMantenimiento,Manuales,Supervisores")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //Asignacion
        [Authorize(Roles = "JefeMantenimiento")]
        public ActionResult Contact()
        {
            //cODIGO DE LISTA DE RESPONSABLES
            var lista = db.Responsables.ToList();
            
            ViewBag.Nombres = new SelectList(lista, "Nombre","NombreCompleto");

            ViewBag.Message = "Your contact page.";
            //CODIGO DE LISTAS DE INCIDENCIAS

            //var listaIncidencias = db.Incidencias.ToList();
            //ViewBag.Incidencias = new SelectList(listaIncidencias, "Nombre", "Nombre");

            var asignaciones = db.Incidencias.Where(r => r.ResponsableId == null).ToList();
            ViewBag.Incidencia = new SelectList(asignaciones, "Nombre", "Nombre");

            return View();
        }   

        [HttpPost]
        public ActionResult Contact(String Incidencia,String Nombres)
        {
            
            var res= db.Responsables.Where(u => u.Nombre.Equals(Nombres)).Select(u => u.Nombre).FirstOrDefault();
            var idRes = db.Responsables.Where(y => y.Nombre.Equals(res)).Select(x => x.Id).FirstOrDefault();

            //SACAR ID DE INCIDENCIAS
            var resIncidencia = db.Incidencias.Where(u => u.Nombre.Equals(Incidencia)).Select(u => u.Nombre).FirstOrDefault();
            var idInc = db.Incidencias.Where(y => y.Nombre.Equals(resIncidencia)).Select(x => x.Id).FirstOrDefault();
            int id = Convert.ToInt32 (idRes);
            int idin = Convert.ToInt32(idInc);
      
           var query = db.Database.ExecuteSqlCommand("UPDATE dbo.Incidencias" +
           " SET ResponsableId = " + id +
            "WHERE Id = " + idin);
          
            db.SaveChanges();
            Thread.Sleep(2500);
            return RedirectToAction("Contact", "Home");

        }
        public ActionResult RegistroCompleto()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}