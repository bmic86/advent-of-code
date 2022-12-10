using AoCHelper;

namespace AdventOfCode.Year2015
{
    public class Day11 : BaseDay
    {
        private readonly char[] _invalidChars = new[] { 'i', 'o', 'l' };

        private string _firstResult = string.Empty;

        public override ValueTask<string> Solve_1()
        {
            string input = File.ReadAllText(InputFilePath);
            _firstResult = FindNewPassword(input);

            return new(new string(_firstResult));
        }

        public override ValueTask<string> Solve_2()
        {
            var input = _firstResult.ToArray();
            IncrementChars(input);

            return new(FindNewPassword(input));
        }

        private string FindNewPassword(string oldPassword)
            => FindNewPassword(oldPassword.ToArray());

        private string FindNewPassword(char[] oldPassword)
        {
            char[] input = oldPassword.ToArray();

            while (!IsValid(input))
            {
                IncrementChars(input);
            }

            return new(new string(input));
        }

        private static void IncrementChars(char[] input)
        {
            for (int i = input.Length - 1; i >= 0; --i)
            {
                ++input[i];
                if (input[i] > 'z')
                {
                    input[i] = 'a';
                    continue;
                }

                break;
            }
        }

        private bool IsValid(char[] input)
            => !ContainsInvalidChars(input) &&
                CalculateMaxIncreasingSequenceCount(input) >= 3 &&
                CalculateUniquePairsCount(input) >= 2;

        private bool ContainsInvalidChars(char[] input)
            => input.Any(c => _invalidChars.Contains(c));

        private static int CalculateUniquePairsCount(char[] input)
        {
            List<char> pairs = new();
            bool foundPair = false;

            char previous = default;
            foreach (char c in input)
            {
                if (previous == c && !foundPair)
                {
                    pairs.Add(c);
                    foundPair = true;
                }
                else
                {
                    foundPair = false;
                }

                previous = c;
            }

            return pairs.Distinct().Count();
        }

        private static int CalculateMaxIncreasingSequenceCount(char[] input)
        {
            int count = 1;
            int max = 1;

            char previous = default;
            foreach (char c in input)
            {
                if (previous + 1 == c)
                {
                    count++;
                }
                else if (count > 1)
                {
                    max = Math.Max(max, count);
                    count = 1;
                }

                previous = c;
            }

            return Math.Max(max, count);
        }
    }
}
