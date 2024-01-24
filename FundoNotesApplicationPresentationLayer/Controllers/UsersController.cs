using FundoBusinessLayer.Interface;
using FundoRepositoryLayer.Entity;
using FundoRepositoryLayer.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using NLog;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;
using FundoRepositoryLayer.Context;

namespace FundoNotesApplicationPresentationLayer.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        private readonly IBus bus;
        private readonly IDistributedCache _cache;

        private readonly FundoContext fundoContext;



        public UsersController(IUserBusiness userBusiness, IBus bus, IDistributedCache cache, FundoContext fundoContext)
        {
            this.userBusiness = userBusiness;
            this.bus = bus;
            this._cache = cache;
            this.fundoContext = fundoContext;

        }

        [HttpGet]
        [Route("getname")]
        public ActionResult FindUser(string name)
        {
            var user = userBusiness.FindName(name);
            if (user != null)
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "user  added", Data = user });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "user not added", Data = "Access failed" });
            }
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult UserRegisterControl(RegisterModel registerModel) {
            var user = userBusiness.UserRegistration(registerModel);

            if (user == null)
            {
                return BadRequest(new ResponseModel<string> { Success= false,Message="user not added",Data="Access failed"});
            }
            else
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "user  added", Data = user });
            }

        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Logincontroller(LoginModel loginModel)
        {
            //var email = loginModel.Email;
            //var password = loginModel.Password;
            var  token = userBusiness.LoginWithEmailAndPassword(loginModel.Email,loginModel.Password);

            if(token != null)
            {
                
                return Ok(new ResponseModel<string> { Success = true, Message = "login successful", Data = token  });
            }
            else {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "login not successful", Data = "user not found" });

            }
        }



        [HttpGet]
        [Route("Loginsession")]
        public ActionResult LoginSessionManagementcontroller(string email, string password)
        {
            var user = userBusiness.LoginSessionManagement(email, password);
            int userId = (int)Convert.ToInt64(user.UsertId);

            if (user != null)
            {
                //session management
                HttpContext.Session.SetInt32("userId",userId);
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "login successful", Data = user });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "login not successful", Data = "user not found" });

            }
        }
        [HttpGet]
        [Route("Checkemail")]
        public ActionResult CheckEmailcontroller(string email)
        {
            var result = userBusiness.CheckEmailBusiness(email);

            if (result)
            {
                return Ok(new ResponseModel<String> { Success = true, Message = "email exists", Data = "user found" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "email not found", Data = "user not found" });

            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> UserForgotPassword(string email)
        {
            if (userBusiness.CheckEmailBusiness(email))
            {

                Send send = new Send();
                ForgotPasswordModel forgotPasswordModel = userBusiness.UserForgotPassword(email);
                send.SendingMail(forgotPasswordModel.Email, forgotPasswordModel.Token);
                Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Queue");
                var endPoint = await bus.GetSendEndpoint(uri);
                await endPoint.Send(forgotPasswordModel);
                return Ok(new ResponseModel<string> { Success = true, Message = "email send successful", Data = forgotPasswordModel.Token });
            }
            else {
                return BadRequest(new ResponseModel<string> { Success = true, Message = "email send successful" });
                    
                    
             }
            

            
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public ActionResult ResetPasswordcontroller(string password,string updatedPassword)
        {
            string email = User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
            var result = userBusiness.ResetPassword(email, password, updatedPassword);
            if (result)
            {
                return Ok(new ResponseModel<String> { Success = true, Message = "password updated successfully" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "please provide correct password" });
            }
        }


        //[Authorize]
        [HttpGet]
        [Route("getallusers")]
        public ActionResult GetAllUsersController()
        {
            var lstUsers = userBusiness.GetAllUsers();
            if(lstUsers.Count > 0)
            {
                return Ok(new ResponseModel<List<UserEntity>> { Success = true, Message = "users fetched success", Data = lstUsers });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "no users found", Data = "user not found" });
            }
        }

        
        [HttpGet]
        [Route("allusersbycache")]
        public async Task<List<UserEntity>> GetAllController(bool enableCache)
        {
            if (!enableCache)
            {
                return  userBusiness.GetAllUsers();
            }
            string cacheKey = "allusers";

            // Trying to get data from the Redis cache
            byte[] cachedData = await _cache.GetAsync(cacheKey);
            List<UserEntity> userEntities = new();
            if (cachedData != null)
            {
                // If the data is found in the cache, encode and deserialize cached data.
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                userEntities = JsonSerializer.Deserialize<List<UserEntity>>(cachedDataString);
            }
            else
            {
                // If the data is not found in the cache, then fetch data from database
                userEntities = userBusiness.GetAllUsers();

                // Serializing the data
                string cachedDataString = JsonSerializer.Serialize(userEntities);
                var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                // Setting up the cache options
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                // Add the data into the cache
                await _cache.SetAsync(cacheKey, dataToCache, options);
            }
            return userEntities;
        }


    }
}
