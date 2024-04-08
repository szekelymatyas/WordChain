# WordChain

This is a full-stack project designed to generate word chains from a set of words. The backend is written in C# with ASP.NET, and the frontend is a React app.

## How it works

The words are preloaded and structured into a graph in memory. Upon request, a pathfinding algorithm searches for the shortest path. The current pathfinding algorithm is A*. For more information on this topic, refer to  [Wiki](https://en.wikipedia.org/wiki/A*_search_algorithm)

## How to use

0. Clone the repository
1. Run the command: `docker compose up`
2. Open the following link: [Link](http://localhost:3000)