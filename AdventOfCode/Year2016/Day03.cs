using AoCHelper;

namespace AdventOfCode.Year2016
{
    public class Day03 : BaseDay
    {
        private readonly int[] _edgeLengths;

        public Day03() => _edgeLengths = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int validTrianglesCount = 0;
            for (int i = 0; i < _edgeLengths.Length; i += 3)
            {
                bool isValid = IsValidTriangle(
                    _edgeLengths[i],
                    _edgeLengths[i + 1],
                    _edgeLengths[i + 2]);

                if (isValid)
                {
                    validTrianglesCount++;
                }
            }

            return new(validTrianglesCount.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int validTrianglesCount = 0;
            for (int column = 0; column < 3; ++column)
            {
                for (int i = column; i < _edgeLengths.Length; i += 9)
                {
                    bool isValid = IsValidTriangle(
                        _edgeLengths[i],
                        _edgeLengths[i + 3],
                        _edgeLengths[i + 6]);

                    if (isValid)
                    {
                        validTrianglesCount++;
                    }
                }
            }

            return new(validTrianglesCount.ToString());
        }

        private static bool IsValidTriangle(int a, int b, int c)
            => (a + b > c) && (a + c > b) && (b + c > a);

        private int[] LoadData()
            => File.ReadAllText(InputFilePath)
                .Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
    }
}
