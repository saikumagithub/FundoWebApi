using FundoRepositoryLayer.Entity;

namespace FundoBusinessLayer.Interface
{
    public interface IColloboratorBusiness
    {
        CollaboratorEntity AddColloborator(long userId, long noteId, string email);
    }
}