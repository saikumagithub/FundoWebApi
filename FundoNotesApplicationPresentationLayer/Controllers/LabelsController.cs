using FundoBusinessLayer.Interface;
using FundoBusinessLayer.Service;
using FundoRepositoryLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;

namespace FundoNotesApplicationPresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelBusiness labelBusiness;
        public LabelsController(ILabelBusiness labelBusiness)
        {
            this.labelBusiness = labelBusiness;
        }


        [Authorize]
        [HttpPost]
        [Route("addlabel")]
        public ActionResult AddLabelController(long noteId,string labelName)
        {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);
            var result = labelBusiness.AddLabel(userId,noteId,labelName);
            if(result != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "label is added", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "label added failed", Data = "some error occured try again " });
            }

        }

        [Authorize]
        [HttpGet]
        [Route("getalllabels")]
        public ActionResult GetAllLabelsController() { 
        
         var result = labelBusiness.GetAllLabels();
        if(result != null)
            {
                return Ok(new ResponseModel<List<LabelEntity>> { Success = true, Message = "all labels fetched ", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "no labels present", Data = "some error occured try again " });
            } 
        
        }
        [Authorize]
        [HttpDelete]
        [Route("deleteLabel")]
        public ActionResult DeleteController(long noteId,long labelId)
        {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);

            var result = labelBusiness.DeleteLabel(userId,noteId,labelId);
            if (result)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "label deleted successfully", Data = "operation success" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "no labels present", Data = "some error occured try again " });
            }

        }
        [Authorize]
        [HttpPut]
        [Route("updateLabel")]
        public ActionResult updateLabelController(long noteId, long labelId,string newLabelName)
        {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);

            var result = labelBusiness.UpdateLabel(userId,noteId,newLabelName,labelId);
            if (result)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "label updated successfully", Data = "operation success" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "no labels present", Data = "some error occured try again " });
            }

        }



    }
}
