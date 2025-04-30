namespace Notes.Web.API.Endpoints
{
    public static class NotesEndpoints
    {
        public static void MapNotesEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/notes", async (CreateNoteCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/api/notes/{result.NoteId}", result);
            });

            app.MapGet("/api/notes", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllNotesQuery());
                return Results.Ok(result);
            });

            app.MapGet("/api/notes/{id}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetNoteByIdQuery(id));
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapPut("/api/notes/{id}", async (Guid id, UpdateNoteCommand command, IMediator mediator) =>
            {
                if (id != command.NoteId) return Results.BadRequest("ID mismatch");

                var result = await mediator.Send(command);
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            app.MapDelete("/api/notes/{id}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteNoteCommand(id));
                return result is null ? Results.NotFound() : Results.Ok(result);
            });
        }
    }
}
