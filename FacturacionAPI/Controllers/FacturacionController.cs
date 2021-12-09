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
        private readonly IVFacturacionRepository _vfacturacionRepository;

        public FacturacionController(IFacturacionRepository facturacionRepository, IVendedoresRepository vendedoresRepository, IClientesRepository clientesRepository, IArticulosRepository articulosRepository, IVFacturacionRepository vfacturacionRepository)
        {
            this._factuacionRespository = facturacionRepository;
            this._vendedoresRepository = vendedoresRepository;
            this._clientesRepository = clientesRepository;
            this._articulosRepository = articulosRepository;
            this._vfacturacionRepository = vfacturacionRepository;
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

        [HttpGet("searchFacturas")]
        public IActionResult searchFacturas(DateTime desde, DateTime hasta)
        {
            if(hasta.Year > 2020)
            {
                var facturas = this._vfacturacionRepository.GetAllBy(u => u.Fecha >= desde && u.Fecha <= hasta).ToList();

                if (facturas == null)
                {
                    return NotFound();
                }

                return Ok(facturas);
            }
            else
            {
                var facturas = this._vfacturacionRepository.GetAllBy(u => u.Fecha >= desde).ToList();

                if (facturas == null)
                {
                    return NotFound();
                }

                return Ok(facturas);
            }

        }

        [HttpGet("searchFacturas2")]
        public IActionResult searchFacturas2(DateTime desde, DateTime hasta)
        {
            if (hasta.Year > 2020)
            {
                var facturas = this._factuacionRespository.GetAllBy(u => u.Fecha >= desde && u.Fecha <= hasta).ToList();
                foreach (Facturacion f in facturas)
                {
                    f.IdArticuloNavigation = this._articulosRepository.GetAllBy(x => x.Id == f.IdArticulo).FirstOrDefault();
                    f.IdVendedorNavigation = this._vendedoresRepository.GetAllBy(x => x.Id == f.IdVendedor).FirstOrDefault();
                    f.IdClienteNavigation = this._clientesRepository.GetAllBy(x => x.Id == f.IdCliente).FirstOrDefault();
                }

                if (facturas == null)
                {
                    return NotFound();
                }

                return Ok(facturas);
            }
            else
            {
                var facturas = this._factuacionRespository.GetAllBy(u => u.Fecha >= desde).ToList();
                foreach (Facturacion f in facturas)
                {
                    f.IdArticuloNavigation = this._articulosRepository.GetAllBy(x => x.Id == f.IdArticulo).FirstOrDefault();
                    f.IdVendedorNavigation = this._vendedoresRepository.GetAllBy(x => x.Id == f.IdVendedor).FirstOrDefault();
                    f.IdClienteNavigation = this._clientesRepository.GetAllBy(x => x.Id == f.IdCliente).FirstOrDefault();
                }

                if (facturas == null)
                {
                    return NotFound();
                }

                return Ok(facturas);
            }

        }

        // PUT api/<FacturacionController>/5
        [HttpPost("UpdateInvoices")]
        public IActionResult UpdateInvoices(Contabilizar c)
        {

            if (c.Hasta.Year > 2020)
            {
                var facturas = this._factuacionRespository.GetAllBy(u => u.Fecha >= c.Desde && u.Fecha <= c.Hasta && u.IdAsiento == null).ToList();
                foreach (Facturacion f in facturas)
                {
                    f.IdAsiento = c.IdAsiento;
                }


                if (facturas == null)
                {
                    return NotFound();
                }

                return Ok(this._factuacionRespository.UpdateRange(facturas));
            }
            else
            {
                var facturas = this._factuacionRespository.GetAllBy(u => u.Fecha >= c.Desde && u.IdAsiento == null).ToList();
                foreach (Facturacion f in facturas)
                {
                    f.IdAsiento = c.IdAsiento;
                }

                if (facturas == null)
                {
                    return NotFound();
                }

                return Ok(this._factuacionRespository.UpdateRange(facturas));
            }
        }

    }
}
