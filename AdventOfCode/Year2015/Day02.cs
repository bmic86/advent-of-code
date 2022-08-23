using AoCHelper;

namespace AdventOfCode.Year2015
{
    public readonly record struct Box(int Length, int Width, int Height)
    {
        public static Box Parse(string dimensions)
        {
            string[] splited = dimensions.Split('x', StringSplitOptions.RemoveEmptyEntries);
            return new Box(int.Parse(splited[0]), int.Parse(splited[1]), int.Parse(splited[2]));
        }
    }

    public class Day02 : BaseDay
    {
        public override ValueTask<string> Solve_1()
        {
            string[] inputLines = File.ReadAllLines(InputFilePath);

            int result = inputLines
                .Select(line => CalculateRequiredPaperArea(Box.Parse(line)))
                .Sum();

            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            string[] inputLines = File.ReadAllLines(InputFilePath);

            int result = inputLines
                .Select(line => CalculateRequiredRibbonLength(Box.Parse(line)))
                .Sum();

            return new(result.ToString());
        }

        private static int CalculateRequiredPaperArea(Box box)
        {
            int a = box.Length * box.Width;
            int b = box.Width * box.Height;
            int c = box.Height * box.Length;
            int minBoxSideArea = Math.Min(Math.Min(a, b), c);

            return 2 * a + 2 * b + 2 * c + minBoxSideArea;
        }

        private static int CalculateRequiredRibbonLength(Box box)
        {
            int largestSideWrapLength = 2 * Math.Max(Math.Max(box.Length, box.Width), box.Height);
            int wrapLength = (2 * box.Length + 2 * box.Width + 2 * box.Height) - largestSideWrapLength;
            int bowLength = box.Length * box.Width * box.Height;

            return wrapLength + bowLength;
        }
    }
}
