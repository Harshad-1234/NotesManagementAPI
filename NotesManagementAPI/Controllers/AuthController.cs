using Microsoft.AspNetCore.Mvc;
using NotesManagementAPI.Models;
using NotesManagementAPI.Services;
using NotesManagementAPI.Services.Interfaces;

namespace NotesManagementAPI.Controllers
{
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;
        private readonly INotesService _notesService;

        public AuthController(IAuthService authService, INotesService notesService)
        {
            _authService = authService; 
            _notesService = notesService;   
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await _authService.Login(loginRequest);


            if (response == null)
            {
                return NotFound();
            }

           

            return Ok(response);

        }


        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUesrRequest registerUesrRequest)
        {

            var response = await _authService.RegisterUser(registerUesrRequest);

            return Ok(response);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest, [FromHeader] int UserId, [FromHeader] string accessToken)
        {

            var response = await _authService.ChangePassword(changePasswordRequest,UserId);

            return Ok(response);
        }


        [HttpPut("UpdateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] EditUesrProfileRequest editUesrProfileRequest, [FromHeader] int UserId, [FromHeader] string accessToken)
        {


            var IsValid = await _notesService.VaidateToken(UserId, accessToken);

            if (!IsValid)
            {
                return Unauthorized();
            }


            var user = await _authService.UpdateUserProfile(editUesrProfileRequest);

            return Ok(user);
        }
    }
}
