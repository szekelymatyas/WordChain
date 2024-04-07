// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


// Configure the HTTP request pipeline.

namespace WordChain;

public class Node
{
    public HashSet<Edge> OutGoing { get; } = new HashSet<Edge>();
}

public class Edge
{
    public required Node From { get; init; }
    public required Node To { get; init; }
}
