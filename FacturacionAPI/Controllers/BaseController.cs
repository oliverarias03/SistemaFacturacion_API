using FacturacionAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity> : ControllerBase where TEntity : class
    {
        // protected readonly IStringLocalizer<BaseController<TEntity>> _localizer;
        protected Func<TEntity, bool> existsPredicate { get; set; }

        protected ILogger _logger;
        protected IAuthenticationService _iAuthenticationService;

        protected IRepository<TEntity> _iRepository;
        //protected IRepository<Recursos> _recursos;

        public BaseController() { }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public virtual IActionResult Create(TEntity entity)
        {
            try
            {
                //string errorMessage = this._iRepository.getRecursobyKey("M_ModelValError");
                if (this._iRepository.Exists(this.existsPredicate))
                {
                    //ModelState.AddModelError("Duplicate", this._iRepository.getRecursobyKey("M_EntityExists"));
                    //errorMessage = this._iRepository.getRecursobyKey("M_EntityExists");
                    return Conflict();
                }

                if (ModelState.IsValid)
                {
                    var request = this._iRepository.Add(entity);
                    Dictionary<string, object> response = new Dictionary<string, object>();
                    response.Add("message", "Transaccion Exitosa");
                    response.Add("data", request);

                    return Created("", response);
                }
                else
                {
                    return BadRequest("Los datos enviados no son validos");
                }
            }
            catch (Exception ex)
            {
                //ControllerContext.HttpContext.Response.WriteAsync(string.Format(this._iRepository.getRecursobyKey("M_CreateError"), ex.Message, ex.InnerException, ex.StackTrace, ex.Source));
                return Conflict(ex);
            }
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public virtual IActionResult Delete(TEntity entity)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    this._iRepository.Remove(entity);
                    Dictionary<string, object> response = new Dictionary<string, object>();
                    response.Add("message", "Transaccion Exitosa");
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Los datos enviados no son validos");
                }
            }
            catch (DbUpdateException ex)
            {
                //ControllerContext.HttpContext.Response.WriteAsync(string.Format(this._iRepository.getRecursobyKey("M_EntityUsedError, ex.Message, ex.InnerException, ex.StackTrace, ex.Source")));
                return Conflict(ex);
            }
            catch (Exception ex)
            {
                //ControllerContext.HttpContext.Response.WriteAsync(string.Format(this._iRepository.getRecursobyKey("M_DeleteEntityError, ex.Message, ex.InnerException, ex.StackTrace, ex.Source")));
                return Conflict(ex);
            }
        }

        [HttpPut("Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public virtual IActionResult Edit(TEntity entity)
        {
            try
            {
                //string errorMessage = this._iRepository.getRecursobyKey("M_ModelValError");
                if (existsPredicate != null)
                {
                    if (this._iRepository.Exists(this.existsPredicate))
                    {
                        //ModelState.AddModelError("Duplicate", this._iRepository.getRecursobyKey("M_EntityExists"));
                        //errorMessage = this._iRepository.getRecursobyKey("M_EntityExists");
                        return Conflict();
                    }
                }

                if (ModelState.IsValid)
                {
                    var request = this._iRepository.Update(entity);
                    Dictionary<string, object> response = new Dictionary<string, object>();
                    response.Add("data", request);
                    response.Add("message", "Transaccion Exitosa.");
                    return Ok(response);

                }
                else
                {
                    return BadRequest("Los Parametros enviados han sido incorrectos.");
                }
            }
            catch (DbUpdateException ex)
            {
                ///ControllerContext.HttpContext.Response.WriteAsync(string.Format(this._iRepository.getRecursobyKey("M_UpdateError"), ex.Message, ex.InnerException, ex.StackTrace, ex.Source));
                return Conflict(ex);
            }
            catch (Exception ex)
            {
                // ControllerContext.HttpContext.Response.WriteAsync(string.Format(this._iRepository.getRecursobyKey("M_UpdateError"), ex.Message, ex.InnerException, ex.StackTrace, ex.Source));
                return Conflict(ex);
            }
        }
    }
}
