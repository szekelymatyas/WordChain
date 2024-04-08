using FluentAssertions;
using WordChain.Backend;

namespace WordChain.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var source = "nyerő";
            var target = "gyári";

            string[] words = ["nyerő", "nyers", "nyárs", "nyári", "gyári", "almás"];

            var graphFactory = new WordsGraphFactory();

            var graph = graphFactory.Create(words);

            var id = graph.Nodes["nyerő"].GroupId;
            graph.Nodes.Where(x => x.Value.GroupId == id).Should().HaveCount(5);
        }

        [Fact]
        public void Test2()
        {
            var source = "nyerő";
            var target = "gyári";

            string[] words = ["nyerő", "nyers", "nyárs", "nyári", "gyári", "almás"];
            var graphFactory = new WordsGraphFactory();

            var graph = graphFactory.Create(words);
            var wordChainGenerator = new AStarPathFindAlgorithm(new CharacterDiff());

            var path = wordChainGenerator.Generate(graph, source, target);
        }
    }
}
