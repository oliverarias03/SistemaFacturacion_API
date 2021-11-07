using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacturacionAPI.Models;
using FacturacionAPI.Repositories.Interfaces;

namespace FacturacionAPI.Repositories.Implementations
{
    public class VendedoresRepository : Repository<Vendedores>, IVendedoresRepository
    {
        public VendedoresRepository(Microsoft.EntityFrameworkCore.DbContextOptions<FacturacionContext> options)
        {
            this.Context = new FacturacionContext(options);
        }
    }
}
