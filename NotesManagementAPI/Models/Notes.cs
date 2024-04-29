namespace NotesManagementAPI.Models
{
    public class AddNotesRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class Notes
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }

    public class NotesV1
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifedDate { get; set; }

    }

    public class DeleteNotesRequest
    {
        public int Id { get; set; }

    }

    public class UpdateNotesRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }

    public class GetAllNotesByUserIdResponse
    {
     public int UserId { get; set; }   

     public List<NotesV1> Notes { get; set; }


    }

}
