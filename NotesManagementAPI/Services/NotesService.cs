using Dapper;
using Microsoft.AspNetCore.Mvc;
using NotesManagementAPI.Models;
using NotesManagementAPI.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace NotesManagementAPI.Services
{
    public class NotesService : INotesService
    {
        private readonly string _connectionString;

        public NotesService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Notes> AddNote(AddNotesRequest addNotesRequest, int UserId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Notes (UserId, Title, Description, CreatedDate , ModifedDate) VALUES (@UserId, @Title, @Description, GETDATE() , GETDATE() );" +
                             "SELECT SCOPE_IDENTITY();";

                int newNoteId = await db.ExecuteScalarAsync<int>(sql, new { UserId = UserId, Title = addNotesRequest.Title, Description = addNotesRequest.Description });


                return new Notes
                {
                    Id = newNoteId,
                    UserId = UserId,
                    Title = addNotesRequest.Title,
                    Description = addNotesRequest.Description
                };
            }
        }

        public async Task<bool> DeleteNote(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Notes WHERE NoteId = @Id";
                int affectedRows = await db.ExecuteAsync(sql, new { Id = id });

                return affectedRows > 0;
            }
        }


        public async Task<Notes> UpdateNote(UpdateNotesRequest updateNotesRequest)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Notes SET Title = @Title, Description = @Description , ModifedDate = GETDATE() WHERE NoteId = @Id";
                await db.ExecuteAsync(sql, updateNotesRequest);

                var Notes = await db.QueryFirstOrDefaultAsync<Notes>("SELECT NoteId as Id , UserId, Title, Description FROM Notes WHERE NoteId = @NoteId", new { NoteId = updateNotesRequest.Id });

                return Notes;
            }
        }


        public async Task<GetAllNotesByUserIdResponse> GetAllNotesByUserId(int UserId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT NoteId as Id , UserId, Title, Description , CreatedDate , ModifedDate FROM Notes WHERE UserId = @UserId order by CreatedDate desc";

                var notes = await db.QueryAsync<NotesV1>(query, new { UserId = UserId });

                return new GetAllNotesByUserIdResponse
                {
                    UserId = UserId,
                    Notes = notes.AsList()
                };
            }


        }
        public async Task<Notes> GetNoteById(int noteId, int UserId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT NoteId as Id , UserId, Title, Description FROM Notes WHERE NoteId = @noteId and UserId = @UserId ";

                var notes = await db.QueryFirstOrDefaultAsync<Notes>(query, new { noteId = noteId, UserId = UserId });

                return notes;

            }


        }


        public async Task<bool> VaidateToken(int UserId, string Token)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "select UserId ,AccessToken from LoginAccessToken where UserId = @UserId and AccessToken = @Token  ";
                int affectedRows = await db.ExecuteScalarAsync<int>(sql, new { UserId = UserId, Token = Token });

                return affectedRows > 0;
            }
        }


      
    }
}
