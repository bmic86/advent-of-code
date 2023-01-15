using AoCHelper;

namespace AdventOfCode.Year2021
{
    public class Day01 : BaseDay
    {
        private const int SliceWindowSize = 3;

        private readonly int[] _depths;

        public Day01() => _depths = LoadData();

        public override ValueTask<string> Solve_1()
            => new(SumIncreasingDepths(_depths).ToString());

        public override ValueTask<string> Solve_2()
        {
            List<int> sums = SumDepthsInSliceWindow();
            return new(SumIncreasingDepths(sums).ToString());
        }

        private static int SumIncreasingDepths(IEnumerable<int> depths)
            => depths
                .Skip(1)
                .Aggregate(
                    (Previous: depths.First(), Sum: 0),
                    (acc, current) =>
                    {
                        int sum = (current > acc.Previous)
                            ? acc.Sum + 1
                            : acc.Sum;

                        return (current, sum);
                    })
                .Sum;

        private List<int> SumDepthsInSliceWindow()
        {
            List<int> depthSums = new();
            for (int i = 0; i <= _depths.Length - SliceWindowSize; i++)
            {
                int sum = _depths[i..(i + SliceWindowSize)].Sum();
                depthSums.Add(sum);
            }

            return depthSums;
        }

        private int[] LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(int.Parse)
                .ToArray();
    }
}
