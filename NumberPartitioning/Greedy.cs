using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberPartitioning
{
    public static class Greedy
    {
        /// <summary>
        /// Solves the partition problem using the greedy algorithm.
        /// </summary>
        /// <param name="numbers">The numbers to partition.</param>
        /// <param name="numParts">The number of desired parts.</param>
        /// <param name="preSorted">Set to <see langword="true" /> to save time when the input numbers are
        /// already sorted in descending order.</param>
        /// <returns>The partition as a <see cref="PartitioningResult"/>.</returns>
        public static PartitioningResult Heuristic(double[] numbers, int numParts, bool preSorted = false)
        {
            var indexSortingMap = Enumerable.Range(0, numbers.Length).ToArray();
            if (!preSorted)
            {
                Array.Sort(numbers, indexSortingMap);
                numbers = numbers.Reverse().ToArray();
                indexSortingMap = indexSortingMap.Reverse().ToArray();
            }
            var sizes = new double[numParts];
            var partition = new List<int>[numParts];
            for (var i = 0; i < numParts; i++)
                partition[i] = new List<int>();
            for (var i = 0; i < numbers.Length; i++)
            {
                var number = numbers[i];
                var lowest = double.PositiveInfinity;
                int lowestIndex = default;
                for (var j = 0; j < numParts; j++)
                {
                    if (sizes[j] < lowest)
                    {
                        lowest = sizes[j];
                        lowestIndex = j;
                    }
                }

                sizes[lowestIndex] += number;
                partition[lowestIndex].Add(indexSortingMap[i]);
            }

            return new PartitioningResult(partition, sizes);
        }
    }
}
