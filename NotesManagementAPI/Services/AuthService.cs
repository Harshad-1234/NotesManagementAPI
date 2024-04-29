using NotesManagementAPI.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using NotesManagementAPI.Services.Interfaces;
using System.Text;
using System.Security.Cryptography;

namespace NotesManagementAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _connectionString;

        public AuthService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var user = await db.QueryFirstOrDefaultAsync<LoginResponse>("SELECT UserId, Username, IsActive FROM Users WHERE Username = @Username AND Password = @Password AND IsActive = 1",
                    new { Username = loginRequest.Username, Password = loginRequest.Password });

                if (user != null && user.IsActive)
                {
                    string token = GenerateToken(loginRequest.Username);

                    string sql = "INSERT INTO LoginAccessToken values(@UesrId,@token)";

                    await db.ExecuteScalarAsync<int>(sql, new { UesrId = user.UserId, token = token});

                    return new LoginResponse
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        IsActive = user.IsActive,
                        AccessToken = token,
                    };
                }
                else
                {
                    return null;
                }
            }


        }
        public async Task<bool> RegisterUser(RegisterUesrRequest  registerUesrRequest)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                string sql = "insert into Users values(@Username,@Password,@IsActive,@Email,@MobileNo)  ";
                int affectedRows = await db.ExecuteAsync(sql, new { Username = registerUesrRequest.Username, Password=registerUesrRequest.Password , IsActive=true, Email  =registerUesrRequest.Email , MobileNo = registerUesrRequest.mobile});
               
                return affectedRows > 0;
            }


        }

        public async Task<bool> ChangePassword(ChangePasswordRequest changePasswordRequest,int UserId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                string sql = "Update Users set Password = @Password  where UserId = @UserId ";
                int affectedRows = await db.ExecuteAsync(sql, new { Password = changePasswordRequest.NewPassword , UserId = UserId });

                return affectedRows > 0;
            }


        }


        public async Task<UesrProfileResponse> GetUserProfile(int UserId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT Username , Email , MobileNo from Users WHERE UserId = @UserId ";

                var user = await db.QueryFirstOrDefaultAsync<UesrProfileResponse>(query, new {  UserId = UserId });

                return user;

            }
        }


        public async Task<EditUesrProfileResposne> UpdateUserProfile(EditUesrProfileRequest editUesrProfileRequest)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Users SET Username = @Username , Email=@Email , MobileNo = @MobileNo  WHERE UserId = @UserId";
                await db.ExecuteAsync(sql, editUesrProfileRequest);

                var User = await db.QueryFirstOrDefaultAsync<EditUesrProfileResposne>("SELECT Username , Email, MobileNo   FROM Users WHERE UserId = @UserId", new { UserId = editUesrProfileRequest.UserId });

                return User;
            }
        }


        private string GenerateToken(string username)
        {
            byte[] tokenData = Encoding.UTF8.GetBytes(username + DateTime.UtcNow.ToString());
            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(tokenData);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
