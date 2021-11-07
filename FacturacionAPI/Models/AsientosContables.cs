using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FacturacionAPI.Models
{
    public partial class AsientosContables
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int? IdCliente { get; set; }
        public string Cuenta { get; set; }
        public string TipoMovimiento { get; set; }
        public DateTime? FechaAsiento { get; set; }
        public decimal? MontoAsiento { get; set; }
        public string Estado { get; set; }

        public virtual Clientes IdClienteNavigation { get; set; }
    }
}
