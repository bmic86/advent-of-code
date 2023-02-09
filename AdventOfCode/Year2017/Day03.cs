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

            GridCoords gridCoords = new(x, 0);

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
            return new("");
        }

        private static void GoUp(
            int maxSteps,
            ref int remainingSteps,
            ref GridCoords gridCoords)
        {
            int stepsToBurn = Math.Min(maxSteps, remainingSteps);
            gridCoords.Y += stepsToBurn;
            remainingSteps -= stepsToBurn;
        }

        private static void GoDown(
            int maxSteps,
            ref int remainingSteps,
            ref GridCoords gridCoords)
        {
            int stepsToBurn = Math.Min(maxSteps, remainingSteps);
            gridCoords.Y -= stepsToBurn;
            remainingSteps -= stepsToBurn;
        }

        private static void GoLeft(
            int maxSteps,
            ref int remainingSteps,
            ref GridCoords gridCoords)
        {
            int stepsToBurn = Math.Min(maxSteps, remainingSteps);
            gridCoords.X -= stepsToBurn;
            remainingSteps -= stepsToBurn;
        }

        private static void GoRight(
            int maxSteps,
            ref int remainingSteps,
            ref GridCoords gridCoords)
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
