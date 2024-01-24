using FundoRepositoryLayer.Entity;
using ModelLayer.Models;

namespace FundoBusinessLayer.Interface
{
    public interface IUserBusiness
    {
        UserEntity UserRegistration(RegisterModel registerModel);

        string LoginWithEmailAndPassword(string email, string password);

        bool CheckEmailBusiness(string email);

        ForgotPasswordModel UserForgotPassword(string email);

        bool ResetPassword(string email, string password, string updatePassword);

        UserEntity FindName(string name);

        List<UserEntity> GetAllUsers();
        UserEntity LoginSessionManagement(string email, string password);
    }
}