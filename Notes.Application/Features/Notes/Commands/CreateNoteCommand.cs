namespace Notes.Application.Features.Notes.Commands
{
    public class CreateNoteCommand : IRequest<NoteDto>
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
