using FundoBusinessLayer.Interface;
using FundoRepositoryLayer.Entity;
using FundoRepositoryLayer.Interface;
using Microsoft.AspNetCore.Http;
using ModelLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoBusinessLayer.Service
{
    public class NotesBusiness : INotesBusiness
    {

        private readonly INotesRepo notesRepo;

        // intializing the notesRepo object through constructor
        public NotesBusiness(INotesRepo notesRepo)
        {
            this.notesRepo = notesRepo;
        }

        //adding a note 
        public NoteEntity AddNote(NotesModel notesModel, long userId)
        {
            return notesRepo.AddNote(notesModel, userId);
        }

       public  List<NoteEntity> GetAllNotes(long userId)
        {
            return notesRepo.GetAllNotes(userId);
        }
        public bool UpdateNote(long userId, long noteId, NotesModel notesModel)
        {
            return notesRepo.UpdateNote(userId, noteId, notesModel);
        }

       public  bool DeleteNoteByNoteId(long userId, long noteId)
        {
            return notesRepo.DeleteNoteByNoteId(userId, noteId);
        }
        public bool CheckIsArchive(long userId, long noteId)
        {
            return notesRepo.CheckIsArchive(userId, noteId);
        }

        public bool CheckIsPin(long userId, long noteId)
        {
            return notesRepo.CheckIsPin(userId, noteId);

        }

        public bool CheckIsTrash(long userId, long noteId)
        {
            return notesRepo.CheckIsTrash(userId, noteId);
        }

        public bool AddUpdateColor(long userId, long noteId, string BackGroundColor)
        {
            return notesRepo.AddUpdateColor(userId, noteId, BackGroundColor);
        }
        public bool AddUpdateRemainder(long userId, long noteId, DateTime remainder)
        {
            return notesRepo.AddUpdateRemainder(userId, noteId, remainder);
        }
        public bool AddBackGroundImage(long userId, long noteId, IFormFile image)
        {
            return notesRepo.AddBackGroundImage(userId, noteId, image);
        }
       

        public ArrayList GetAllDetails(long userId)
        {
            return notesRepo.GetAllDetails(userId);
        }
    }
}
