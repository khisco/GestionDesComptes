using AutoMapper;
using GestionBank.Data.Entities;
using GestionBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionBank.Data
{
    public class ClientProfile:Profile 
    {
        public ClientProfile()
        {
            this.CreateMap<Client, ClientModel>()
                .ReverseMap();
            this.CreateMap<Compte, CompteModel>()
                .ReverseMap();

        }
    }
}
