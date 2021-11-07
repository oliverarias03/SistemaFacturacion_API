using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FacturacionAPI.Models
{
    public partial class Facturacion
    {
        public int Id { get; set; }
        public int? IdVendedor { get; set; }
        public int? IdCliente { get; set; }
        public int? IdArticulo { get; set; }
        public DateTime? Fecha { get; set; }
        public string Comentario { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }

        public virtual Articulos IdArticuloNavigation { get; set; }
        public virtual Clientes IdClienteNavigation { get; set; }
        public virtual Vendedores IdVendedorNavigation { get; set; }
    }
}
