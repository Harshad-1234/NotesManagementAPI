using Microsoft.AspNetCore.Mvc;
using NotesManagementAPI.Models;
using NotesManagementAPI.Services.Interfaces;

namespace NotesManagementAPI.Controllers
{
    public class NotesController : Controller
    {

        private readonly INotesService _notesService;
        private readonly IAuthService _authService;

        public NotesController(INotesService notesService, IAuthService authService)
        {
            _notesService = notesService;
            _authService = authService;
        }

        [HttpPost("AddNote")]
        public async Task<IActionResult> AddNote([FromBody] AddNotesRequest addNotesRequest, [FromHeader] int UserId , [FromHeader] string accessToken)
        {
           

            var IsValid = await _notesService.VaidateToken(UserId, accessToken);

            if (!IsValid)
            {
                return Unauthorized();
            }

            var note = await _notesService.AddNote(addNotesRequest, UserId);

            return Ok(note);   
        }


        [HttpDelete("DeleteNote/{id}")]
        public async Task<IActionResult> DeleteNote(int id, [FromHeader] int UserId, [FromHeader] string accessToken)
        {
            

            var IsValid = await _notesService.VaidateToken(UserId, accessToken);

            if (!IsValid)
            {
                return Unauthorized();
            }

            var response = await _notesService.DeleteNote(id);

            return Ok(response);
        }



        [HttpPut("UpdateNote")]
        public async Task<IActionResult> UpdateNote([FromBody] UpdateNotesRequest updateNotesRequest, [FromHeader] int UserId, [FromHeader] string accessToken)
        {
  

            var IsValid = await _notesService.VaidateToken(UserId, accessToken);

            if (!IsValid)
            {
                return Unauthorized();
            }
           

            var note = await _notesService.UpdateNote(updateNotesRequest);  

            return Ok(note);
        }

        [HttpGet("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile([FromHeader] int UserId, [FromHeader] string accessToken)
        {


            var IsValid = await _notesService.VaidateToken(UserId, accessToken);

            if (!IsValid)
            {
                return Unauthorized();
            }

            var response = await _authService.GetUserProfile(UserId);

            return Ok(response);
        }


        [HttpGet("GetAllNotesByUserId")]
        public async Task<IActionResult> GetAllNotesByUserId([FromHeader] int UserId, [FromHeader] string accessToken)
        {
            

            var IsValid = await _notesService.VaidateToken(UserId, accessToken);

            if (!IsValid)
            {
                return Unauthorized();
            }

            var response = await _notesService.GetAllNotesByUserId(UserId);

            return Ok(response) ;
        }

        [HttpGet("GetNoteById/{Id}")]
        public async Task<IActionResult> GetNoteById(int Id, [FromHeader] int UserId, [FromHeader] string accessToken)
        {
       

            var IsValid = await _notesService.VaidateToken(UserId, accessToken);

            var noteId = Id;

            if (!IsValid)
            {
                return Unauthorized();
            }

            var resonse = await _notesService.GetNoteById(noteId,UserId);

            return Ok(resonse);
        }


    
    }
}
