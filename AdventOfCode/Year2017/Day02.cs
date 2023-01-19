using AoCHelper;

namespace AdventOfCode.Year2017
{
    public class Day02 : BaseDay
    {
        private readonly int[][] _spreadsheet;

        public Day02() => _spreadsheet = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int checksum = _spreadsheet
                .Select(CalculateRowSimpleChecksum)
                .Sum();

            return new(checksum.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int checksum = _spreadsheet
                .Select(CalculateRowComplexChecksum)
                .Sum();

            return new(checksum.ToString());
        }

        private int CalculateRowSimpleChecksum(int[] row)
        {
            int min = int.MaxValue;
            int max = int.MinValue;

            foreach (int value in row)
            {
                min = Math.Min(min, value);
                max = Math.Max(max, value);
            }

            return max - min;
        }

        private int CalculateRowComplexChecksum(int[] row)
        {
            for (int i = 0; i < row.Length - 1; ++i)
            {
                for (int j = i + 1; j < row.Length; ++j)
                {
                    int min = Math.Min(row[i], row[j]);
                    int max = Math.Max(row[i], row[j]);
                    if (max % min == 0)
                    {
                        return max / min;
                    }
                }
            }

            return 0;
        }

        private int[][] LoadData()
            => File.ReadLines(InputFilePath)
                .Select(line => line.Split().Select(int.Parse).ToArray())
                .ToArray();
    }
}
