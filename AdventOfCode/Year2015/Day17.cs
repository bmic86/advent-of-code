using AoCHelper;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AdventOfCode.Year2015
{
    public class Day17 : BaseDay
    {
        private record MultipleContainers(int Liters, int ContainersCount);

        private const int EggnogLiters = 150;

        private readonly int[] _containerSizes;

        public Day17()
        {
            _containerSizes = File.ReadAllLines(InputFilePath)
                .Select(int.Parse)
                .ToArray();
        }

        public override ValueTask<string> Solve_1()
        {
            int limit = CalculateLimit();

            int result = 0;
            for (int bitmap = 1; bitmap <= limit; bitmap++)
            {
                if (AggregateContainers(bitmap).Liters == EggnogLiters)
                {
                    result++;
                }
            }

            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int limit = CalculateLimit();

            Dictionary<int, int> containersCountMap = new();
            for (int bitmap = 1; bitmap <= limit; bitmap++)
            {
                var containers = AggregateContainers(bitmap);
                if (containers.Liters == EggnogLiters)
                {
                    containersCountMap.TryGetValue(containers.ContainersCount, out int count);
                    containersCountMap[containers.ContainersCount] = count + 1;
                }
            }

            int minCount = containersCountMap.Min(x => x.Key);
            return new(containersCountMap[minCount].ToString());
        }

        private MultipleContainers AggregateContainers(int containersBitmap)
        {
            BitVector32 bitmap = new(containersBitmap);

            int sum = 0;
            int containersCount = 0;
            for (int i = 0; i <= _containerSizes.Length; i++)
            {
                if (bitmap[1 << i])
                {
                    sum += _containerSizes[i];
                    containersCount++;
                }
            }

            return new MultipleContainers(sum, containersCount);
        }

        private int CalculateLimit()
        {
            BitVector32 bits = new();
            for (int i = 0; i < _containerSizes.Length; i++)
            {
                bits[1 << i] = true;
            }

            return bits.Data;
        }
    }
}
