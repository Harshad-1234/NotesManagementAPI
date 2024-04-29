using NotesManagementAPI.Models;

namespace NotesManagementAPI.Services.Interfaces
{
    public interface IAuthService
    {
         Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<bool> RegisterUser(RegisterUesrRequest registerUesrRequest);
        Task<UesrProfileResponse> GetUserProfile(int UserId);
        Task<EditUesrProfileResposne> UpdateUserProfile(EditUesrProfileRequest editUesrProfileRequest);
        Task<bool> ChangePassword(ChangePasswordRequest changePasswordRequest, int UserId);

    }
}
