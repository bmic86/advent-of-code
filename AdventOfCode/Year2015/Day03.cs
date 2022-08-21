using AoCHelper;

namespace AdventOfCode.Year2015
{
    public record GridCoords(int X, int Y);

    public class Day03 : BaseDay
    {
        public override ValueTask<string> Solve_1()
        {
            string input = File.ReadAllText(InputFilePath);

            HashSet<GridCoords> visited = new() { new GridCoords(0, 0) };

            int currentX = 0;
            int currentY = 0;
            foreach (char c in input)
            {
                switch (c)
                {
                    case '<': currentX--; break;
                    case '>': currentX++; break;
                    case '^': currentY++; break;
                    case 'v': currentY--; break;
                    default: continue;
                }

                visited.Add(new GridCoords(currentX, currentY));
            }

            return new(visited.Count.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            throw new NotImplementedException();
        }
    }
}
