using Notes.Domain.Common;

namespace Notes.Web.API.Domain.Entities
{
    public class Note:BaseEntity
    {
        public long NoteId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
