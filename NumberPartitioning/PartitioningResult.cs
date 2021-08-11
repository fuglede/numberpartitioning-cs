using System.Collections.Generic;

namespace NumberPartitioning
{
    /// <summary>
    /// Represents a partition of a given number set.
    /// </summary>
    public class PartitioningResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartitioningResult"/>
        /// class with a given partition and partition sizes.
        /// </summary>
        /// <param name="partition">The partition as a list of arrays of integers. The
        /// integers can represent either the indices in a number set or the
        /// numbers themselves.</param>
        /// <param name="sizes">The sums of the values in the partition.</param>
        public PartitioningResult(List<int>[] partition, int[] sizes)
        {
            Partition = partition;
            Sizes = sizes;
        }

        /// <summary>
        /// The partition as a list of arrays of integers. The
        /// integers can represent either the indices in a number set or the
        /// numbers themselves.
        /// </summary>
        public List<int>[] Partition { get; }

        /// <summary>
        /// The sums of the values in the partition.
        /// </summary>
        public int[] Sizes { get; }

        public void Deconstruct(out List<int>[] partition, out int[] sizes)
        {
            partition = Partition;
            sizes = Sizes;
        }
    } 
}
