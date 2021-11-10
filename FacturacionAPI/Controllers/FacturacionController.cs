using FacturacionAPI.Models;
using FacturacionAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturacionController : BaseController<Facturacion>
    {
        private readonly IFacturacionRepository _factuacionRespository;

        public FacturacionController(IFacturacionRepository facturacionRepository)
        {
            this._factuacionRespository = facturacionRepository;
        }

        // GET: api/<FacturacionController>
        [HttpGet("GetFacturaciones")]
        public ActionResult GetFacturacion()
        {
            return Ok(_factuacionRespository.GetAll().ToList());
        }

        // GET api/<FacturacionController>/5
        [HttpGet("GetFacturacionById")]
        public IActionResult GetFacturacion(int id)
        {
            var factura = this._factuacionRespository.GetAllBy(u => u.Id == id).ToList();

            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        // POST api/<FacturacionController>
        [HttpPost("Create")]
        public override IActionResult Create(Facturacion entity)
        {

            if (this._factuacionRespository.Exists(x => x.Id == entity.Id))
            {
                return BadRequest("Id Existente");
            }
            else
            {

                entity.Id = 0;
                var res = this._factuacionRespository.Add(entity);

                return Ok(res);
            }

        }

        // PUT api/<FacturacionController>/5
        [HttpPut("Editar")]
        public override IActionResult Edit(Facturacion entity)
        {
            if (this._factuacionRespository.Exists(x => x.Comentario.ToLower() == entity.Comentario.ToLower() && x.Id != entity.Id))
            {
                return BadRequest("Comentario Existente");
            }
            else
            {
                var res = this._factuacionRespository.Update(entity);

                return Ok(res);
            }
        }

        // DELETE api/<FacturacionController>/5
        [HttpDelete("Eliminar")]
        public IActionResult Eliminar(Facturacion factura)
        {
            try
            {
                if (!this._factuacionRespository.Exists(x => x.Id == factura.Id))
                {
                    return BadRequest("Usuario no existente");
                }
                else
                {
                    Facturacion u = this._factuacionRespository.Find(factura.Id);

                    this._factuacionRespository.Remove(u);

                    return Ok(1);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}
