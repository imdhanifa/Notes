namespace Notes.Application.Features.Notes.Queries
{
    public class GetNoteByIdQuery : IRequest<NoteDto>
    {
        public Guid Id { get; set; }
        public GetNoteByIdQuery(Guid id) => Id = id;
    }
}
