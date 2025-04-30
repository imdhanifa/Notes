namespace Notes.Application.Features.Notes.Commands
{
    public class UpdateNoteCommand : IRequest<NoteDto>
    {
        public Guid NoteId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
