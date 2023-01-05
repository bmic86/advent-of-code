using AoCHelper;

namespace AdventOfCode.Year2015
{
    public class Day18 : BaseDay
    {
        private record NeighborsArea(int MinX, int MaxX, int MinY, int MaxY);
        private record LightStateInput(int NeighborsOnCount, bool CurrentLight, int X, int Y);

        private const int Steps = 100;
        private const int LightsGridSize = 100;
        private const int FirstIndex = 0;
        private const int LastIndex = LightsGridSize - 1;

        private bool[,] _currentLights = new bool[LightsGridSize, LightsGridSize];
        private bool[,] _nextLights = new bool[LightsGridSize, LightsGridSize];

        public override ValueTask<string> Solve_1()
        {
            LoadData();

            bool getNextLightState(LightStateInput input)
                => (input.NeighborsOnCount, input.CurrentLight) switch
                {
                    (2, true) => true,
                    (3, _) => true,
                    _ => false
                };

            UpdateLights(getNextLightState);
            return new(GetLightsOnCount().ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            LoadData();
            TurnOnLightGridCorners();

            bool getNextLightState(LightStateInput input)
                => (input) switch
                {
                    (_, _, FirstIndex, FirstIndex) => true,
                    (_, _, FirstIndex, LastIndex) => true,
                    (_, _, LastIndex, FirstIndex) => true,
                    (_, _, LastIndex, LastIndex) => true,
                    (2, true, _, _) => true,
                    (3, _, _, _) => true,
                    _ => false
                };

            UpdateLights(getNextLightState);
            return new(GetLightsOnCount().ToString());
        }

        private void UpdateLights(Func<LightStateInput, bool> getNextLightState)
        {
            for (int step = 0; step < Steps; step++)
            {
                for (int y = FirstIndex; y <= LastIndex; y++)
                {
                    for (int x = FirstIndex; x <= LastIndex; x++)
                    {
                        NeighborsArea neighborsArea = GetNeighborsArea(x, y);
                        int neighborsOnCount = GetNeighborLightsOnCount(x, y, neighborsArea);

                        LightStateInput input = new(neighborsOnCount, _currentLights[y, x], x, y);
                        _nextLights[y, x] = getNextLightState(input);
                    }
                }

                (_currentLights, _nextLights) = (_nextLights, _currentLights);
            }
        }

        private NeighborsArea GetNeighborsArea(int x, int y)
            => new(
                MinX: Math.Max(x - 1, FirstIndex),
                MaxX: Math.Min(x + 1, LastIndex),
                MinY: Math.Max(y - 1, FirstIndex),
                MaxY: Math.Min(y + 1, LastIndex));

        private int GetNeighborLightsOnCount(
            int currentLightX,
            int currentLightY,
            NeighborsArea neighborsArea)
        {
            int neighborLightsOnCount = 0;
            for (int y = neighborsArea.MinY; y <= neighborsArea.MaxY; y++)
            {
                for (int x = neighborsArea.MinX; x <= neighborsArea.MaxX; x++)
                {
                    bool isNeighbor = (x != currentLightX || y != currentLightY);
                    if (isNeighbor && _currentLights[y, x])
                    {
                        neighborLightsOnCount++;
                    }
                }
            }

            return neighborLightsOnCount;
        }

        private int GetLightsOnCount()
        {
            int lightsOnCount = 0;
            for (int y = FirstIndex; y <= LastIndex; y++)
            {
                for (int x = FirstIndex; x <= LastIndex; x++)
                {
                    if (_currentLights[y, x])
                    {
                        lightsOnCount++;
                    }
                }
            }

            return lightsOnCount;
        }

        private void TurnOnLightGridCorners()
        {
            _currentLights[FirstIndex, FirstIndex] = true;
            _currentLights[FirstIndex, LastIndex] = true;
            _currentLights[LastIndex, FirstIndex] = true;
            _currentLights[LastIndex, LastIndex] = true;
        }

        private void LoadData()
        {
            string[] lines = File.ReadAllLines(InputFilePath);
            for (int y = FirstIndex; y <= LastIndex; y++)
            {
                for (int x = FirstIndex; x <= LastIndex; x++)
                {
                    _currentLights[y, x] = lines[y][x] == '#';
                }
            }
        }
    }
}
