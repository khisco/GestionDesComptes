using GestionBank.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBank.Models
{
    public class ClientModel
    {
        public int Clientid { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public Sex sex { get; set; }
        public ICollection<CompteModel> Comptes { get; set; }
    }
}
