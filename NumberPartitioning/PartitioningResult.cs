using System.Collections.Generic;

namespace NumberPartitioning
{
    public class PartitioningResult
    {
        public PartitioningResult(List<int>[] partition, int[] sizes)
        {
            Partition = partition;
            Sizes = sizes;
        }

        public List<int>[] Partition { get; }
        public int[] Sizes { get; }

        public void Deconstruct(out List<int>[] partition, out int[] sizes)
        {
            partition = Partition;
            sizes = Sizes;
        }
    } 
}
