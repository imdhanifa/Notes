namespace Notes.Application.Features.Notes.Commands
{
    public class DeleteNoteCommand : IRequest<NoteDto>
    {
        public Guid Id { get; set; }
        public DeleteNoteCommand(Guid id) => Id = id;
    }
}
