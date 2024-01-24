using FundoRepositoryLayer.Context;
using FundoRepositoryLayer.Entity;
using FundoRepositoryLayer.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoRepositoryLayer.Service
{
    public class LabelRepo : ILabelRepo
    {
        private readonly FundoContext labelContext;

        public LabelRepo(FundoContext labelContext)
        {
            this.labelContext = labelContext;
        }

        public LabelEntity AddLabel(long userId, long noteId, string labelName)
        {
            NoteEntity note = labelContext.Notes.Where(n => n.UsertId == userId && n.NoteId == noteId).FirstOrDefault();
            if (note != null)
            {
                try
                {
                    LabelEntity label = new LabelEntity();
                    label.UsertId = userId;
                    label.NoteId = noteId;
                    label.LabelName = labelName;
                    labelContext.Labels.Add(label);
                    labelContext.SaveChanges();
                    return label;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            else { 
                return null; 
            }   

        }

        public List<LabelEntity> GetAllLabels()
        {
            List<LabelEntity> lstLabels = new List<LabelEntity>();
            try
            {
                lstLabels = labelContext.Labels.ToList();
            }
            catch (Exception ex)
            {
                lstLabels = null;
            }
            return lstLabels;
        }


        public bool DeleteLabel(long userId, long noteId,long labelId) { 
        
        var result = labelContext.Labels.Where(l => l.UsertId == userId && l.NoteId == noteId && l.LabelId == labelId).FirstOrDefault();
        if (result != null)
            {
                labelContext.Labels.Remove(result);
                labelContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        
        
        
        }

        public bool UpdateLabel(long userId, long noteId, string newlabelName,long labelId)
        {
            var label = labelContext.Labels.Where(l => l.UsertId == userId && l.NoteId == noteId && l.LabelId == labelId).FirstOrDefault();
            if (label != null)
            {
                label.LabelName = newlabelName;
                labelContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

      




    }
}
