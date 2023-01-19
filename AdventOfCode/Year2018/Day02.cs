using AoCHelper;

namespace AdventOfCode.Year2018
{
    public class Day02 : BaseDay
    {
        private record struct StringDiffs(int Count, int LastIndex);

        private readonly string[] _input;

        public Day02()
            => _input = File.ReadAllLines(InputFilePath);

        public override ValueTask<string> Solve_1()
        {
            var sums = _input.Aggregate((Doubles: 0, Triples: 0), (sums, line) =>
            {
                var charCounts = line
                    .GroupBy(c => c)
                    .Select(g => g.Count());

                if (charCounts.Any(count => count == 2))
                {
                    sums.Doubles++;
                }

                if (charCounts.Any(count => count == 3))
                {
                    sums.Triples++;
                }

                return (sums.Doubles, sums.Triples);
            });

            int checksum = sums.Doubles * sums.Triples;
            return new(checksum.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            for (int i = 0; i < _input.Length - 1; ++i)
            {
                for (int j = i + 1; j < _input.Length; ++j)
                {
                    var diffs = GetStringDiffs(_input[i], _input[j]);
                    if (diffs.Count == 1)
                    {
                        string result = _input[j].Remove(diffs.LastIndex, 1);
                        return new(result);
                    }
                }
            }

            return new("No result.");
        }

        private static StringDiffs GetStringDiffs(string first, string second)
        {
            int diffsCount = 0;
            int lastDiffIndex = -1;

            for (int i = 0; i < first.Length; ++i)
            {
                if (first[i] != second[i])
                {
                    lastDiffIndex = i;
                    diffsCount++;
                }
            }

            return new(diffsCount, lastDiffIndex);
        }
    }
}
