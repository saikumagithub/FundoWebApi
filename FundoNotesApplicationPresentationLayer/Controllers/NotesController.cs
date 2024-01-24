using FundoBusinessLayer.Interface;
using FundoBusinessLayer.Service;
using FundoRepositoryLayer.Entity;
using FundoRepositoryLayer.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using System.Collections;

namespace FundoNotesApplicationPresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        //declaring a instance variable
        private readonly INotesBusiness notesBusiness;

        // intializing a constructor and creating a object through dependency injection
        public NotesController(INotesBusiness notesBusiness)
        {
             this.notesBusiness = notesBusiness;
        }

        //creating a method  for adding a notes 
       [Authorize]
        [HttpPost]
        [Route("addnote")]
        public ActionResult AddNoteController(NotesModel notesModel)
        {

            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);
            //long userId = 1;
            
            //int userId = (int)HttpContext.Session.GetInt32("userId");
            var result = notesBusiness.AddNote(notesModel, userId);

            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "note is added", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "note added failed", Data = "some error occured try again " });
            }
        }




        //getting all notes by a userid
        [Authorize]
        [HttpGet]
        [Route("getallnotes")]
        public ActionResult GetAllNotesByUserIdController() {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
         
            var userId = long.Parse(useridresult);
            var lstNotes = notesBusiness.GetAllNotes(userId);
            if (lstNotes != null)
            {
                return Ok(new ResponseModel<List<NoteEntity>> { Success = true, Message = "all notes fetched successfully", Data = lstNotes });
            }
            else {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "no notes available under given userid", Data = "some error occured" });

            }

        }

        // update notes by noteid 
        [Authorize]
        [HttpPut]
        [Route("updateNote")]
        public ActionResult UpdatenotesController(long noteId,NotesModel notesModel) {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);
            bool result = notesBusiness.UpdateNote(userId,noteId,notesModel);
            if (result)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = " notes updated successfully", Data = "operation success" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "notes update failed", Data = "some error occured" });
            }


        }

        //delete all notes method by noteId
        [Authorize]
        [HttpDelete]
        [Route("DeleteNote")]
        public ActionResult DeleteNoteController(long noteId)
        {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);
            bool result = notesBusiness.DeleteNoteByNoteId(userId,noteId);
            if (result)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = " notes deleted successfully", Data = "operation success" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "notes deleted failed", Data = "some error occured" });
            }
        }

        //checkingarchivestatus
        [Authorize]
        [HttpPut]
        [Route("ArchiveStaus")]
        public ActionResult CheckArchiveStatusController(long noteId)
        {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);
            bool result = notesBusiness.CheckIsArchive(userId, noteId);
            if (result)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = " archive status changed success", Data = "operation success" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "archive status changed failed", Data = "some error occured" });
            }
        }
        //checkingarchivestatus
        [Authorize]
        [HttpPut]
        [Route("PinStaus")]
        public ActionResult CheckPinStatusController(long noteId)
        {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);
            bool result = notesBusiness.CheckIsPin(userId, noteId);
            if (result)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = " pin status changed success", Data = "operation success" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "pin status changed failed", Data = "some error occured" });
            }
        }
        //checking trash status
        [Authorize]
        [HttpPut]
        [Route("TrashStaus")]
        public ActionResult CheckTrashStatusController(long noteId)
        {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);
            bool result = notesBusiness.CheckIsTrash(userId, noteId);
            if (result)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = " trash status changed success", Data = "operation success" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "trash status changed failed", Data = "some error occured" });
            }
        }
        //adding or updating background color notes
        [Authorize]
        [HttpPut]
        [Route("addupdatecolor")]
        public ActionResult AddUpdateBackGroundColorController(long noteId,string backgroundColor)
        {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);
            bool result = notesBusiness.AddUpdateColor(userId,noteId,backgroundColor);
            if (result)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "color changed", Data = "operation success" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "color changed failed", Data = "some error occured" });
            }
        }
        //adding or updating background color notes
        [Authorize]
        [HttpPut]
        [Route("addupdateremainder")]
        public ActionResult AddUpdateRemainderController(long noteId, DateTime remainder)
        {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);
            
            bool result = notesBusiness.AddUpdateRemainder(userId, noteId, remainder);
            if (result)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "remainder added success", Data = "operation success" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "remainder changed failed", Data = "some error occured" });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("addbackgroundimage")]
        public ActionResult AddBackgroundImageController(long noteId, IFormFile image)
        {
            var useridresult = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userId = long.Parse(useridresult);
            bool result = notesBusiness.AddBackGroundImage(userId, noteId, image);
            if (result)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "image added success", Data = "operation success" });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "image added failed", Data = "some error occured" });
            }

        }

        //[Authorize]
        [HttpGet]
        [Route("getallNotesLabelsColloborators")]
       public ActionResult GetAllDetailsController(long userId) {
        var objectDetails = notesBusiness.GetAllDetails(userId);
        
            
       if (objectDetails != null)
            {
                return Ok(new ResponseModel<ArrayList> { Success = true, Message = "success", Data = objectDetails });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "no details found", Data = "some error occured" });
            }
        }

    }

        

}

