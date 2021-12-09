using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionAPI.Models
{
    public class Contabilizar
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
        public int IdAsiento { get; set; }
    }
}
