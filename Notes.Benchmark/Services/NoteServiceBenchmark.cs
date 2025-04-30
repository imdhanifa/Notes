using BenchmarkDotNet.Engines;

namespace Notes.Benchmark.Services
{
    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.Throughput, launchCount: 1, warmupCount: 1, iterationCount: 2)]
    public class NoteServiceBenchmark
    {
        private NoteService _noteService;
        private Mock<IGenericRepository<Note>> _noteRepoMock;

        [GlobalSetup]
        public void Setup()
        {
            _noteRepoMock = new Mock<IGenericRepository<Note>>();

            var mockNotes = Enumerable.Range(1, 100).Select(i => new Note
            {
                Id = Guid.NewGuid(),
                Title = $"Note {i}",
                Content = $"Content {i}"
            }).ToList();

            _noteRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(mockNotes);
            _noteRepoMock.Setup(r => r.AddAsync(It.IsAny<Note>())).Returns(Task.CompletedTask);

            _noteService = new NoteService(_noteRepoMock.Object);
        }

        [Benchmark]
        public async Task GetAllAsync()
        {
            var result = await _noteService.GetAllAsync();
        }

        [Benchmark]
        public async Task CreateAsync()
        {
            var dto = new CreateNoteDto
            {
                Title = "Benchmark",
                Content = "Benchmark content"
            };
            var result = await _noteService.CreateAsync(dto);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _noteService = null;
            _noteRepoMock = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
