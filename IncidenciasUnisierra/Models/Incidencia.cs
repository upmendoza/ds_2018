using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IncidenciasUnisierra.Models
{
    public class Incidencia
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int IdUbicacion { get; set; }

        public int IdArea { get; set; }

        public int Prioridad { get; set; }

        public DateTime? Fecha { get; set; }

        public string Imagen { get; set; }

        public int Estado { get; set; }

        public int? ResponsableId { get; set; }

        public virtual Responsable Responsable { get; set; }

        public virtual Area Area { get; set; }
    }
}