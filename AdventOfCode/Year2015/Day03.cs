using AdventOfCode.Common;
using AoCHelper;

namespace AdventOfCode.Year2015
{
    public class Day03 : BaseDay
    {
        public override ValueTask<string> Solve_1()
        {
            string input = File.ReadAllText(InputFilePath);
            return new(CountTotalVisitedHouses(input).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            string input = File.ReadAllText(InputFilePath);
            return new(CountTotalVisitedHouses(input, 2).ToString());
        }

        private static int CountTotalVisitedHouses(string path, int numOfSantas = 1)
        {
            HashSet<Vector2Int> visited = new() { new Vector2Int(0, 0) };

            int santaIndex = 0;
            var currentPositions = new Vector2Int[numOfSantas];

            foreach (char c in path)
            {
                switch (c)
                {
                    case '<': currentPositions[santaIndex].X--; break;
                    case '>': currentPositions[santaIndex].X++; break;
                    case '^': currentPositions[santaIndex].Y++; break;
                    case 'v': currentPositions[santaIndex].Y--; break;
                    default: continue;
                }

                visited.Add(currentPositions[santaIndex]);
                santaIndex = (santaIndex + 1) % numOfSantas;
            }

            return visited.Count;
        }
    }
}
