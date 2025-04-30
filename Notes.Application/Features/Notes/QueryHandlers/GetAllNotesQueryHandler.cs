namespace Notes.Application.Features.Notes.QueryHandlers
{
    public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery, List<NoteDto>>
    {
        private readonly INoteService _noteService;
        public GetAllNotesQueryHandler(INoteService noteService) => _noteService = noteService;

        public Task<List<NoteDto>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
            => _noteService.GetAllAsync();
    }
}
