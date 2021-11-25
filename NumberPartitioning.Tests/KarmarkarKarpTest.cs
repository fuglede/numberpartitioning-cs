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
            var numbers = new double[] { 4, 6, 7, 5, 8 };
            var (partition, sizes) = KarmarkarKarp.Heuristic(numbers, 3, true);
            Assert.Equal(new double[] { 8, 11, 11 }, sizes);
            Assert.Equal(
                new List<int>[] { new() { 4 }, new() { 0, 2 }, new() { 3, 1 } },
                partition);
        }

        [Fact]
        public void TestHeuristicSuboptimal()
        {
            var numbers = new double[] { 5, 5, 5, 4, 4, 3, 3, 1 };
            var (partition, sizes) = KarmarkarKarp.Heuristic(numbers, 3);
            Assert.Equal(new double[] { 9, 10, 11 }, sizes);
            Assert.Equal(
                new List<int>[] { new() { 3, 1 }, new() { 7, 4, 0 }, new() { 5, 6, 2 } },
                partition);
        }

        [Fact]
        public void TestLargeProblemGivesMeaningfulSizeDifference()
        {
            var numbers = Enumerable.Range(800, 400).Select(x => (double)x).ToArray();
            var (partition, sizes) = KarmarkarKarp.Heuristic(numbers, 7);
            Assert.Equal(603, sizes.Max() - sizes.Min());
            var expectedSizes = partition.Select(part => part.Select(i => numbers[i]).Sum());
            Assert.Equal(sizes, expectedSizes);
        }
    }
}
