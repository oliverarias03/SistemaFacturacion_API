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
    public class ArticulosController : BaseController<Articulos>
    {
        private readonly IArticulosRepository _articulosRepository;

        public ArticulosController(IArticulosRepository articulosRepository)
        {
            this._articulosRepository = articulosRepository;
        }
               
        [HttpGet("GetArticulos")]
        public IActionResult GetArticulos()
        {
           return Ok(this._articulosRepository.GetAll().ToList());
        }

        [HttpGet("GetArticulosById")]
        public IActionResult GetUsersById(int id)
        {
            var items = this._articulosRepository.GetAllBy(u => u.Id == id).ToList();

            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost("Create")]
        public override IActionResult Create(Articulos entity)
        {
            if (this._articulosRepository.Exists(x => x.Id == entity.Id))
            {
                return BadRequest("Codigo Existente");
            }
            else
            {
                entity.Id = 0;
                var result = this._articulosRepository.Add(entity);
                return Ok(result);
            }
        }

        [HttpPut("Editar")]
        public override IActionResult Edit(Articulos entity)
        {
            if (this._articulosRepository.Exists(x => x.Descripcion.ToLower() == entity.Descripcion.ToLower() && x.Id != entity.Id))
            {
                return BadRequest("Descripcion Existente");
            }
            else
            {
                var result = this._articulosRepository.Update(entity);
                return Ok(result);
            }
        }

        [HttpDelete("Eliminar")]
        public IActionResult Eliminar(int id)
        {
            if (!this._articulosRepository.Exists(x => x.Id == id))
            {
                return BadRequest("Articulo no existente");
            }
            else
            {
                Articulos a = this._articulosRepository.Find(id);
                this._articulosRepository.Remove(a);

                return Ok("Articulo Eliminado");
            }

        }
    }
}
