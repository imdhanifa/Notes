namespace Notes.Application.Features.Notes.CommandHandlers
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteDto>
    {
        private readonly INoteService _noteService;
        public CreateNoteCommandHandler(INoteService noteService) => _noteService = noteService;

        public async Task<NoteDto> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
            => await _noteService.CreateAsync(new CreateNoteDto { Title = request.Title, Content = request.Content });
    }
}
