namespace Notes.Application.DTOs
{
    public class UpdateNoteDto
    {
        public Guid NoteId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
