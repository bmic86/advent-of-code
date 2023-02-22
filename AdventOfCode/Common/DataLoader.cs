namespace AdventOfCode.Common
{
    public static class DataLoader
    {
        public static (bool[,] Table, int Width, int Height)
            LoadBoolTable2D(string inputFilePath, char trueValue = '1')
        {
            string[] lines = File.ReadAllLines(inputFilePath);

            int width = lines.First().Length;
            int height = lines.Length;

            bool[,] table = new bool[height, width];
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    if (lines[y][x] == trueValue)
                    {
                        table[y, x] = true;
                    }
                }
            }

            return (table, width, height);
        }
    }
}
