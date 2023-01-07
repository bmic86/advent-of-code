using AoCHelper;

namespace AdventOfCode.Year2017
{
    public class Day01 : BaseDay
    {
        private readonly string _input;

        public Day01()
            => _input = File.ReadAllText(InputFilePath).Trim();

        public override ValueTask<string> Solve_1()
        {
            var result = _input
                .Aggregate(
                    (LastChar: _input.Last(), Sum: 0),
                    (result, currentChar) =>
                    {
                        int value = 0;
                        if (result.LastChar == currentChar)
                        {
                            value = int.Parse(currentChar.ToString());
                        }
                        return new(currentChar, result.Sum + value);
                    });

            return new(result.Sum.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int halfLength = _input.Length / 2;

            int sum = 0;
            for (int i = 0; i < _input.Length; ++i)
            {
                int halfwayIndex = (i + halfLength) % _input.Length;
                if (_input[i] == _input[halfwayIndex])
                {
                    sum += int.Parse(_input[i].ToString());
                }
            }

            return new(sum.ToString());
        }
    }
}
