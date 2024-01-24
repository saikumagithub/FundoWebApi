using FundoRepositoryLayer.Entity;

namespace FundoRepositoryLayer.Interface
{
    public interface IColloboratorRepo
    {
        CollaboratorEntity AddColloborator(long userId, long noteId, string email);
    }
}