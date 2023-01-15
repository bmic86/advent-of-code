using AoCHelper;

namespace AdventOfCode.Year2022
{
    public class Day01 : BaseDay
    {
        private readonly int[][] _elvesInventories;

        public Day01() => _elvesInventories = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int result = _elvesInventories
                .Select(inventory => inventory.Sum())
                .Max();

            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int result = _elvesInventories
                .Select(inventory => inventory.Sum())
                .OrderDescending()
                .Take(3)
                .Sum();

            return new(result.ToString());
        }

        private int[][] LoadData()
            => File.ReadAllText(base.InputFilePath)
                .Split("\n\n")
                .Select(ParseElfInventory)
                .ToArray();

        private static int[] ParseElfInventory(string input)
            => input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
    }
}
