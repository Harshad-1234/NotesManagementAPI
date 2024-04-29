namespace NotesManagementAPI.Models
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public string AccessToken { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
       
    }


 

    public class RegisterUesrRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }   
        public bool IsActive { get; set; } = true;
        public string mobile { get; set; } 

    }
    public class UesrProfileResponse
    {
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }

    }

    public class EditUesrProfileRequest
    {
        public int UserId { get; set; } 
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }

    }
    public class EditUesrProfileResposne
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }

    }
    public class ChangePasswordRequest
    {
        public string NewPassword { get; set; }

    }


}
