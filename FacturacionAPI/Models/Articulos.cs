using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FacturacionAPI.Models
{
    public partial class Articulos
    {
        public Articulos()
        {
            Facturacion = new HashSet<Facturacion>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Facturacion> Facturacion { get; set; }
    }
}
