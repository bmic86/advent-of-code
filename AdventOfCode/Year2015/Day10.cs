using AoCHelper;
using System.Globalization;
using System.Text;

namespace AdventOfCode.Year2015
{
    public class Day10 : BaseDay
    {
        private readonly string _inputData;

        public Day10() => _inputData = File.ReadAllText(InputFilePath);

        public override ValueTask<string> Solve_1()
            => new(GenerateLookAndSayLength(_inputData, 40).ToString());

        public override ValueTask<string> Solve_2()
            => new(GenerateLookAndSayLength(_inputData, 50).ToString());

        private static int GenerateLookAndSayLength(string value, int iterations)
        {
            for (int i = 0; i < iterations; ++i)
            {
                value = GenerateLookAndSaySequence(value);
            }

            return value.Length;
        }

        private static string GenerateLookAndSaySequence(string input)
        {
            StringBuilder resultBuilder = new();

            char previous = input[0];
            int count = 0;
            for (int i = 0; i < input.Length; ++i)
            {
                char current = input[i];
                if (current != previous)
                {
                    resultBuilder.Append(count);
                    resultBuilder.Append(previous);

                    count = 0;
                    previous = current;
                }
                count++;
            }

            resultBuilder.Append(count);
            resultBuilder.Append(previous);

            return resultBuilder.ToString();
        }
    }
}
