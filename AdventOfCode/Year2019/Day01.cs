using AoCHelper;

namespace AdventOfCode.Year2019
{
    public class Day01 : BaseDay
    {
        private readonly int[] _moduleMasses;

        public Day01() => _moduleMasses = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int result = _moduleMasses
                .Select(CalculateRequiredFuel)
                .Sum();

            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int result = _moduleMasses
                .Select(CalculateTotalRequiredFuel)
                .Sum();

            return new(result.ToString());
        }

        private static int CalculateRequiredFuel(int mass)
            => mass / 3 - 2;

        private static int CalculateTotalRequiredFuel(int mass)
        {
            int total = 0;
            while ((mass = CalculateRequiredFuel(mass)) > 0)
            {
                total += mass;
            }
            return total;
        }

        private int[] LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(int.Parse)
                .ToArray();
    }
}
