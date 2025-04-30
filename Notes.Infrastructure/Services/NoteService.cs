using Notes.Web.API.Domain.Entities;

namespace Notes.Infrastructure.Services
{
    public class NoteService : INoteService
    {
        private readonly IGenericRepository<Note> _noteRepository;

        public NoteService(IGenericRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<NoteDto?> GetByIdAsync(Guid id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            return note == null ? new NoteDto() : new NoteDto
            {
                NoteId = note.NoteId,
                Title = note.Title,
                Content = note.Content
            };
        }

        public async Task<List<NoteDto>> GetAllAsync()
        {
            var notes = await _noteRepository.GetAllAsync();
            return notes.Select(n => new NoteDto
            {
                NoteId = n.NoteId,
                Title = n.Title,
                Content = n.Content
            }).ToList();
        }

        public async Task<NoteDto> CreateAsync(CreateNoteDto dto)
        {
            var note = new Note
            {
                NoteId = Guid.NewGuid(),
                Title = dto.Title,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _noteRepository.AddAsync(note);

            return new NoteDto
            {
                NoteId = note.NoteId,
                Title = note.Title,
                Content = note.Content
            };
        }

        public async Task<NoteDto?> UpdateAsync(UpdateNoteDto dto)
        {
            var note = await _noteRepository.GetByIdAsync(dto.NoteId);
            if (note == null) return null;

            note.Title = dto.Title;
            note.Content = dto.Content;

            await _noteRepository.UpdateAsync(note);

            return new NoteDto
            {
                NoteId = note.NoteId,
                Title = note.Title,
                Content = note.Content
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            await _noteRepository.DeleteAsync(id); // performs soft delete
        }
    }
}
