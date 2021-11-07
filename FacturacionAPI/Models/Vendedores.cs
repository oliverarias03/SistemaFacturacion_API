using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FacturacionAPI.Models
{
    public partial class Vendedores
    {
        public Vendedores()
        {
            Facturacion = new HashSet<Facturacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Clave { get; set; }
        public decimal? Comision { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Facturacion> Facturacion { get; set; }
    }
}
