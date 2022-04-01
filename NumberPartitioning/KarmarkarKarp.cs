using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberPartitioning
{
    public static class KarmarkarKarp
    {
        /// <summary>
        /// Solves the partition problem using the greedy algorithm.
        /// </summary>
        /// <param name="numbers">The weights to partition into parts of similar sum.</param>
        /// <param name="numParts">The number of desired parts.</param>
        /// <param name="preSorted">Set to <see langword="true" /> to save time when the input numbers are
        /// already sorted in descending order.</param>
        /// <returns>The partition as a <see cref="PartitioningResult{T}"/> whose generic type argument is <see cref="int"/>.</returns>
        public static PartitioningResult<int> Heuristic(double[] numbers, int numParts,
            bool preSorted = false) =>
            Heuristic(Enumerable.Range(0, numbers.Length).ToArray(), numbers, numParts, preSorted);

        /// <summary>
        /// Solves the partition problem using the Karmarkar--Karp algorithm.
        /// </summary>
        /// <param name="elements">The elements to partition into parts of similar weight.</param>
        /// <param name="weights">The weights of the elements, assumed to be equal in count to <paramref name="elements"/>.</param>
        /// <param name="numParts">The number of desired parts.</param>
        /// <param name="preSorted">Set to <see langword="true" /> to save time when the input weights are
        /// already sorted in descending order.</param>
        /// <returns>The partition as a <see cref="PartitioningResult{T}"/>.</returns>
        public static PartitioningResult<T> Heuristic<T>(T[] elements, double[] weights, int numParts, bool preSorted = false)
        {
            var indexSortingMap = Enumerable.Range(0, weights.Length).ToArray();
            if (!preSorted)
            {
                Array.Sort(weights, indexSortingMap);
                weights = weights.Reverse().ToArray();
                indexSortingMap = indexSortingMap.Reverse().ToArray();
            }

            var partitions = new FastPriorityQueue<PartitionNode<T>>(weights.Length);
            for (var i = 0; i < weights.Length; i++)
            {
                var number = weights[i];
                var thisPartition = new List<T>[numParts];
                for (var n = 0; n < numParts - 1; n++)
                    thisPartition[n] = new List<T>();
                thisPartition[numParts - 1] = new List<T> { elements[indexSortingMap[i]] };
                var thisSum = new double[numParts];
                thisSum[numParts - 1] = number;
                var thisNode = new PartitionNode<T>(thisPartition, thisSum);
                partitions.Enqueue(thisNode, -(float)number);
            }

            for (var i = 0; i < weights.Length - 1; i++)
            {
                var node1 = partitions.Dequeue();
                var node2 = partitions.Dequeue();
                var newPartition = new List<T>[numParts];
                var newSizes = new double[numParts];
                for (var k = 0; k < numParts; k++)
                {
                    newSizes[k] = node1.Sizes[k] + node2.Sizes[numParts - k - 1];
                    node1.Partition[k].AddRange(node2.Partition[numParts - k - 1]);
                    newPartition[k] = node1.Partition[k];
                }

                Array.Sort(newSizes, newPartition);
                var newNode = new PartitionNode<T>(newPartition, newSizes);
                var diff = newSizes[numParts - 1] - newSizes[0];
                partitions.Enqueue(newNode, -(float)diff);
            }

            var node = partitions.Dequeue();
            return new PartitioningResult<T>(node.Partition, node.Sizes);
        }

        private class PartitionNode<T> : FastPriorityQueueNode
        {
            public PartitionNode(List<T>[] partition, double[] sizes)
            {
                Partition = partition;
                Sizes = sizes;
            }

            public List<T>[] Partition { get; }
            public double[] Sizes { get; }
        }
    }
}
