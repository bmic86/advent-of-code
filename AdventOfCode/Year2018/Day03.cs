using AoCHelper;
using System.Security.Claims;

namespace AdventOfCode.Year2018
{
    public class Day03 : BaseDay
    {
        private record Claim(int Id, int X, int Y, int Width, int Height);

        private readonly char[] _inputIgnoreChars = new[]
        {
            '#', ' ', '@', ',', ':', 'x'
        };

        private readonly IReadOnlyList<Claim> _claims;

        private const int FabricLength = 1000;

        private readonly int[,] _fabric = new int[FabricLength, FabricLength];

        public Day03() => _claims = LoadData();

        public override ValueTask<string> Solve_1()
        {
            ClaimFabric();
            int fabricSquares = CountFabricSquaresWithMultipleClaims();
            return new(fabricSquares.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            foreach (var claim in _claims)
            {
                int totalClaimedArea = GetTotalFabricClaimedArea(
                    claim.X,
                    claim.Y,
                    claim.Width,
                    claim.Height);

                if (totalClaimedArea == claim.Width * claim.Height)
                {
                    return new(claim.Id.ToString());
                }
            }

            return new("No result found.");
        }

        private int GetTotalFabricClaimedArea(
            int startX,
            int startY,
            int width,
            int height)
        {
            int claimedArea = 0;
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    int x = startX + i;
                    int y = startY + j;
                    claimedArea += _fabric[y, x];
                }
            }

            return claimedArea;
        }

        private void ClaimFabric()
        {
            foreach (var claim in _claims)
            {
                for (int i = 0; i < claim.Width; ++i)
                {
                    for (int j = 0; j < claim.Height; ++j)
                    {
                        int x = claim.X + i;
                        int y = claim.Y + j;
                        _fabric[y, x]++;
                    }
                }
            }
        }

        private int CountFabricSquaresWithMultipleClaims()
        {
            int sum = 0;
            for (int x = 0; x < FabricLength; ++x)
            {
                for (int y = 0; y < FabricLength; ++y)
                {
                    if (_fabric[y, x] > 1)
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }

        private List<Claim> LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(line =>
                {
                    string[] splitted = line.Split(
                        _inputIgnoreChars,
                        StringSplitOptions.RemoveEmptyEntries);

                    return new Claim(
                        int.Parse(splitted[0]),
                        int.Parse(splitted[1]),
                        int.Parse(splitted[2]),
                        int.Parse(splitted[3]),
                        int.Parse(splitted[4]));
                })
                .ToList();
    }
}
