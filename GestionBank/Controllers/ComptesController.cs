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
    [Route("/api/clients/{id:int}/[controller]")]
   [ApiController]
    public class ComptesController:ControllerBase 
    {
        private readonly IGestion gestionnaire;
        private readonly IMapper mapper;
        private readonly LinkGenerator link;

        public ComptesController(IGestion gestionnaire ,IMapper mapper , LinkGenerator link )
        {
            this.gestionnaire = gestionnaire;
            this.mapper = mapper;
            this.link = link;
        }
        [HttpGet]
        public async Task<ActionResult<CompteModel[]>> Get(int id)
        {
            try
            {
                var client = await gestionnaire.GetClient(id);
                if (client == null) return BadRequest("aucun client");
                var comptes = await gestionnaire.ConsulterLesComptes(id);
                if (comptes.Count()==0) return NotFound("pas des comptes pour ce client");
                else
                    return mapper.Map<CompteModel[]>(comptes);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                
            }
        }
        [HttpGet("id2:int")]
        public async Task<ActionResult<CompteModel>> Get(int id,int id2)
        {
            try
            {
                var client = await gestionnaire.GetClient(id);
                if (client == null) return BadRequest("id introuvable");
                var compte = await gestionnaire.ConsulterCompte(id,id2);
                if (compte == null) return NotFound("ce compte introuvable");
                else
                    return mapper.Map<CompteModel>(compte);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

        [HttpPost]
        public async Task<ActionResult<CompteModel>> Post(int id, CompteModel model)
        {
            try
            {
                var lien = link.GetPathByAction("Get", "Comptes", values: new { id,id2=model.CompteId });
                var client = await gestionnaire.GetClient(id);
                if (client == null) return BadRequest("id introuvable");
                var compte = mapper.Map<Compte>(model);
                compte.Client = client;
                gestionnaire.Add(compte);
                if(await gestionnaire.SaveChanges())
                {
                    return Created(lien, mapper.Map<CompteModel>(compte));
                }
                return BadRequest("error");
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }
        [HttpPut("{id2:int}")]
        public async Task<ActionResult<CompteModel>> Put(int id,int id2, CompteModel model)
        {
            try
            {
                //var lien = link.GetPathByAction("Get", "Comptes", values: new { id, id2 = model.CompteId });
                var compte = await gestionnaire.ConsulterCompte(id,id2);
                if (compte == null) return BadRequest("id introuvable");
                mapper.Map(model, compte);
                if(await gestionnaire.SaveChanges())
                {
                    return mapper.Map<CompteModel>(compte);
                }
                return BadRequest("error");
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

        [HttpDelete("{id2:int}")]
        public async Task<IActionResult> Delete(int id, int id2)
        {
            try
            {
                
                var client = await gestionnaire.GetClient(id);
                if (client == null) return BadRequest("id introuvable");
                var compte = await gestionnaire.ConsulterCompte(id, id2);
                if (compte == null) return NotFound("ce compte n'existe pas");
                gestionnaire.Delete(compte);
                
                gestionnaire.Delete(compte);
                if (await gestionnaire.SaveChanges())
                {
                    return Ok();
                }
                return BadRequest("error");
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

        [HttpPut("{id2:int}/{operation}/{montant:double}")]
        public async Task<ActionResult<CompteModel>> Putperation(int id,int id2,string operation,double montant)
        {
            try
            {
               // var client = await gestionnaire.GetClient(id);
//if (client == null) return BadRequest("Client Introuvable");
                var compte = await gestionnaire.ConsulterCompte(id,id2);
                if (compte == null) return NotFound("Compte n'existe pas");
                if (operation.Equals("retirer")) { 
                if (compte.Solde >= montant)
                {
                    compte.Solde -= montant;
                    if (await gestionnaire.SaveChanges())
                        return mapper.Map<CompteModel>(compte);
                    else
                        return BadRequest("error");
                }
                else
                {
                    return BadRequest("votre solde insufaisant");
                }
                }
                if (operation.Equals("verser"))
                {
                    compte.Solde += montant;
                    if (await gestionnaire.SaveChanges())
                        return Ok($"versement a ete effectue avec succes {compte.Solde}");
                    else
                        return BadRequest("error");

                }
                else
                    return BadRequest("Error");
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{id1:int}/virement/{id2:int}/{id3:int}/{montant:double}")]
        public async Task<ActionResult<CompteModel>> Putperation(int id, int id1, int id2,int id3 , double montant)
        {
            try
            {
                var client1 = await gestionnaire.GetClient(id);
               if (client1 == null) return BadRequest("Client Introuvable");
               var client2 = await gestionnaire.GetClient(id2);
                if (client2 == null) return BadRequest("Client Introuvable");
                var compte1 = await gestionnaire.ConsulterCompte(id, id1);
                if (compte1 == null) return NotFound("Compte n'existe pas");
                var compte2 = await gestionnaire.ConsulterCompte(id2, id3);
                if (compte2 == null) return NotFound("Compte n'existe pas");
                if (compte1.Solde >= montant)
                {
                    compte1.Solde -= montant;
                   
                    compte2.Solde += montant;
                    if (await gestionnaire.SaveChanges())
                    {
                        return Ok($"virement a ete effectue avec succes {montant}");
                    }
                    else
                    {
                        return BadRequest("Error de virement");
                    }
                    
                }
                else
                    return BadRequest("solde insufaisant");
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


    }
}
