using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IncidenciasUnisierra.Models
{
    public class Area
    {
        [Key]
        public int Id { get; set; }
        public String NombreArea { get; set; }
        public int Planta { get; set; }
        public int UbicacionId { get; set; }

        public virtual Ubicacion Ubicacion { get; set; }

    }
}