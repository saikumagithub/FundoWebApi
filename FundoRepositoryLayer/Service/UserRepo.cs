using FundoRepositoryLayer.Context;
using FundoRepositoryLayer.Entity;
using FundoRepositoryLayer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FundoRepositoryLayer.Service
{
    public class UserRepo : IUserRepo
    {
        private readonly FundoContext fundoContext;
        private readonly IConfiguration _config;
        public UserRepo(FundoContext fundoContext, IConfiguration _config)
        {
            this.fundoContext = fundoContext;
            this._config = _config;
        }


        public UserEntity FindName(string name)
        {
            UserEntity user =  fundoContext.Users.Where(u => u.FirstName == name).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            return null;
        }
        public UserEntity UserRegistration(RegisterModel registerModel)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.FirstName = registerModel.FirstName;
            userEntity.LastName = registerModel.LastName;
            userEntity.Email = registerModel.Email;
            userEntity.Password = registerModel.Password;

            fundoContext.Users.Add(userEntity);
            fundoContext.SaveChanges();

            return userEntity;
        }

        public string LoginWithEmailAndPassword(string email,string password)
        {

             UserEntity user = fundoContext.Users.Where(u => u.Email == email).FirstOrDefault();
                if (user != null) {
                    if (user.Email == email && user.Password == password)
                    {
                    var token = GenerateToken(user.UsertId, user.Email);
                    return token;
                    }
                }
                
                return null;

        }

        public bool CheckEmail(string email)
        {
            
            UserEntity user = fundoContext.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;

        }

        // To generate token
        private string GenerateToken(long userId,string userEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",userEmail),
                new Claim("UserId",userId.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public ForgotPasswordModel UserForgotPassword(string email)
        {

            var userDetails = fundoContext.Users.Where(u => u.Email == email).FirstOrDefault();
            ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel();
            forgotPasswordModel.Email = userDetails.Email;
            forgotPasswordModel.Token  = GenerateToken(userDetails.UsertId,userDetails.Email);
            forgotPasswordModel.UserId = userDetails.UsertId;
            return forgotPasswordModel;

        }

        //forgot password method
        public bool ResetPassword(string email, string password, string updatePassword)
        {
            if (password.Equals(updatePassword)) {
                UserEntity user = fundoContext.Users.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    user.Password = updatePassword;
                    fundoContext.SaveChanges();
                    return true;
                }

            }
            return false;
        }

        public List<UserEntity> GetAllUsers()
        {
            List<UserEntity> lstUsers = new List<UserEntity>();
            try
            {
                lstUsers = fundoContext.Users.ToList();
            }
            catch (Exception ex)
            {
                lstUsers = null;
            }
            return lstUsers;
        }

        public UserEntity LoginSessionManagement(string email,string password)
        {
            UserEntity user = fundoContext.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                if (user.Email == email && user.Password == password)
                {
                    
                    return user;
                }
            }

            return null;
        }

       
    }
}
