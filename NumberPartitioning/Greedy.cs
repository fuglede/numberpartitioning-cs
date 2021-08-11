using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberPartitioning
{
    public static class Greedy
    {
        public static PartitioningResult Heuristic(int[] numbers, int numParts, bool returnIndices = false, bool preSorted = false)
        {
            var indexSortingMap = Enumerable.Range(0, numbers.Length).ToArray();
            if (!preSorted)
            {
                Array.Sort(numbers, indexSortingMap);
                numbers = numbers.Reverse().ToArray();
                indexSortingMap = indexSortingMap.Reverse().ToArray();
            }
            var sizes = new int[numParts];
            var partition = new List<int>[numParts];
            for (var i = 0; i < numParts; i++)
                partition[i] = new List<int>();
            for (var i = 0; i < numbers.Length; i++)
            {
                var number = numbers[i];
                var lowest = int.MaxValue;
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
                partition[lowestIndex].Add(returnIndices ? indexSortingMap[i] : number);
            }

            return new PartitioningResult(partition, sizes);
        }
    }
}
