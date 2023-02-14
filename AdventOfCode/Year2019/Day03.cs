using AdventOfCode.Common;
using AoCHelper;

namespace AdventOfCode.Year2019
{
    public class Day03 : BaseDay
    {
        private record WirePath(char Direction, int Steps);

        private readonly IReadOnlyDictionary<Vector2Int, int> _intersections;

        public Day03()
        {
            WirePath[][] wiresPaths = LoadData();
            Dictionary<Vector2Int, int> wireMap = CreateWireMap(wiresPaths[0]);
            _intersections = GetIntesectionsMap(wireMap, wiresPaths[1]);
        }

        public override ValueTask<string> Solve_1()
        {
            int result = _intersections.Keys
                .Min(x => x.GetManhattanDistanceFromOrigin());

            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int result = _intersections.Values.Min();
            return new(result.ToString());
        }

        private static Dictionary<Vector2Int, int> GetIntesectionsMap(
           IReadOnlyDictionary<Vector2Int, int> firstWireMap,
           WirePath[] secondWire)
        {
            Dictionary<Vector2Int, int> intersectionsMap = new();
            Vector2Int position = new(0, 0);
            int secondWireSteps = 1;

            foreach (WirePath wirePath in secondWire)
            {
                Vector2Int dirVector = GetDirectionVector(wirePath.Direction);
                for (int i = 0; i < wirePath.Steps; i++)
                {
                    position += dirVector;

                    if (firstWireMap.TryGetValue(position, out int firstWireSteps) &&
                        !intersectionsMap.ContainsKey(position))
                    {
                        int steps = firstWireSteps + secondWireSteps;
                        intersectionsMap.Add(position, steps);
                    }

                    secondWireSteps++;
                }
            }

            return intersectionsMap;
        }

        private static Dictionary<Vector2Int, int> CreateWireMap(WirePath[] wirePaths)
        {
            Dictionary<Vector2Int, int> wiredPositions = new();
            Vector2Int position = new(0, 0);
            int stepCount = 1;

            foreach (WirePath wirePath in wirePaths)
            {
                Vector2Int dirVector = GetDirectionVector(wirePath.Direction);

                for (int i = 1; i <= wirePath.Steps; i++)
                {
                    position += dirVector;
                    wiredPositions.TryAdd(position, stepCount);
                    stepCount++;
                }
            }

            return wiredPositions;
        }

        private static Vector2Int GetDirectionVector(char direction)
            => direction switch
            {
                'L' => new Vector2Int(-1, 0),
                'R' => new Vector2Int(1, 0),
                'U' => new Vector2Int(0, 1),
                'D' => new Vector2Int(0, -1),

                _ => throw new ArgumentException(
                        $"Invalid direction: {direction}",
                        nameof(direction)),
            };

        private WirePath[][] LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(line => line.Split(',')
                    .Select(ParseWirePath)
                    .ToArray())
                .ToArray();

        private WirePath ParseWirePath(string value)
            => new(value[0], int.Parse(value[1..]));
    }
}
