using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IncidenciasUnisierra.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace IncidenciasUnisierra.Controllers
{
    public class IncidenciasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Incidencias
        [Authorize(Roles = "JefeMantenimiento, Supervisores")]
        public ActionResult Index()
        {
            return View(db.Incidencias.ToList());
        }

        [HttpPost]
        public ActionResult Index(string IdBotones, string opcion)
        {
            int id = Convert.ToInt32(IdBotones);


            var query = db.Database.ExecuteSqlCommand("UPDATE dbo.Incidencias" +
        " SET Estado = '" + opcion + "'  WHERE Id = " + id);

            db.SaveChanges();

            return View(db.Incidencias.ToList());
        }


        [Authorize(Roles = "Manuales,Supervisores")]
        public ActionResult Tareas()
        {
            db.Incidencias.ToList();
        
           var usuario = User.Identity.GetUserName();
           var id = db.Responsables.Where(y => y.NombreUsuario.Equals(usuario)).Select(x => x.Id).FirstOrDefault();
           var lista = db.Incidencias.Where(y => y.ResponsableId == id).ToList();

            return View(lista);
        }

        [HttpPost]
        public ActionResult Tareas(string IdBotones, string opcion)
        {
            int id = Convert.ToInt32(IdBotones);

            var query = db.Database.ExecuteSqlCommand("UPDATE dbo.Incidencias" +
        " SET Estado = '" + opcion + "'  WHERE Id = " + id);

            db.SaveChanges();

            return RedirectToAction("Tareas", "Incidencias");
        }



        public JsonResult AreasporEdificioyPlanta(string idedificio, string idplanta)
        {

            // Indice de edificios
            // 1= Telematica
            // 2= Industrial
            // 3= Principal

            // Indice de plantas
            // 1= Alta
            // 2= Baja

            List<SelectListItem> states = new List<SelectListItem>();
            switch (idedificio + idplanta)
            {
                //Telematica Alta
                case "11":
                    states.Add(new SelectListItem { Text = "Área", Value = "" });
                    states.Add(new SelectListItem { Text = "Cubo de Profesor David", Value = "1" });
                    states.Add(new SelectListItem { Text = "Cubo de Profesor Ulices", Value = "2" });
                    states.Add(new SelectListItem { Text = "Salón Sistemas 1-5", Value = "3" });
                    states.Add(new SelectListItem { Text = "Salón Sistemas 1-7", Value = "4" });
                    states.Add(new SelectListItem { Text = "Laboratorio Señales y electronica", Value = "5" });
                    states.Add(new SelectListItem { Text = "Laboratorio De Sistemas", Value = "6" });
                    states.Add(new SelectListItem { Text = "Laboratorio de Desarrollo de Sistemas", Value = "7" });
                    states.Add(new SelectListItem { Text = "Laboratorio De Redes", Value = "8" });

                    break;

                //Telematica Baja
                case "12":
                    states.Add(new SelectListItem { Text = "Área", Value = "" });
                    states.Add(new SelectListItem { Text = "Centro de Computo", Value = "9" });
                    states.Add(new SelectListItem { Text = "Cubo de Profesor Aldo", Value = "10" });
                    states.Add(new SelectListItem { Text = "Salón Sistemas 1-3", Value = "11" });
                    states.Add(new SelectListItem { Text = "Salón Sistemas 1-5", Value = "12" });
                    states.Add(new SelectListItem { Text = "Baño Hombres", Value = "13" });
                    states.Add(new SelectListItem { Text = "Baño Mujeres", Value = "14" });

                    break;

                //Industrial Alta
                case "21":
                    states.Add(new SelectListItem { Text = "Área", Value = "" });
                    states.Add(new SelectListItem { Text = "Biblioteca", Value = "15" });
                    states.Add(new SelectListItem { Text = "Centro de Copiado", Value = "16" });
                    states.Add(new SelectListItem { Text = "Industrial 1-2", Value = "17" });
                    states.Add(new SelectListItem { Text = "Industrial 1-1", Value = "18" });
                    states.Add(new SelectListItem { Text = "Baños hombres", Value = "19" });
                    states.Add(new SelectListItem { Text = "Baños Mujeres", Value = "20" });
                    states.Add(new SelectListItem { Text = "Cubo de Profesor Juan Y David", Value = "21" });
                    states.Add(new SelectListItem { Text = "Cubo de Profesor Dinora", Value = "22" });
                    break;
                //Industrial Baja
                case "22":
                    states.Add(new SelectListItem { Text = "Área", Value = "" });
                    states.Add(new SelectListItem { Text = "Industrial 1-5", Value = "23" });
                    states.Add(new SelectListItem { Text = "Laboratorio Botanica", Value = "24" });
                    states.Add(new SelectListItem { Text = "Laboratorio Zoología", Value = "25" });
                    states.Add(new SelectListItem { Text = "Baño hombres", Value = "26" });
                    states.Add(new SelectListItem { Text = "Baño Mujeres", Value = "27" });
                    break;

                //Principal Planta Alta
                case "31":
                    states.Add(new SelectListItem { Text = "Área", Value = "" });
                    states.Add(new SelectListItem { Text = "Vinculacion", Value = "28" });
                    states.Add(new SelectListItem { Text = "Laboratorio de Idiomas", Value = "29" });
                    states.Add(new SelectListItem { Text = "Unidad de Administración y finanzas", Value = "30" });
                    states.Add(new SelectListItem { Text = "Baño hombres", Value = "31" });
                    states.Add(new SelectListItem { Text = "Baño Mujeres", Value = "32" });
                    break;

                //Principal Planta Baja
                case "32":
                    states.Add(new SelectListItem { Text = "Área", Value = "" });
                    states.Add(new SelectListItem { Text = "Servicios Escolares ", Value = "33" });
                    states.Add(new SelectListItem { Text = "Laboratorio de Turismo Rural", Value = "34" });
                    states.Add(new SelectListItem { Text = "Auditorio", Value = "35" });
                    states.Add(new SelectListItem { Text = "Rectoria", Value = "36" });
                    states.Add(new SelectListItem { Text = "Explanada", Value = "37" });
                    states.Add(new SelectListItem { Text = "Baño hombres", Value = "38" });
                    states.Add(new SelectListItem { Text = "Baño Mujeres", Value = "39" });
                    states.Add(new SelectListItem { Text = "Enfermeria", Value = "40" });
                    states.Add(new SelectListItem { Text = "Almacén de Mantenimiento", Value = "41" });

                    break;

                //Biologia Planta Alta
                case "41":
                    states.Add(new SelectListItem { Text = "Área", Value = "" });
                    states.Add(new SelectListItem { Text = "Laboratorio de Usos Multiples", Value = "42" });
                    states.Add(new SelectListItem { Text = "Laboratorio de Microbiología y Biotecnología", Value = "43" });
                    states.Add(new SelectListItem { Text = "Almacén de Reactivos", Value = "44" });
                    break;

                //Biologia Planta Baja
                case "42":
                    states.Add(new SelectListItem { Text = "Área", Value = "" });
                    states.Add(new SelectListItem { Text = "Maquinas y Herramientas", Value = "45" });
                    states.Add(new SelectListItem { Text = "Centro de Computo 2", Value = "46" });
                    states.Add(new SelectListItem { Text = "Laboratorio de Física", Value = "47" });
                    states.Add(new SelectListItem { Text = "Baño hombres", Value = "48" });
                    states.Add(new SelectListItem { Text = "Baño Mujeres", Value = "49" });
                    states.Add(new SelectListItem { Text = "Cubo de Profesor Cristian", Value = "50" });
                    break;


                //Cafeteria Planta Alta
                case "52":
                    states.Add(new SelectListItem { Text = "Área", Value = "" });
                    states.Add(new SelectListItem { Text = "Cafeteria", Value = "51" });
                    states.Add(new SelectListItem { Text = "Baño hombres", Value = "52" });
                    states.Add(new SelectListItem { Text = "Baño Mujeres", Value = "53" });
                    break;

                //Cafeteria Planta Baja
                case "51":
                    states.Add(new SelectListItem { Text = "Área", Value = "" });
                    states.Add(new SelectListItem { Text = "Sum", Value = "54" });
                    states.Add(new SelectListItem { Text = "Baño hombres", Value = "55" });
                    states.Add(new SelectListItem { Text = "Baño Mujeres", Value = "56" });
                    break;

                default:
                    states.Add(new SelectListItem { Text = "Área", Value = "" });
                    break;

            }
            return Json(new SelectList(states, "Value", "Text"));
        }


        // GET: Incidencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidencia incidencia = db.Incidencias.Find(id);
            if (incidencia == null)
            {
                return HttpNotFound();
            }
            return View(incidencia);
        }

        // GET: Incidencias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Incidencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "Id,Nombre,Descripcion,edificio,planta,NombreArea, image")] Incidencia incidencia,
           Ubicacion ubicacion, Area area, HttpPostedFileBase image, string edificio, string areas)
        {
            if (image != null)
            {
                string nombre = "";

                nombre = image.FileName;
                string ruta = Path.Combine(Server.MapPath("~/Content/Evidencias"), Path.GetFileName(nombre));
                image.SaveAs(ruta);

                incidencia.IdUbicacion = Convert.ToInt32(edificio);
                incidencia.Fecha = DateTime.UtcNow;
                incidencia.IdArea = Convert.ToInt32(areas);
                incidencia.Imagen = nombre;
                incidencia.Estado = 1;

                db.Incidencias.Add(incidencia);
                db.SaveChanges();

            }
            //if (ModelState.IsValid)
            //{
            //    if (image != null)
            //    {
            //        using (MemoryStream ms = new MemoryStream())
            //        {
            //            image.InputStream.CopyTo(ms);
            //            byte[] array = ms.GetBuffer();

            //            var context = new Incidencia();

            //            ubicacion.Area = area;
            //            incidencia.Fecha = DateTime.UtcNow;
            //            incidencia.Ubicacion = ubicacion;
            //            incidencia.Imagen = array;
            //            incidencia.Estado = "No Atendido";

            //            db.Incidencias.Add(incidencia);

            //            db.SaveChanges();
            //        }
            //    }
            //    else
            //    {
            //        ubicacion.Area = area;
            //        incidencia.Fecha = DateTime.UtcNow;
            //        incidencia.Ubicacion = ubicacion;
            //        incidencia.Estado = "No Atendido";

            //        db.Incidencias.Add(incidencia);

            //        db.SaveChanges();
            //    }




            Thread.Sleep(2500);
                return RedirectToAction("Login", "Account");
            }

            //return View(incidencia);
        


        //public ActionResult Imagenes(int id)
        //{

        //    byte[] imageData = db.Incidencias.FirstOrDefault(i => i.Id == id)?.Imagen;
        //    if (imageData != null)
        //    {

        //        return File(imageData, "image/png");
        //    }

        //    return null;
        //}



        // GET: Incidencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidencia incidencia = db.Incidencias.Find(id);
            if (incidencia == null)
            {
                return HttpNotFound();
            }
            return View(incidencia);
        }

        // POST: Incidencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Descripcion,UbicacionId,Prioridad,Fecha,Estado")] Incidencia incidencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidencia);
        }

        // GET: Incidencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidencia incidencia = db.Incidencias.Find(id);
            if (incidencia == null)
            {
                return HttpNotFound();
            }
            return View(incidencia);
        }

        // POST: Incidencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incidencia incidencia = db.Incidencias.Find(id);
            db.Incidencias.Remove(incidencia);
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
