using AoCHelper;
using Spectre.Console.Rendering;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day14 : BaseDay
    {
        private record Reindeer(string Name, int Speed, int FlyTime, int RestTime);

        private const int RaceTime = 2503;

        private readonly List<Reindeer> _reindeers;

        public Day14() => _reindeers = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int maxDistance = _reindeers
                .Select(r => CalculateDistance(r, RaceTime))
                .Max();

            return new(maxDistance.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            throw new NotImplementedException();
        }

        private List<Reindeer> LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(line =>
                {
                    string[] splited = line.Split(' ');
                    return new Reindeer(
                        splited[0],
                        int.Parse(splited[3]),
                        int.Parse(splited[6]),
                        int.Parse(splited[13]));
                }).ToList();

        private static int CalculateDistance(Reindeer reindeer, int raceTime)
        {
            int moveLenght = reindeer.FlyTime + reindeer.RestTime;
            int moves = raceTime / moveLenght;

            int fullMovesTime = moves * moveLenght;
            int partialMoveTime = raceTime - fullMovesTime;

            int effectiveFlyTime = moves * reindeer.FlyTime + Math.Min(partialMoveTime, reindeer.FlyTime);
            return effectiveFlyTime * reindeer.Speed;
        }
    }
}
