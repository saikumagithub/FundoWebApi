using FundoRepositoryLayer.Entity;

namespace FundoBusinessLayer.Interface
{
    public interface ILabelBusiness
    {
        LabelEntity AddLabel(long userId, long noteId, string labelName);

        List<LabelEntity> GetAllLabels();

        bool DeleteLabel(long userId, long noteId, long labelId);
        bool UpdateLabel(long userId, long noteId, string newlabelName, long labelId);
    }
}