using FundoBusinessLayer.Interface;
using FundoRepositoryLayer.Entity;
using FundoRepositoryLayer.Interface;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoBusinessLayer.Service
{
    public class UserBusiness : IUserBusiness
    {

        private readonly IUserRepo repo;

        public UserBusiness(IUserRepo repo)
        {
            this.repo = repo;
        }

        public UserEntity FindName(string name)
        {
            return repo.FindName(name);
        }
        public UserEntity UserRegistration(RegisterModel registerModel)
        {

            return repo.UserRegistration(registerModel);
        }

        public string LoginWithEmailAndPassword(string email, string password)
        {
            return repo.LoginWithEmailAndPassword(email, password);
        }

        public bool CheckEmailBusiness(string email)
        {
            return repo.CheckEmail(email);
        }
        public ForgotPasswordModel UserForgotPassword(string email)
        {
            return repo.UserForgotPassword(email);
        }

        public bool ResetPassword(string email, string password, string updatePassword)
        {
            return repo.ResetPassword(email, password, updatePassword);
        }

        public List<UserEntity> GetAllUsers()
        {
            return repo.GetAllUsers();
        }
        public UserEntity LoginSessionManagement(string email, string password)
        {
            return repo.LoginSessionManagement(email, password);
        }
    }
}
