using FundoBusinessLayer.Interface;
using FundoRepositoryLayer.Entity;
using FundoRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoBusinessLayer.Service
{
    public class LabelBusiness : ILabelBusiness
    {

        private readonly ILabelRepo labelRepo;
        public LabelBusiness(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }

        public LabelEntity AddLabel(long userId, long noteId, string labelName)
        {
            return labelRepo.AddLabel(userId, noteId, labelName);
        }

        public List<LabelEntity> GetAllLabels()
        {
            return labelRepo.GetAllLabels();
        }
        public bool DeleteLabel(long userId, long noteId, long labelId)
        {
            return labelRepo.DeleteLabel(userId, noteId,labelId);
        }

       public  bool UpdateLabel(long userId, long noteId, string newlabelName, long labelId)
        {
            return labelRepo.UpdateLabel(userId,noteId,newlabelName,labelId);
        }
    }
}
