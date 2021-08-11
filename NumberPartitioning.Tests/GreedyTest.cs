using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NumberPartitioning.Tests
{
    public class GreedyTest
    {
        [Fact]
        public void TestHeuristicWorks()
        {
            var numbers = new[] { 4, 6, 7, 5, 8 };
            var (partition, sizes) = Greedy.Heuristic(numbers, 3);
            Assert.Equal(new[] { 8, 11, 11 }, sizes);
            Assert.Equal(
                new List<int>[] { new() { 8 }, new() { 7, 4 }, new() { 6, 5 } },
                partition);
        }

        [Fact]
        public void TestHeuristicCanGiveIndices()
        {
            var numbers = new[] { 4, 6, 7, 5, 8 };
            var (partition, sizes) = Greedy.Heuristic(numbers, 3, true);
            Assert.Equal(new[] { 8, 11, 11 }, sizes);
            Assert.Equal(
                new List<int>[] { new() { 4 }, new() { 2, 0 }, new() { 1, 3 } },
                partition);
        }

        [Fact]
        public void TestLargeProblemGivesMeaningfulSizeDifference()
        {
            var numbers = Enumerable.Range(800, 400).ToArray();
            var (partition, sizes) = Greedy.Heuristic(numbers, 7);
            Assert.Equal(799, sizes.Max() - sizes.Min());
            Assert.Equal(sizes, partition.Select(x => x.Sum()));
        }
    }
}
