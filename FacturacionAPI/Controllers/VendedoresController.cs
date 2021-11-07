using FacturacionAPI.Models;
using FacturacionAPI.Repositories.Interfaces;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedoresController : BaseController<Vendedores>
    {
        private readonly IVendedoresRepository _vendedoresRepository;

        public VendedoresController(IVendedoresRepository vendedoresRepository)
        {
            this._vendedoresRepository = vendedoresRepository;
        }

        [HttpPost("Login")]
        public IActionResult Login(Vendedores entity)
        {

            if (!this._vendedoresRepository.Exists(x => x.Cedula.ToLower() == entity.Cedula.ToLower() && x.Clave == entity.Clave))
            {
                return BadRequest("Credenciales Incorrectas");
            }
            else
            {
                Vendedores u = this._vendedoresRepository.Find(x => x.Cedula == entity.Cedula);
                return Ok(u);
            }

        }

        [HttpPost("Create")]
        public override IActionResult Create(Vendedores entity)
        {

            if (this._vendedoresRepository.Exists(x => x.Cedula == entity.Cedula))
            {
                return BadRequest("Cedula Existente");
            }
            else
            {

                entity.Id = 0;
                var res = this._vendedoresRepository.Add(entity);

                return Ok(res);
            }

        }

        [HttpPut("Editar")]
        public override IActionResult Edit(Vendedores entity)
        {
            if (this._vendedoresRepository.Exists(x => x.Cedula.ToLower() == entity.Cedula.ToLower() && x.Id != entity.Id))
            {
                return BadRequest("Cedula Existente");
            }
            else
            {
                var res = this._vendedoresRepository.Update(entity);

                return Ok(res);
            }
        }

        [HttpDelete("Eliminar")]
        public IActionResult Eliminar(int id)
        {
            if (!this._vendedoresRepository.Exists(x => x.Id == id))
            {
                return BadRequest("Usuario no existente");
            }
            else
            {
                Vendedores u = this._vendedoresRepository.Find(id);

                this._vendedoresRepository.Remove(u);

                return Ok("Usuario Eliminado");
            }

        }
    }
}
