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

        private readonly IReadOnlyDictionary<WorldDirection, Vector2Int> WorldDirectionCoords =
            new Dictionary<WorldDirection, Vector2Int>()
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
            Vector2Int currentPosition = new(0, 0);
            var facingDirection = WorldDirection.North;

            foreach (var move in _moves)
            {
                facingDirection = GetNextFacingDirection(facingDirection, move.Direction);

                currentPosition = GetNextPosition(
                    currentPosition,
                    WorldDirectionCoords[facingDirection],
                    move.Steps);
            }

            int result = currentPosition.GetManhattanDistanceFromOrigin();
            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            HashSet<Vector2Int> visited = new();

            Vector2Int currentPosition = new(0, 0);
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
                        int result = currentPosition.GetManhattanDistanceFromOrigin();
                        return new(result.ToString());
                    }

                    visited.Add(currentPosition);
                }
            }

            return new("No result.");
        }

        private static Vector2Int GetNextPosition(
            Vector2Int currentPosition,
            Vector2Int directionCoords,
            int steps = 1)
        {
            directionCoords *= steps;
            return currentPosition + directionCoords;
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
