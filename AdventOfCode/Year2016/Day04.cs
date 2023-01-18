using AoCHelper;
using System.Text;

namespace AdventOfCode.Year2016
{
    public class Day04 : BaseDay
    {
        private record Room(string Name, int SectorId, string Checksum);

        private readonly Room[] _rooms;

        public Day04() => _rooms = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int result = 0;

            foreach (Room room in _rooms)
            {
                var expectedChecksum = room.Name
                    .Where(char.IsAsciiLetterLower)
                    .GroupBy(c => c)
                    .OrderByDescending(g => g.Count())
                    .ThenBy(g => g.Key)
                    .Take(5)
                    .Select(g => g.Key);

                if (expectedChecksum.SequenceEqual(room.Checksum))
                {
                    result += room.SectorId;
                }
            }

            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            foreach (Room room in _rooms)
            {
                StringBuilder stringBuilder = new(room.Name.Length);

                foreach (char c in room.Name)
                {
                    char decrypted = DecryptChar(c, room.SectorId);
                    stringBuilder.Append(decrypted);
                }

                string decriptedName = stringBuilder.ToString();
                if (decriptedName.Contains("northpole"))
                {
                    return new(room.SectorId.ToString());
                }
            }

            return new("No result found.");
        }

        private static char DecryptChar(char toDecrypt, int sectorId)
        {
            if (toDecrypt == '-')
            {
                return ' ';
            }

            int offset = (toDecrypt - 'a' + sectorId) % ('z' - 'a' + 1);
            return (char)(offset + 'a');
        }

        private Room[] LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(line =>
                {
                    int nameEnd = line.LastIndexOf('-');
                    int sectorIdEnd = line.LastIndexOf('[');
                    int checksumEnd = line.LastIndexOf(']');

                    return new Room(
                        line[..nameEnd],
                        int.Parse(line[(nameEnd + 1)..sectorIdEnd]),
                        line[(sectorIdEnd + 1)..checksumEnd]);
                })
                .ToArray();
    }
}
