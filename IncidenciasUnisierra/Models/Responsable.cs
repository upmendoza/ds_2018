using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IncidenciasUnisierra.Models
{
    public class Responsable
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string NombreUsuario { get; set; }

        [NotMapped]
        public string NombreCompleto { get { return string.Format("{0} {1}", Nombre, Apellidos); } set { } } 

        public virtual ICollection<Incidencia> Incidencias { get; set; }
    }
}