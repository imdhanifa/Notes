namespace Notes.Application.Features.Notes.QueryHandlers
{
    public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteDto>
    {
        private readonly INoteService _noteService;
        public GetNoteByIdQueryHandler(INoteService noteService) => _noteService = noteService;

        public Task<NoteDto> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
            => _noteService.GetByIdAsync(request.Id);
    }
}
