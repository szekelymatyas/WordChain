// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


// Configure the HTTP request pipeline.

namespace WordChain;

public class WordsGraph
{
    public required Dictionary<string, Node> Nodes { get; init; }
}

public class Node
{
    public Guid GroupId { get; set; } = Guid.NewGuid();
    public required string Word { get; init; }
    public Dictionary<string, Edge> OutEdges { get; } = new();
    public Dictionary<string, Edge> InEdges { get; } = new();
}

public class Edge
{
    public required Node From { get; init; }
    public required Node To { get; init; }
}
