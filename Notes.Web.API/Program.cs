var builder = WebApplication.CreateBuilder(args);

// 1. Register DbContext (In-Memory)

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("NotesDb"));

// sql
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// 2. Dependency Injection

// Register open generic repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register domain-specific service
builder.Services.AddScoped<INoteService, NoteService>();

// Register MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// 3. OpenAPI / Swagger Setup
builder.Services.AddOpenApi(); // Assuming this is an extension method


// 4. Add Auth Middleware Support
builder.Services.AddAuthorization(); // Fixes "AddAuthorization" error


// 5. Build the app
var app = builder.Build();


// 6. Configure Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Assuming this maps Swagger UI & endpoints
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 7. Map Endpoints
app.MapNotesEndpoints(); // Assuming you use Minimal APIs

await app.RunAsync();