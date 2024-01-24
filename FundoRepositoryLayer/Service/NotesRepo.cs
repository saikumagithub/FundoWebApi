using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundoRepositoryLayer.Context;
using FundoRepositoryLayer.Entity;
using FundoRepositoryLayer.Interface;
using FundoRepositoryLayer.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ModelLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoRepositoryLayer.Service
{
    public class NotesRepo : INotesRepo

    {
        private readonly FundoContext notesContext;
        private readonly IConfiguration configuration;

        
        public NotesRepo(FundoContext fundoContext, IConfiguration configuration)
        {
            this.notesContext = fundoContext;
            this.configuration = configuration;
        }

        public ArrayList GetAllDetails(long userId)
        {
            ArrayList alldetails = new ArrayList();
            var lstNotes = notesContext.Notes.Where( u => u.UsertId == userId).ToList();

            var notesCount = lstNotes.Count;
            var notesmessage = $"no of notes for given userId is {notesCount}";
            alldetails.Add(notesmessage);

            alldetails.Add(lstNotes);


            var labels  = notesContext.Labels.Where(u => u.UsertId == userId).ToList();
            var labelsCount = labels.Count;
            var labelmessage = $"no of labbels for given userId is {labelsCount}";
            alldetails.Add(labelmessage); 
            alldetails.Add(labels);

            var colloborators = notesContext.Collaborators.Where(u => u.UsertId == userId).ToList();
            var collobCount = colloborators.Count;
            var coloMessage = $"no of colloborators for given userId is {collobCount}";
            alldetails.Add(coloMessage);
            alldetails.Add(colloborators);
            

            return alldetails;
               
            
        }
        
        public NoteEntity AddNote(NotesModel notesModel, long userId)
        {
            NoteEntity notesEntity = new NoteEntity();
            notesEntity.Title = notesModel.Title;
            notesEntity.Description = notesModel.Description;
            notesEntity.Remainder = notesModel.Remainder;
            notesEntity.Color = notesModel.Color;
            notesEntity.Image = notesModel.Image;
            notesEntity.IsArchive = notesModel.IsArchive;
            notesEntity.IsPin = notesModel.IsPin;
            notesEntity.IsTrash = notesModel.IsTrash;
            notesEntity.UsertId = userId;

            notesContext.Notes.Add(notesEntity);
            notesContext.SaveChanges();
            return notesEntity;
            

        }

        //getting all notes by l
        //ogged in  user
        public List<NoteEntity> GetAllNotes(long userId)
        {
            List<NoteEntity> lstNotes = new List<NoteEntity>();
            //getting all notes by extension methods
            try
            {
                lstNotes = notesContext.Notes.Where(n => n.UsertId == userId).ToList();

            }
            catch (Exception ex)
            {
                lstNotes = null;
            }
            return lstNotes;
            
            

        }


        //update the notes by noteid  for notes table

        public bool UpdateNote(long userId, long noteId, NotesModel notesModel)
        {
            bool updateStatus = false;
           NoteEntity note  = notesContext.Notes.Where(n => n.UsertId == userId && n.NoteId == noteId).FirstOrDefault();
            try {
                if (note != null)
                {
                    note.Title = notesModel.Title;
                    note.Description = notesModel.Description;
                    note.Remainder = notesModel.Remainder;
                    note.Color = notesModel.Color;
                    note.Image = notesModel.Image;
                    note.IsArchive = notesModel.IsArchive;
                    note.IsPin = notesModel.IsPin;
                    note.IsTrash = notesModel.IsTrash;
                    notesContext.SaveChanges();
                    updateStatus = true;
                }
                else { updateStatus = false; }
            }
            catch(Exception ex)
            {
                updateStatus = false;
            }
            return updateStatus;
            ////similary check all conditions
            //if(note.Title != notesModel.Title &&notesModel.Title != null) { 
            
            //  note.Title = notesModel.Title;
            //}
        }
        //delete all notes 
        public bool DeleteNoteByNoteId(long userId, long noteId)
        {
            bool deleteStatus = false;
            NoteEntity note = notesContext.Notes.Where(n => n.UsertId == userId && n.NoteId == noteId).FirstOrDefault();
            try
            {
                if (note != null)
                {
                    notesContext.Notes.Remove(note);
                    notesContext.SaveChanges();
                    deleteStatus = true;
                }
                else { deleteStatus = false; }
            }
            catch (Exception ex)
            {
                deleteStatus = false;
            }
            return deleteStatus;
        }


        //updating archive status
        public bool CheckIsArchive(long userId,long noteId) {

            bool ArchiveStatus = false;//used for operation tracking status
            NoteEntity note = notesContext.Notes.Where(n => n.UsertId == userId && n.NoteId == noteId).FirstOrDefault();
            try
            {
                if (note != null)
                {   
                    if(note.IsArchive == false ) { 
                    
                       note.IsArchive = true;
                       notesContext.SaveChanges();
                       ArchiveStatus = true;
                    }
                    else
                    {
                    note.IsArchive = false;
                     
                    notesContext.SaveChanges();
                    ArchiveStatus = true;
                    }
                    
                }
                else { 
                    
                    ArchiveStatus = false; 
                }
            }
            catch (Exception ex)
            {
                ArchiveStatus = false;
            }
            return ArchiveStatus;


        }
        //updating pin status
        public bool CheckIsPin(long userId, long noteId)
        {

            bool pinStatus = false;
            NoteEntity note = notesContext.Notes.Where(n => n.UsertId == userId && n.NoteId == noteId).FirstOrDefault();
            try
            {
                if (note != null)
                {
                    if (note.IsPin == true)
                    {

                        note.IsPin = false;
                        notesContext.SaveChanges();
                        pinStatus = true;
                    }
                    else
                    {
                        note.IsPin= true;
                        notesContext.SaveChanges();
                        pinStatus = true;
                    }

                }
                else
                {

                    pinStatus = false;
                }
            }
            catch (Exception ex)
            {
                pinStatus = false;
            }
            return pinStatus;


        }
        //updating trash status
        public bool CheckIsTrash(long userId, long noteId)
        {

            bool trashStatus = false;
            NoteEntity note = notesContext.Notes.Where(n => n.UsertId == userId && n.NoteId == noteId).FirstOrDefault();
            try
            {
                if (note != null)
                {
                    if (note.IsTrash == true)
                    {

                        note.IsTrash = false;
                        notesContext.SaveChanges();
                        trashStatus= true;
                    }
                    else
                    {
                        note.IsTrash = true;
                        notesContext.SaveChanges();
                        trashStatus = true;
                    }

                }
                else
                {

                    trashStatus = false;
                }
            }
            catch (Exception ex)
            {
                trashStatus = false;
            }
            return trashStatus;


        }

        //adding a background color 
        public bool AddUpdateColor(long userId,long noteId,string BackGroundColor) { 
        
        bool addUpdatestatus = false;
        NoteEntity note = notesContext.Notes.Where(n => n.UsertId == userId && n.NoteId == noteId).FirstOrDefault();
            try
            {
                if (note != null)
                {
                    note.Color = BackGroundColor;
                    notesContext.SaveChanges();
                    addUpdatestatus = true;
                }
                else
                {
                    addUpdatestatus = false;
                }
            }catch(Exception ex) { 
                addUpdatestatus = false;
            }
            return addUpdatestatus;


        }

        //adding a remainder
        public bool AddUpdateRemainder(long userId, long noteId, DateTime remainder)
        {

            bool addUpdatestatus = false;
            NoteEntity note = notesContext.Notes.Where(n => n.UsertId == userId && n.NoteId == noteId).FirstOrDefault();
            try
            {
                if (note != null)
                {
                    note.Remainder = remainder;
                    notesContext.SaveChanges();
                    addUpdatestatus = true;
                }
                else
                {
                    addUpdatestatus = false;
                }
            }
            catch (Exception ex)
            {
                addUpdatestatus = false;
            }
            return addUpdatestatus;


        }
     
        public bool AddBackGroundImage(long userId,long noteId,IFormFile image)
        {
            bool imageStatus = false;
            NoteEntity note = notesContext.Notes.Where(n => n.UsertId == userId && n.NoteId == noteId).FirstOrDefault();

            if (note != null)
            {
                Account account = new Account(

                    configuration["CloudinarySettings:CloudName"],
                    configuration["CloudinarySettings:ApiKey"],
                    configuration["CloudinarySettings:ApiSecret"]

                );
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParameters = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, image.OpenReadStream())
                };

                var uploadResult = cloudinary.Upload(uploadParameters);
                string imagePath = uploadResult.Url.ToString();
                note.Image = imagePath;
                notesContext.SaveChanges();
                imageStatus = true;
            }
            else
            {
                imageStatus = false;
            }
            return imageStatus;
        }





    }
}
