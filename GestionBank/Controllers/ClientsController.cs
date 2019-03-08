using AutoMapper;
using GestionBank.Data.Entities;
using GestionBank.Data.Services;
using GestionBank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBank.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ClientsController:ControllerBase 
    {
        private readonly IGestion gestionnaire;
        private readonly LinkGenerator link;
        private readonly IMapper mapper;

        public ClientsController(IGestion gestionnaire,LinkGenerator link,IMapper mapper)
        {
            this.gestionnaire = gestionnaire;
            this.link = link;
            this.mapper = mapper;
        }

        [HttpGet]
       public async Task<ActionResult<ClientModel[]>> Get(bool  includeComptes=false)
        {
            try
            {
                var result = await gestionnaire.GetAllClients(includeComptes);
                return mapper.Map<ClientModel[]>(result);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClientModel>> Get(int id,bool includeComptes = false)
        {
            try
            {
                var result = await gestionnaire.GetClient(id,includeComptes);
                return mapper.Map<ClientModel>(result);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<ClientModel>> Post(ClientModel model)
        {
            try
            {
                var lien = link.GetPathByAction("Get", "Clients", values: new { name = model.Clientid });
                if (string.IsNullOrWhiteSpace(lien))
                {
                    return BadRequest("link error");
                }
                var client=mapper.Map<Client>(model);
                gestionnaire.Add(client);
                if (await gestionnaire.SaveChanges())
                    return Created(lien, mapper.Map<ClientModel>(client));
                return BadRequest("error");
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ClientModel>> Put(int id, ClientModel model)
        {
            try
            {
                var result = await gestionnaire.GetClient(id);
                if (result == null) return NotFound("Client n'existe pas");
                mapper.Map(model, result);
                if(await gestionnaire.SaveChanges())
                {
                    return mapper.Map<ClientModel>(result);
                }
                return BadRequest("Erroooooooooooor");
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = await gestionnaire.GetClient(id);
                if (client == null) return NotFound("ce client n'existe pas");
                gestionnaire.Delete(client);
                if(await gestionnaire.SaveChanges())
                {
                    return Ok("Done");
                }
                return BadRequest("Error");
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        

    }
}
