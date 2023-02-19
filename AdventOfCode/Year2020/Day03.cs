using AdventOfCode.Common;
using AoCHelper;

namespace AdventOfCode.Year2020
{
    public class Day03 : BaseDay
    {
        private readonly int _mapWidth;
        private readonly int _mapHeight;
        private readonly bool[,] _treeMap;

        public Day03()
            => (_treeMap, _mapWidth, _mapHeight) = LoadTreeMap();

        public override ValueTask<string> Solve_1()
        {
            Vector2Int move = new(3, 1);
            int treesCount = CountTreesOnPath(move);
            return new(treesCount.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            Vector2Int[] moves =
            {
                new(1, 1),
                new(3, 1),
                new(5, 1),
                new(7, 1),
                new(1, 2),
            };

            long result = moves.Aggregate(
                (long)1,
                (multiplier, move) => multiplier * CountTreesOnPath(move));

            return new(result.ToString());
        }

        private bool IsTreeOnMap(Vector2Int position)
        {
            int x = position.X % _mapWidth;
            return _treeMap[position.Y, x];
        }

        private int CountTreesOnPath(Vector2Int move)
        {
            int treesCount = 0;
            for (Vector2Int pos = move; pos.Y < _mapHeight; pos += move)
            {
                if (IsTreeOnMap(pos))
                {
                    treesCount++;
                }
            }

            return treesCount;
        }

        private (bool[,] Map, int Width, int Height) LoadTreeMap()
        {
            string[] lines = File.ReadAllLines(InputFilePath);

            int mapWidth = lines.First().Length;
            int mapHeight = lines.Length;

            bool[,] treeMap = new bool[mapHeight, mapWidth];
            for (int y = 0; y < mapHeight; ++y)
            {
                for (int x = 0; x < mapWidth; ++x)
                {
                    if (lines[y][x] == '#')
                    {
                        treeMap[y, x] = true;
                    }
                }
            }

            return (treeMap, mapWidth, mapHeight);
        }
    }
}
