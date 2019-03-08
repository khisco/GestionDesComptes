using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestionBank.Data.Entities;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace GestionBank.Data.Services
{
    public class GestionImpl : IGestion
    {
        private readonly GestionContext gestion;
        private readonly IMapper mapper;

        public LinkGenerator Link { get; }

        public GestionImpl(GestionContext gestion,IMapper mapper,LinkGenerator link )
        {
            this.gestion = gestion;
            this.mapper = mapper;
            Link = link;
        }
        
        public void Add<T>(T entity) where T : class
        {
            gestion.Add(entity);
        }

        public async Task<Compte> ConsulterCompte(int id1, int id2)
        {
            IQueryable<Compte> query = gestion.Comptes
                .Where(c => c.Client.Clientid == id1)
                .Where(c => c.CompteId == id2);
            return await query.FirstOrDefaultAsync();

        }

        public async Task<Compte[]> ConsulterLesComptes(int id)
        {
            IQueryable<Compte> query = gestion.Comptes
                .Where(c => c.Client.Clientid == id)
                .OrderBy(c=>c.CompteId);
            return await query.ToArrayAsync();
        }

        public void Delete<T>(T entity) where T : class
        {
            gestion.Remove(entity);
        }

        public async Task<Client[]> GetAllClients(bool includeComptes = false)
        {
            IQueryable<Client> query = gestion.Clients;
            if (includeComptes)
            {

                query = query.Include(c => c.Comptes);
            }
            query = query.OrderBy(c => c.Clientid);
            return await query.ToArrayAsync();

        }
     
        public async Task<Client> GetClient(int id,bool includeComptes = false)
        {
            IQueryable<Client> query = gestion.Clients
                .Where(c => c.Clientid == id)
                ;
            if (includeComptes)
            {
                query = query.Include(c => c.Comptes);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Compte> retirer(int id, double montant)
        {
            IQueryable<Compte> query = from compte in gestion.Comptes
                                        
                                        where compte.CompteId == id
                                        select compte;
            foreach(Compte compte in query)
            {
                compte.Solde -= montant;
            }
            return await query.FirstOrDefaultAsync();



        }

        public async  Task<bool> SaveChanges()
        {
            return (await gestion.SaveChangesAsync()) > 0;
        }

        public async Task<Compte> Virement(int id1, int id2, double montant)
        {
            IQueryable<Compte> query1 = from compte in gestion.Comptes

                                       where compte.CompteId == id1
                                       select compte;
            IQueryable<Compte> query2 = from compte in gestion.Comptes

                                        where compte.CompteId == id2
                                        select compte;
            query1.First().Solde -= montant;
            query2.First().Solde += montant;
            return await query1.FirstOrDefaultAsync();
        }

        public async Task<Compte> Virser(int id, double montant)
        {
            IQueryable<Compte> query = from compte in gestion.Comptes

                                       where compte.CompteId == id
                                       select compte;
            foreach (Compte compte in query)
            {
                compte.Solde += montant;
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
