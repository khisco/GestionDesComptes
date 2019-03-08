using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBank.Data.Entities
{
    public class Compte
    {
        public int CompteId { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateExpiration { get; set; }
        public double Solde { get; set; }
        public TypeCompt Type { get; set; }
        public Client Client { get; set; }

    }
}
