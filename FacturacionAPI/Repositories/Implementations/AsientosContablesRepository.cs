using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacturacionAPI.Models;
using FacturacionAPI.Repositories.Interfaces;

namespace FacturacionAPI.Repositories.Implementations
{
    public class AsientosContablesRepository : Repository<AsientosContables>, IAsientosContablesRepository
    {
        public AsientosContablesRepository(Microsoft.EntityFrameworkCore.DbContextOptions<FacturacionContext> options)
        {
            this.Context = new FacturacionContext(options);
        }
    }
}
