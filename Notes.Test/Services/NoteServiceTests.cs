namespace Notes.Tests.Services
{
    public class NoteServiceTests
    {
        private readonly Mock<IGenericRepository<Note>> _noteRepositoryMock;
        private readonly NoteService _noteService;

        public NoteServiceTests()
        {
            _noteRepositoryMock = new Mock<IGenericRepository<Note>>();
            _noteService = new NoteService(_noteRepositoryMock.Object);
        }

        [Theory]
        [MemberData(nameof(GetNoteTestData))]
        public async Task GetByIdAsync_ShouldReturnCorrectNoteDto(Note note, bool exists)
        {
            _noteRepositoryMock
                .Setup(repo => repo.GetByIdAsync(note.Id))
                .ReturnsAsync(exists ? note : null);

            var result = await _noteService.GetByIdAsync(note.Id);

            if (exists)
            {
                Assert.NotNull(result);
                Assert.Equal(note.Id, result.Id);
                Assert.Equal(note.Title, result.Title);
            }
            else
            {
                Assert.NotNull(result);
                Assert.Equal(Guid.Empty, result.Id); // Empty dto
            }
        }

        public static IEnumerable<object[]> GetNoteTestData()
        {
            yield return new object[] {
                new Note { Id = Guid.NewGuid(), Title = "Sample 1", Content = "Content" }, true
            };
            yield return new object[] {
                new Note { Id = Guid.NewGuid(), Title = "Sample 2", Content = "Content" }, false
            };
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfNoteDtos()
        {
            var notes = new List<Note>
            {
                new Note { Id = Guid.NewGuid(), Title = "Title 1", Content = "Content 1" },
                new Note { Id = Guid.NewGuid(), Title = "Title 2", Content = "Content 2" }
            };

            _noteRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(notes);

            var result = await _noteService.GetAllAsync();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.Title == "Title 1");
            Assert.Contains(result, r => r.Title == "Title 2");
        }

        [Theory]
        [InlineData("Title A", "Content A")]
        [InlineData("Title B", "Content B")]
        public async Task CreateAsync_ShouldCreateNoteAndReturnDto(string title, string content)
        {
            var dto = new CreateNoteDto { Title = title, Content = content };

            Note? capturedNote = null;
            _noteRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Note>()))
                .Callback<Note>(note => capturedNote = note)
                .Returns(Task.CompletedTask);

            var result = await _noteService.CreateAsync(dto);

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(title, result.Title);
            Assert.Equal(content, result.Content);
            Assert.NotNull(capturedNote);
            Assert.Equal(title, capturedNote.Title);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateNote_WhenExists()
        {
            var id = Guid.NewGuid();
            var existingNote = new Note { Id = id, Title = "Old", Content = "Old" };
            var updateDto = new UpdateNoteDto { Id = id, Title = "New", Content = "New" };

            _noteRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existingNote);
            _noteRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Note>())).Returns(Task.CompletedTask);

            var result = await _noteService.UpdateAsync(updateDto);

            Assert.NotNull(result);
            Assert.Equal("New", result.Title);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenNoteNotFound()
        {
            _noteRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as Note);

            var result = await _noteService.UpdateAsync(new UpdateNoteDto { Id = Guid.NewGuid() });

            Assert.Null(result);
        }

        [Theory]
        [InlineData("2a0f39b2-1121-4d94-ae77-abcfd48999ec")]
        [InlineData("7d08eacd-1b41-4c38-9b18-98e1665589d3")]
        public async Task DeleteAsync_ShouldCallRepositoryDelete(string idString)
        {
            var id = Guid.Parse(idString);

            _noteRepositoryMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask).Verifiable();

            await _noteService.DeleteAsync(id);

            _noteRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }
}
