namespace Notes.Application.Features.Notes.CommandHandlers
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, NoteDto>
    {
        private readonly INoteService _noteService;
        public UpdateNoteCommandHandler(INoteService noteService) => _noteService = noteService;

        public Task<NoteDto> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
            => _noteService.UpdateAsync(new UpdateNoteDto { NoteId = request.NoteId, Title = request.Title, Content = request.Content });
    }
}
