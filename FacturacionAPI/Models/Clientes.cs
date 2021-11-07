using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FacturacionAPI.Models
{
    public partial class Clientes
    {
        public Clientes()
        {
            AsientosContables = new HashSet<AsientosContables>();
            Facturacion = new HashSet<Facturacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rnc { get; set; }
        public string CuentaContable { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<AsientosContables> AsientosContables { get; set; }
        public virtual ICollection<Facturacion> Facturacion { get; set; }
    }
}
