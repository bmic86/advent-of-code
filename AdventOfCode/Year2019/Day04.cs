using AoCHelper;

namespace AdventOfCode.Year2019
{
    public class Day04 : BaseDay
    {
        private readonly Range _range;

        public Day04() => _range = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int result = GetPossiblePasswordsCount(digitsCount => digitsCount >= 2);
            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int result = GetPossiblePasswordsCount(digitsCount => digitsCount == 2);
            return new(result.ToString());
        }

        private int GetPossiblePasswordsCount(Predicate<int> anyAdjacentDigitsCount)
        {
            int start = _range.Start.Value;
            int end = _range.End.Value;

            int sum = 0;
            for (int password = start; password <= end; ++password)
            {
                if (IsValidPassword(password, anyAdjacentDigitsCount))
                {
                    sum++;
                }
            }
            return sum;
        }

        private static bool IsValidPassword(
            int password,
            Predicate<int> anyAdjacentDigitsCount)
        {
            int lastDigit = int.MaxValue;
            Span<byte> digitCounts = stackalloc byte[10];

            for (int i = 0; i < 6; ++i)
            {
                int digit = password % 10;

                bool isLessSignificantDigitDecrease = digit > lastDigit;
                if (isLessSignificantDigitDecrease)
                {
                    return false;
                }

                digitCounts[digit]++;
                password /= 10;
                lastDigit = digit;
            }

            return CheckDigitsCounts(digitCounts, anyAdjacentDigitsCount);
        }

        private static bool CheckDigitsCounts(
            Span<byte> digitCounts,
            Predicate<int> anyAdjacentDigitsCount)
        {
            foreach (int digitCount in digitCounts)
            {
                if (anyAdjacentDigitsCount(digitCount))
                {
                    return true;
                }
            }
            return false;
        }

        private Range LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(line =>
                {
                    string[] splitted = line.Split('-');
                    return new Range(
                        int.Parse(splitted[0]),
                        int.Parse(splitted[1]));
                })
                .First();
    }
}
