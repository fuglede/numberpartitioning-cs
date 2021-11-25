# Partition problem solvers in .NET

This repository includes some pure C# solvers for the [multiway number partitioning optimization problem](https://en.wikipedia.org/wiki/Multiway_number_partitioning); in particular, the standard [greedy algorithm](https://en.wikipedia.org/wiki/Greedy_number_partitioning), and the [Karmarkar--Karp algorithm](https://en.wikipedia.org/wiki/Largest_differencing_method).

## The problem

Concretely, the problem we solve is the following: Suppose *S* is some collection of integers, and *k* is some positive integer, find a partition of *S* into *k* parts so that the sums of the integers in each part are as close as possible.

The objective function describing "closeness" is usually taken to be the difference between the largest and smallest sum among all parts. The optimization version is NP-hard, and the bundled algorithms only aim to provide a good solution in short time. This also means that they can be useful for other objective functions such as, say, the variance of all sums.

## Installation

The package is available from the public [NuGet Gallery](https://www.nuget.org/packages/NumberPartitioning/).

## Example

Suppose we want to split the collect `[4, 6, 7, 5, 8]` into three parts. We can achieve that as follows:

```cs
var numbers = new[] { 4, 6, 7, 5, 8 };
var (partition, sizes) = KarmarkarKarp.Heuristic(numbers, 3);
```

Here, `partition` becomes `[[4], [0, 3], [1, 2]]`, the indices corresponding to the elements `[[8], [4, 7], [5, 6]]`, and `sizes` are the sums of the elements in each part, `[8, 11, 11]`. This happens to be optimal.

As noted [on Wikipedia](https://en.wikipedia.org/wiki/Largest_differencing_method), an example where this approach does not give the optimal result is the following:

```cs
var numbers = new[] { 5, 5, 5, 4, 4, 3, 3, 1 };
var (partition, sizes) = KarmarkarKarp.Heuristic(numbers, 3);
```

Here, `sizes` is `[9, 10, 11]` but it is possible to achieve a solution in which the sums of each part is 10.
