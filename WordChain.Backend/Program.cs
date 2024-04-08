using Microsoft.AspNetCore.Mvc;
using WordChain.Backend;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IPathFindAlgorithm, AStarPathFindAlgorithm>();
builder.Services.AddSingleton<IWords, Words>();
builder.Services.AddSingleton<IWordFactory, WordFactory>();
builder.Services.AddSingleton<IWordsGraphFactory, WordsGraphFactory>();
builder.Services.AddSingleton<IWordsGraphInstance, WordsGraphInstance>();
builder.Services.AddSingleton<IHeuristicCostFunction, CharacterDiff>();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapPost(
        "/wordchain",
        (
            [FromBody] WordChainRequest request,
            [FromServices] IPathFindAlgorithm generator,
            [FromServices] IWordsGraphInstance graph
        ) =>
        {
            var value = graph.GetValue();
            var res = generator.Generate(value, request.Source, request.Target);

            return Results.Ok(res.Select(x => x.Word));
        }
    )
    .WithName("PostWordChain")
    .WithOpenApi();

app.MapGet(
        "/words",
        ([FromServices] IWords words) =>
        {
            return Results.Ok(words.GetWords());
        }
    )
    .WithName("GetWords")
    .WithOpenApi();

app.Run();
