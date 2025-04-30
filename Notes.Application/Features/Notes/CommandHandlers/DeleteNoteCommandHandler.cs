namespace Notes.Application.Features.Notes.CommandHandlers
{
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, NoteDto>
    {
        private readonly INoteService _noteService;
        public DeleteNoteCommandHandler(INoteService noteService) => _noteService = noteService;

        public async Task<NoteDto> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteService.GetByIdAsync(request.Id);
            if (note == null) throw new Exception("Note not found");
            await _noteService.DeleteAsync(request.Id);
            return note;
        }
    }
}
