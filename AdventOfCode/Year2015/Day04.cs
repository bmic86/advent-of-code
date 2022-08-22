using AoCHelper;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Year2015
{
    public class Day04 : BaseDay
    {
        public override ValueTask<string> Solve_1()
        {
            string input = File.ReadAllText(InputFilePath);

            int result = MineAdventCoin(input, 5);
            return new(FormatResult(result));
        }

        public override ValueTask<string> Solve_2()
        {
            string input = File.ReadAllText(InputFilePath);

            int result = MineAdventCoin(input, 6);
            return new(FormatResult(result));
        }

        private static int MineAdventCoin(string input, int leadingHexZeros)
        {
            int mod = leadingHexZeros % 2;
            int half = leadingHexZeros / 2;

            for (int i = 1; i < int.MaxValue; i++)
            {
                byte[] rawData = Encoding.ASCII.GetBytes($"{input}{i}");
                byte[] hash = MD5.HashData(rawData);

                if (mod == 1)
                {
                    hash[half] &= 0xF0;
                }

                if (hash.Take(half + mod).Sum(x => x) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        private static string FormatResult(int result)
            => result > 0 ? result.ToString() : "No result found.";
    }
}
