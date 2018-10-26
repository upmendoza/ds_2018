using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IncidenciasUnisierra.Models
{
    public class Ubicacion
    {
        [Key]
        public int Id { get; set; }

        public string Edificio { get; set; }

        public virtual Collection<Area> Areas { get; set; }

    }
}