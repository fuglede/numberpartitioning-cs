using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberPartitioning
{
    public static class KarmarkarKarp
    {
        /// <summary>
        /// Solves the partition problem using the Karmarkar--Karp algorithm.
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

            var partitions = new FastPriorityQueue<PartitionNode>(numbers.Length);
            for (var i = 0; i < numbers.Length; i++)
            {
                var number = numbers[i];
                var thisPartition = new List<int>[numParts];
                for (var n = 0; n < numParts - 1; n++)
                    thisPartition[n] = new List<int>();
                thisPartition[numParts - 1] = new List<int> { indexSortingMap[i] };
                var thisSum = new double[numParts];
                thisSum[numParts - 1] = number;
                var thisNode = new PartitionNode { Sizes = thisSum, Partition = thisPartition };
                partitions.Enqueue(thisNode, -(float)number);
            }

            for (var i = 0; i < numbers.Length - 1; i++)
            {
                var node1 = partitions.Dequeue();
                var node2 = partitions.Dequeue();
                var newPartition = new List<int>[numParts];
                var newSizes = new double[numParts];
                for (var k = 0; k < numParts; k++)
                {
                    newSizes[k] = node1.Sizes[k] + node2.Sizes[numParts - k - 1];
                    node1.Partition[k].AddRange(node2.Partition[numParts - k - 1]);
                    newPartition[k] = node1.Partition[k];
                }

                Array.Sort(newSizes, newPartition);
                var newNode = new PartitionNode { Sizes = newSizes, Partition = newPartition };
                var diff = newSizes[numParts - 1] - newSizes[0];
                partitions.Enqueue(newNode, -(float)diff);
            }

            var node = partitions.Dequeue();
            return new PartitioningResult(node.Partition, node.Sizes);
        }

        private class PartitionNode : FastPriorityQueueNode
        {
            public List<int>[] Partition { get; set; }
            public double[] Sizes { get; set; }
        }
    }
}
