using FundoRepositoryLayer.Entity;
using FundoRepositoryLayer.Interface;
using FundoRepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoBusinessLayer.Interface
{
    public class ColloboratorBusiness : IColloboratorBusiness
    {
        private readonly IColloboratorRepo colloboratorRepo;

        public ColloboratorBusiness(IColloboratorRepo colloboratorRepo)
        {
            this.colloboratorRepo = colloboratorRepo;
        }


        public CollaboratorEntity AddColloborator(long userId, long noteId, string email)
        {
            return colloboratorRepo.AddColloborator(userId, noteId, email);

        }
    }
}
