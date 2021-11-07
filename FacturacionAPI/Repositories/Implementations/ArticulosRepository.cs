using FacturacionAPI.Models;
using FacturacionAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionAPI.Repositories.Implementations
{
    public class ArticulosRepository : Repository<Articulos>, IArticulosRepository
    {
        public ArticulosRepository(Microsoft.EntityFrameworkCore.DbContextOptions<FacturacionContext> options)
        {
            this.Context = new FacturacionContext(options);
        }
    }
}
