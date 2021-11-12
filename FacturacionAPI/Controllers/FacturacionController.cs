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
        private readonly IVendedoresRepository _vendedoresRepository;
        private readonly IClientesRepository _clientesRepository;
        private readonly IArticulosRepository _articulosRepository;

        public FacturacionController(IFacturacionRepository facturacionRepository, IVendedoresRepository vendedoresRepository, IClientesRepository clientesRepository, IArticulosRepository articulosRepository)
        {
            this._factuacionRespository = facturacionRepository;
            this._vendedoresRepository = vendedoresRepository;
            this._clientesRepository = clientesRepository;
            this._articulosRepository = articulosRepository;
        }

        // GET: api/<FacturacionController>
        [HttpGet("GetFacturaciones")]
        public ActionResult GetFacturacion()
        {
            var res = this._factuacionRespository.GetAll().ToList();
            foreach (Facturacion f in res)
            {
                f.IdArticuloNavigation = this._articulosRepository.GetAllBy(x => x.Id == f.IdArticulo).FirstOrDefault();
                f.IdVendedorNavigation = this._vendedoresRepository.GetAllBy(x => x.Id == f.IdVendedor).FirstOrDefault();
                f.IdClienteNavigation = this._clientesRepository.GetAllBy(x => x.Id == f.IdCliente).FirstOrDefault();
            }
            return Ok(res);
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
            Facturacion facturacion = new Facturacion();
            facturacion.IdVendedor = entity.IdVendedor;
            facturacion.IdArticulo = entity.IdArticulo;
            facturacion.IdCliente = entity.IdCliente;
            facturacion.Fecha = entity.Fecha;
            facturacion.Comentario = entity.Comentario;
            facturacion.Cantidad = entity.Cantidad;
            facturacion.PrecioUnitario = entity.PrecioUnitario;

            if (this._factuacionRespository.Exists(x => x.Id == entity.Id))
            {
                return BadRequest("Id Existente");
            }
            else
            {

                facturacion.Id = 0;

                var res = this._factuacionRespository.Add(facturacion);

                return Ok(res);
            }

        }

        // PUT api/<FacturacionController>/5
        [HttpPost("Editar")]
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
        [HttpPost("Eliminar")]
        public IActionResult Eliminar(Facturacion factura)
        {
            try
            {
                if (!this._factuacionRespository.Exists(x => x.Id == factura.Id))
                {
                    return BadRequest("Factura no existente");
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
