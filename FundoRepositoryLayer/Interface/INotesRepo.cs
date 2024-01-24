using FundoRepositoryLayer.Entity;
using Microsoft.AspNetCore.Http;
using ModelLayer.Models;
using System.Collections;

namespace FundoRepositoryLayer.Interface
{
    public interface INotesRepo
    {
        NoteEntity AddNote(NotesModel notesModel, long userId);

        List<NoteEntity> GetAllNotes(long userId);
        bool UpdateNote(long userId, long noteId, NotesModel notesModel);

        bool DeleteNoteByNoteId(long userId, long noteId);

        bool CheckIsArchive(long userId, long noteId);

        bool CheckIsPin(long userId, long noteId);
        bool CheckIsTrash(long userId, long noteId);

        bool AddUpdateColor(long userId, long noteId, string BackGroundColor);

        bool AddUpdateRemainder(long userId, long noteId, DateTime remainder);

        bool AddBackGroundImage(long userId, long noteId, IFormFile image);

        ArrayList GetAllDetails(long userId);


    }
}