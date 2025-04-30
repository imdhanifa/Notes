namespace Notes.Application.Mappings
{
    public static class NoteMappingExtensions
    {
        public static NoteDto ToDto(this Note note) => new NoteDto
        {
            NoteId = note.NoteId,
            Title = note.Title,
            Content = note.Content,
            CreatedAt = note.CreatedAt
        };

        public static Note ToEntity(this CreateNoteDto createNoteDto) => new Note
        {
            Title = createNoteDto.Title,
            Content = createNoteDto.Content,
            CreatedAt = DateTime.UtcNow
        };

        public static Note UpdateEntity(this Note note, UpdateNoteDto updateNoteDto) => new Note
        {

            Title = updateNoteDto.Title,
            Content = updateNoteDto.Content,
            UpdatedAt = DateTime.UtcNow,
        };
    }
}