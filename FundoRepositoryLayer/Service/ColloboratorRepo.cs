using FundoRepositoryLayer.Context;
using FundoRepositoryLayer.Entity;
using FundoRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoRepositoryLayer.Service
{
    public class ColloboratorRepo : IColloboratorRepo
    {
        private readonly FundoContext ColloboratorContext;

        public ColloboratorRepo(FundoContext ColloboratorContext)
        {
            this.ColloboratorContext = ColloboratorContext;
        }

        public CollaboratorEntity AddColloborator(long userId, long noteId, string email)
        {
            //checking whether the noteid and userid exits or not in the note table
            NoteEntity note = ColloboratorContext.Notes.Where(n => n.UsertId == userId && n.NoteId == noteId).FirstOrDefault();
            if (note != null)
            {
                try
                {
                    CollaboratorEntity collaborator = new CollaboratorEntity();
                    collaborator.UsertId = userId;
                    collaborator.NoteId = noteId;
                    collaborator.Email = email;
                    ColloboratorContext.Collaborators.Add(collaborator);
                    ColloboratorContext.SaveChanges();
                    return collaborator;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            else
            {
                return null;
            }

        }
    }
}
