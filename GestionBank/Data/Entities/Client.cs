using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBank.Data.Entities
{
    public class Client
    {
        public int Clientid { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public Sex sex { get; set; }
        public ICollection<Compte> Comptes { get; set; }
        
    }
}
