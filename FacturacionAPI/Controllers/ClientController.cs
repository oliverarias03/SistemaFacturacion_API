using FacturacionAPI.Models;
using FacturacionAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseController<Clientes>
    {
        private readonly IClientesRepository _clientRepo;
        public ClientController(IClientesRepository clientRepo)
        {
            _clientRepo = clientRepo;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            return Ok(_clientRepo.GetAll().ToList());
        }

        [HttpGet("{rnc}")]
        public IActionResult GetClient(string rnc)
        {
            var client = _clientRepo.GetAllBy(u => u.Rnc == rnc).ToList();

            if (client == null)
                return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public override IActionResult Create(Clientes model)
        {
            if (_clientRepo.Exists(x => x.Rnc == model.Rnc))
                return BadRequest("Este RNC ya existe.");

            model.Id = 0;
            var res = _clientRepo.Add(model);

            return Ok(res);
        }

        [HttpPut]
        public override IActionResult Edit(Clientes model)
        {
            if (_clientRepo.Exists(x => x.Rnc == model.Rnc && x.Id != model.Id))
                return BadRequest("Este RNC ya existe.");

            var res = _clientRepo.Update(model);
            return Ok(res);
        }

        [HttpDelete("{clientId}")]
        public IActionResult Delete(int clientId)
        {
            try
            {
                var client = _clientRepo.Find(clientId);

                if (client == null) return BadRequest("Ha ocurrido un error buscando el cliente.");

                _clientRepo.Remove(client);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
