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
            var numbers = new double[] { 4, 6, 7, 5, 8 };
            var (partition, sizes) = Greedy.Heuristic(numbers, 3);
            Assert.Equal(new double[] { 8, 11, 11 }, sizes);
            Assert.Equal(
                new List<int>[] { new() { 4 }, new() { 2, 0 }, new() { 1, 3 } },
                partition);
        }

        [Fact]
        public void TestHeuristicWithGenericElements()
        {
            var weights = new double[] { 4, 6, 7, 5, 8 };
            var elements = new[] { "foo", "bar", "baz", "faz", "boo" };
            var (partition, sizes) = Greedy.Heuristic(elements, weights, 3);
            Assert.Equal(new double[] { 8, 11, 11 }, sizes);
            Assert.Equal(
                new List<string>[] { new() { "boo" }, new() { "baz", "foo" }, new() { "bar", "faz" } },
                partition);
        }

        [Fact]
        public void TestLargeProblemGivesMeaningfulSizeDifference()
        {
            var numbers = Enumerable.Range(800, 400).Select(x => (double)x).ToArray();
            var (partition, sizes) = Greedy.Heuristic(numbers, 7);
            Assert.Equal(799, sizes.Max() - sizes.Min());
            var expectedSizes = partition.Select(part => part.Select(i => numbers[i]).Sum());
            Assert.Equal(sizes, expectedSizes);
        }
    }
}
