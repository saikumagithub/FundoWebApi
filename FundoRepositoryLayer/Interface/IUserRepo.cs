using FundoRepositoryLayer.Entity;
using ModelLayer.Models;

namespace FundoRepositoryLayer.Interface
{
    public interface IUserRepo
    {
        UserEntity UserRegistration(RegisterModel registerModel);


        string LoginWithEmailAndPassword(string email, string password);

        bool CheckEmail(string email);

        ForgotPasswordModel UserForgotPassword(string email);

        bool ResetPassword(string email, string password, string updatePassword);

        UserEntity FindName(string name);

        List<UserEntity> GetAllUsers();

         UserEntity LoginSessionManagement(string email, string password);
    }

    
}