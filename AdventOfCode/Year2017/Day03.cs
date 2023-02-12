using AdventOfCode.Common;
using AoCHelper;

namespace AdventOfCode.Year2017
{
    public class Day03 : BaseDay
    {
        private readonly int _inputNumber;

        public Day03()
            => _inputNumber = int.Parse(File.ReadAllText(InputFilePath));

        public override ValueTask<string> Solve_1()
        {
            var (blockSize, x, remainingSteps) =
                GoToRightmostGridBlock(_inputNumber);

            Vector2Int gridCoords = new(x, 0);

            int halfSize = blockSize / 2;
            int blockEdgeSteps = blockSize - 1;

            GoUp(halfSize, ref remainingSteps, ref gridCoords);
            GoLeft(blockEdgeSteps, ref remainingSteps, ref gridCoords);
            GoDown(blockEdgeSteps, ref remainingSteps, ref gridCoords);
            GoRight(blockEdgeSteps, ref remainingSteps, ref gridCoords);
            GoUp(halfSize, ref remainingSteps, ref gridCoords);

            int result = gridCoords.GetManhattanDistanceFromOrigin();
            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int limit = 0;
            Vector2Int currentPoint = new(0, 0);
            Vector2Int currentDirection = new(0, 0);

            Dictionary<Vector2Int, int> map = new()
            {
                [new(0, 0)] = 1
            };

            int lastSum = 0;
            while (lastSum <= _inputNumber)
            {
                if (currentPoint == new Vector2Int(limit, -limit))
                {
                    currentDirection = new(1, 0);
                    limit++;
                }
                else if (currentPoint == new Vector2Int(-limit, -limit))
                {
                    currentDirection = new(1, 0);
                }
                else if (currentPoint == new Vector2Int(limit, limit))
                {
                    currentDirection = new(-1, 0);
                }
                else if (currentPoint.X == limit)
                {
                    currentDirection = new(0, 1);
                }
                else if (currentPoint == new Vector2Int(-limit, limit))
                {
                    currentDirection = new(0, -1);
                }

                currentPoint += currentDirection;
                lastSum = SumRegion(currentPoint, map);
                map[currentPoint] = lastSum;
            }

            return new(lastSum.ToString());
        }

        private static int SumRegion(
            Vector2Int point,
            IReadOnlyDictionary<Vector2Int, int> map)
        {
            int sum = 0;
            for (int x = point.X - 1; x <= point.X + 1; ++x)
            {
                for (int y = point.Y - 1; y <= point.Y + 1; ++y)
                {
                    sum += map.GetValueOrDefault(new Vector2Int(x, y));
                }
            }

            return sum;
        }

        private static void GoUp(
            int maxSteps,
            ref int remainingSteps,
            ref Vector2Int gridCoords)
        {
            int stepsToBurn = Math.Min(maxSteps, remainingSteps);
            gridCoords.Y += stepsToBurn;
            remainingSteps -= stepsToBurn;
        }

        private static void GoDown(
            int maxSteps,
            ref int remainingSteps,
            ref Vector2Int gridCoords)
        {
            int stepsToBurn = Math.Min(maxSteps, remainingSteps);
            gridCoords.Y -= stepsToBurn;
            remainingSteps -= stepsToBurn;
        }

        private static void GoLeft(
            int maxSteps,
            ref int remainingSteps,
            ref Vector2Int gridCoords)
        {
            int stepsToBurn = Math.Min(maxSteps, remainingSteps);
            gridCoords.X -= stepsToBurn;
            remainingSteps -= stepsToBurn;
        }

        private static void GoRight(
            int maxSteps,
            ref int remainingSteps,
            ref Vector2Int gridCoords)
        {
            int stepsToBurn = Math.Min(maxSteps, remainingSteps);
            gridCoords.X += stepsToBurn;
            remainingSteps -= stepsToBurn;
        }

        private static (int BlockSize, int GridXPosition, int RemainingSteps)
            GoToRightmostGridBlock(int remainingSteps)
        {
            int currentBlockSize = 1;
            int x = 0;

            remainingSteps -= 2;
            while (true)
            {
                currentBlockSize += 2;
                x++;

                int offset = currentBlockSize * 2 + (currentBlockSize - 1) * 2 - 1;
                int value = remainingSteps - offset;
                if (value < 0)
                {
                    break;
                }
                remainingSteps = value;
            }

            return (currentBlockSize, x, remainingSteps);
        }
    }
}
