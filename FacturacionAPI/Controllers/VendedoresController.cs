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

        [HttpGet("GetVendedores")]
        public IActionResult GetVendedores()
        {
           return Ok(this._vendedoresRepository.GetAll().ToList());
        }


        [HttpGet("GetVendedoresByCedula")]
        public IActionResult GetUsersById(string cedula)
        {
            var users = this._vendedoresRepository.GetAllBy(u => u.Cedula == cedula).ToList();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
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

        [HttpPost("Editar")]
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

        [HttpPost("Eliminar")]
        public IActionResult Eliminar(Vendedores vendedor)
        {
            try
            {
                if (!this._vendedoresRepository.Exists(x => x.Id == vendedor.Id))
                {
                    return BadRequest("Usuario no existente");
                }
                else
                {
                    Vendedores u = this._vendedoresRepository.Find(vendedor.Id);

                    this._vendedoresRepository.Remove(u);

                    return Ok(1);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
