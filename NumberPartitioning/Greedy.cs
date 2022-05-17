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
        /// <param name="numbers">The numbers to partition into parts of similar sum.</param>
        /// <param name="numParts">The number of desired parts.</param>
        /// <param name="preSorted">Set to <see langword="true" /> to save time when the input numbers are
        /// already sorted in descending order.</param>
        /// <returns>The partition as a <see cref="PartitioningResult{T}"/> whose generic type argument is <see cref="int"/>.</returns>
        public static PartitioningResult<int> Heuristic(double[] numbers, int numParts,
            bool preSorted = false) =>
            Heuristic(Enumerable.Range(0, numbers.Length).ToArray(), numbers, numParts, preSorted);

        /// <summary>
        /// Solves the partition problem using the greedy algorithm.
        /// </summary>
        /// <param name="elements">The elements to partition into parts of similar weight.</param>
        /// <param name="weights">The weights of the elements, assumed to be equal in count to <paramref name="elements"/>.</param>
        /// <param name="numParts">The number of desired parts.</param>
        /// <param name="preSorted">Set to <see langword="true" /> to save time when the input weights are
        /// already sorted in descending order.</param>
        /// <returns>The partition as a <see cref="PartitioningResult{T}"/>.</returns>
        public static PartitioningResult<T> Heuristic<T>(T[] elements, double[] weights, int numParts, bool preSorted = false)
        {
            if (numParts <= 0)
                throw new ArgumentOutOfRangeException(nameof(numParts), $"{numParts} must be positive");

            var indexSortingMap = Enumerable.Range(0, weights.Length).ToArray();
            if (!preSorted)
            {
                Array.Sort(weights, indexSortingMap);
                weights = weights.Reverse().ToArray();
                indexSortingMap = indexSortingMap.Reverse().ToArray();
            }
            var sizes = new double[numParts];
            var partition = new List<T>[numParts];
            for (var i = 0; i < numParts; i++)
                partition[i] = new List<T>();
            for (var i = 0; i < weights.Length; i++)
            {
                var number = weights[i];
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
                partition[lowestIndex].Add(elements[indexSortingMap[i]]);
            }

            return new PartitioningResult<T>(partition, sizes);
        }
    }
}
