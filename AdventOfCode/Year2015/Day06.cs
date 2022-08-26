using AoCHelper;

namespace AdventOfCode.Year2015
{
    public class Day06 : BaseDay
    {
        private bool[,] _lights = new bool[1000, 1000];
        private int[,] _lightsWithBrightness = new int[1000, 1000];

        public override ValueTask<string> Solve_1()
        {
            string[] inputLines = File.ReadAllLines(InputFilePath);

            foreach (string line in inputLines)
            {
                if (line.StartsWith("turn on"))
                {
                    var splited = line.Split(new char[] { ' ', ',' });
                    TurnOn(int.Parse(splited[2]), int.Parse(splited[3]), int.Parse(splited[5]), int.Parse(splited[6]));
                }
                else if (line.StartsWith("turn off"))
                {
                    var splited = line.Split(new char[] { ' ', ',' });
                    TurnOff(int.Parse(splited[2]), int.Parse(splited[3]), int.Parse(splited[5]), int.Parse(splited[6]));
                }
                else if (line.StartsWith("toggle"))
                {
                    var splited = line.Split(new char[] { ' ', ',' });
                    Toggle(int.Parse(splited[1]), int.Parse(splited[2]), int.Parse(splited[4]), int.Parse(splited[5]));
                }
            }

            return new(_lights.Cast<bool>().Count(l => l).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            string[] inputLines = File.ReadAllLines(InputFilePath);

            foreach (string line in inputLines)
            {
                if (line.StartsWith("turn on"))
                {
                    var splited = line.Split(new char[] { ' ', ',' });
                    TurnOnWithBrightness(int.Parse(splited[2]), int.Parse(splited[3]), int.Parse(splited[5]), int.Parse(splited[6]));
                }
                else if (line.StartsWith("turn off"))
                {
                    var splited = line.Split(new char[] { ' ', ',' });
                    TurnOffWithBrightness(int.Parse(splited[2]), int.Parse(splited[3]), int.Parse(splited[5]), int.Parse(splited[6]));
                }
                else if (line.StartsWith("toggle"))
                {
                    var splited = line.Split(new char[] { ' ', ',' });
                    ToggleWithBrightness(int.Parse(splited[1]), int.Parse(splited[2]), int.Parse(splited[4]), int.Parse(splited[5]));
                }
            }

            return new(_lightsWithBrightness.Cast<int>().Sum(l => l).ToString());
        }

        private void Toggle(int startX, int startY, int endX, int endY)
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    _lights[x, y] = !_lights[x, y];
                }
            }
        }

        private void TurnOff(int startX, int startY, int endX, int endY)
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    _lights[x, y] = false;
                }
            }
        }

        private void TurnOn(int startX, int startY, int endX, int endY)
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    _lights[x, y] = true;
                }
            }
        }

        private void ToggleWithBrightness(int startX, int startY, int endX, int endY)
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    _lightsWithBrightness[x, y] += 2;
                }
            }
        }

        private void TurnOffWithBrightness(int startX, int startY, int endX, int endY)
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    if (_lightsWithBrightness[x, y] > 0)
                    {
                        _lightsWithBrightness[x, y]--;
                    }
                }
            }
        }

        private void TurnOnWithBrightness(int startX, int startY, int endX, int endY)
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    _lightsWithBrightness[x, y]++;
                }
            }
        }
    }
}
