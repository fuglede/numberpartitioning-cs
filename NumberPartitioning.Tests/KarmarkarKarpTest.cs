using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NumberPartitioning.Tests
{
    public class KarmarkarKarpTest
    {
        [Fact]
        public void TestHeuristicWorks()
        {
            var numbers = new [] {4, 6, 7, 5, 8};
            var (partition, sizes) = KarmarkarKarp.Heuristic(numbers, 3);
            Assert.Equal(new [] { 8, 11, 11 }, sizes);
            Assert.Equal(
                new List<int>[] {new() {8}, new() {4, 7}, new() {5, 6}},
                partition);
        }

        [Fact]
        public void TestHeuristicCanGiveIndices()
        {
            var numbers = new [] { 4, 6, 7, 5, 8 };
            var (partition, sizes) = KarmarkarKarp.Heuristic(numbers, 3, true);
            Assert.Equal(new[] { 8, 11, 11 }, sizes);
            Assert.Equal(
                new List<int>[] { new() { 4 }, new() { 0, 2 }, new() { 3, 1 } },
                partition);
        }

        [Fact]
        public void TestLargeProblemGivesMeaningfulSizeDifference()
        {
            var numbers = Enumerable.Range(800, 400).ToArray();
            var (partition, sizes) = KarmarkarKarp.Heuristic(numbers, 7);
            Assert.Equal(603, sizes.Max() - sizes.Min());
            Assert.Equal(sizes, partition.Select(x => x.Sum()));
        }
    }
}
