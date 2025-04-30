namespace Notes.Application.Interfaces.Services
{
    public interface INoteService
    {
        Task<NoteDto?> GetByIdAsync(Guid id);
        Task<List<NoteDto>> GetAllAsync();
        Task<NoteDto> CreateAsync(CreateNoteDto dto);
        Task<NoteDto?> UpdateAsync(UpdateNoteDto dto);
        Task DeleteAsync(Guid id);
    }
}
