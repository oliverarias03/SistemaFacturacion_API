using FacturacionAPI.Models;
using FacturacionAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionAPI.Repositories.Implementations
{
    public class VFacturacionRepository : Repository<VFacturacion>, IVFacturacionRepository
    {
        public VFacturacionRepository(Microsoft.EntityFrameworkCore.DbContextOptions<FacturacionContext> options)
        {
            this.Context = new FacturacionContext(options);
        }
    }
}
