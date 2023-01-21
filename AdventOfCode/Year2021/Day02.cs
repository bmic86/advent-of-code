using AdventOfCode.Common;
using AoCHelper;

namespace AdventOfCode.Year2021
{
    public class Day02 : BaseDay
    {
        private record Move(string Direction, int Length);

        private readonly IReadOnlyList<Move> _moves;

        public Day02()
            => _moves = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int position = 0;
            int depth = 0;

            foreach (var move in _moves)
            {
                switch (move.Direction)
                {
                    case "forward":
                        position += move.Length;
                        break;
                    case "up":
                        depth -= move.Length;
                        break;
                    case "down":
                        depth += move.Length;
                        break;
                };
            }

            int result = position * depth;
            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int position = 0;
            int depth = 0;
            int aim = 0;

            foreach (var move in _moves)
            {
                switch (move.Direction)
                {
                    case "forward":
                        position += move.Length;
                        depth += aim * move.Length;
                        break;
                    case "up":
                        aim -= move.Length;
                        break;
                    case "down":
                        aim += move.Length;
                        break;
                };
            }

            int result = position * depth;
            return new(result.ToString());
        }

        private IReadOnlyList<Move> LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(line =>
                {
                    string[] splitted = line.Split(' ');
                    return new Move(splitted[0], int.Parse(splitted[1]));
                })
                .ToList();
    }
}
