using AdventOfCode.Common;
using AoCHelper;

namespace AdventOfCode.Year2016
{
    public class Day01 : BaseDay
    {
        private enum MoveDirection { L = -1, R = 1, }
        private record Move(MoveDirection Direction, int Steps);

        private enum WorldDirection
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3,
        }

        private readonly IReadOnlyDictionary<WorldDirection, GridCoords> WorldDirectionCoords =
            new Dictionary<WorldDirection, GridCoords>()
            {
                { WorldDirection.North, new(0, -1) },
                { WorldDirection.East, new(1, 0) },
                { WorldDirection.South, new(0, 1) },
                { WorldDirection.West, new(-1, 0) },
            };

        private readonly IReadOnlyList<Move> _moves;

        public Day01() => _moves = LoadData();

        public override ValueTask<string> Solve_1()
        {
            GridCoords currentPosition = new(0, 0);
            var facingDirection = WorldDirection.North;

            foreach (var move in _moves)
            {
                facingDirection = GetNextFacingDirection(facingDirection, move.Direction);

                currentPosition = GetNextPosition(
                    currentPosition,
                    WorldDirectionCoords[facingDirection],
                    move.Steps);
            }

            return new(DistanceFromStartPosition(currentPosition).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            HashSet<GridCoords> visited = new();

            GridCoords currentPosition = new(0, 0);
            var facingDirection = WorldDirection.North;

            foreach (var move in _moves)
            {
                facingDirection = GetNextFacingDirection(facingDirection, move.Direction);

                for (int step = 1; step <= move.Steps; step++)
                {
                    currentPosition = GetNextPosition(
                        currentPosition,
                        WorldDirectionCoords[facingDirection]);

                    if (visited.Contains(currentPosition))
                    {
                        return new(DistanceFromStartPosition(currentPosition).ToString());
                    }

                    visited.Add(currentPosition);
                }
            }

            return new("No result.");
        }

        private static GridCoords GetNextPosition(
            GridCoords currentPosition,
            GridCoords directionCoords,
            int steps = 1)
        {
            directionCoords = new(
                directionCoords.X * steps,
                directionCoords.Y * steps);

            return new(
                currentPosition.X + directionCoords.X,
                currentPosition.Y + directionCoords.Y);
        }

        private static WorldDirection GetNextFacingDirection(
            WorldDirection facingDirection,
            MoveDirection moveDirection)
        {
            int value = (int)facingDirection + (int)moveDirection;
            return value switch
            {
                -1 => WorldDirection.West,
                4 => WorldDirection.North,
                _ => (WorldDirection)value
            };
        }

        private static int DistanceFromStartPosition(GridCoords currentPosition)
            => Math.Abs(currentPosition.X) + Math.Abs(currentPosition.Y);

        private List<Move> LoadData()
            => File.ReadAllText(InputFilePath)
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(str =>
                {
                    var direction = Enum.Parse<MoveDirection>(str[0..1]);
                    int steps = int.Parse(str[1..]);
                    return new Move(direction, steps);
                })
                .ToList();
    }
}
