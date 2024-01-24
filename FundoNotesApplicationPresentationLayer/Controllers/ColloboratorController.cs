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
    public class ColloboratorController : ControllerBase
    {

        private readonly IColloboratorBusiness colloboratorBusiness;
        public ColloboratorController(IColloboratorBusiness colloboratorBusiness)
        {
            this.colloboratorBusiness = colloboratorBusiness;
        }

        [Authorize]
        [HttpPost]
        [Route("addcolloborator")]
        public ActionResult AddColloboratorController(long noteId, string email)
        {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);
            var result = colloboratorBusiness.AddColloborator(userId,noteId,email);
            if (result != null)
            {
                return Ok(new ResponseModel<CollaboratorEntity> { Success = true, Message = "colloborator  is added", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "colloborator added failed", Data = "some error occured try again " });
            }

        }
    }
}
