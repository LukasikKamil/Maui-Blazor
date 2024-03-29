using Microsoft.AspNetCore.Mvc; // [FromServices]
using ALLinONE.Shared; // AddNorthwindContext extension method

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddNorthwindContext();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.MapGet("api/categories", (
    [FromServices] NorthwindContext db) => db.Categories)
    .WithName("GetCategories")
    .Produces<Category[]>(StatusCodes.Status200OK);

app.MapGet("api/categories/{id:int}", (
    [FromRoute] int id,
    [FromServices] NorthwindContext db) => db.Categories
    .Where(category => category.CategoryId == id))
    .WithName("GetCategory")
    .Produces<Category[]>(StatusCodes.Status200OK);

app.MapPost("api/categories", async (
    [FromBody] Category category,
    [FromServices] NorthwindContext db) =>
    {
        db.Categories.Add(category);
        await db.SaveChangesAsync();
        return Results.Created($"api/categories/{category.CategoryId}", category);
    }).WithOpenApi()
      .Produces<Category>(StatusCodes.Status201Created);

app.MapPut("api/categories/{id:int}", async (
    [FromRoute] int id,
    [FromBody] Category category,
    [FromServices] NorthwindContext db) =>
    {
        Category? foundCategory = await db.Categories.FindAsync(id);

        if (foundCategory is null)
            return Results.NotFound();

        foundCategory.CategoryName = category.CategoryName;
        foundCategory.Description = category.Description;
        foundCategory.Picture = category.Picture;

        await db.SaveChangesAsync();

        return Results.NoContent();
    }).WithOpenApi()
      .Produces(StatusCodes.Status404NotFound)
      .Produces(StatusCodes.Status204NoContent);

app.MapDelete("api/categories/{id:int}", async (
    [FromRoute] int id,
    [FromServices] NorthwindContext db) =>
    {
        if (await db.Categories.FindAsync(id) is Category category)
        {
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }
        return Results.NotFound();
    }).WithOpenApi()
      .Produces(StatusCodes.Status404NotFound)
      .Produces(StatusCodes.Status204NoContent);

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
