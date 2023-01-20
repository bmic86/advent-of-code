using AoCHelper;

namespace AdventOfCode.Year2020
{
    public class Day01 : BaseDay
    {
        private const int RequiredSum = 2020;

        private readonly int[] _expenses;

        public Day01() => _expenses = LoadData();

        public override ValueTask<string> Solve_1()
        {
            for (int i = 0; i < _expenses.Length - 1; ++i)
            {
                for (int j = i + 1; j < _expenses.Length; ++j)
                {
                    if (_expenses[i] + _expenses[j] == RequiredSum)
                    {
                        int result = _expenses[i] * _expenses[j];
                        return new(result.ToString());
                    }
                }
            }

            return new("No result.");
        }

        public override ValueTask<string> Solve_2()
        {
            for (int i = 0; i < _expenses.Length - 2; ++i)
            {
                for (int j = i + 1; j < _expenses.Length - 1; ++j)
                {
                    for (int k = j + 1; k < _expenses.Length; ++k)
                    {
                        if (_expenses[i] + _expenses[j] + _expenses[k] == RequiredSum)
                        {
                            int result = _expenses[i] * _expenses[j] * _expenses[k];
                            return new(result.ToString());
                        }
                    }
                }
            }

            return new("No result.");
        }

        private int[] LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(int.Parse)
                .ToArray();
    }
}
