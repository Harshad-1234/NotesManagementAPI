using NotesManagementAPI.Models;

namespace NotesManagementAPI.Services.Interfaces
{
    public interface INotesService
    {
        Task<Notes> AddNote(AddNotesRequest addNotesRequest, int UserId);
        Task<bool> DeleteNote(int id);
        Task<Notes> UpdateNote(UpdateNotesRequest updateNotesRequest);
        Task<GetAllNotesByUserIdResponse> GetAllNotesByUserId(int UserId);
        Task<Notes> GetNoteById(int noteId, int UserId);
        Task<bool> VaidateToken(int UserId, string Token);
    }
}
