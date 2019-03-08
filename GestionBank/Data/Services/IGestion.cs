using GestionBank.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBank.Data.Services
{
    public interface IGestion
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChanges();
        Task<Compte[]> ConsulterLesComptes(int id);
        Task<Compte> ConsulterCompte(int id1,int id2);
        Task<Client[]> GetAllClients(bool includeComptes=false);
        Task<Client> GetClient(int id,bool includeComptes = false);
        Task<Compte> Virser(int id,double montant);
        Task<Compte> retirer(int id, double montant);
        Task<Compte> Virement(int id1,int id2, double montant);

    }
}
