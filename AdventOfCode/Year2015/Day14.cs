using AoCHelper;
using Spectre.Console.Rendering;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day14 : BaseDay
    {
        private record Reindeer(string Name, int Speed, int FlyTime, int RestTime);

        private class Score
        {
            public int Distance { get; set; }
            public int Points { get; set; }
        }

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
            Dictionary<string, Score> scoreBoard = _reindeers
                .ToDictionary(r => r.Name, r => new Score());

            for (int i = 0; i < RaceTime; ++i)
            {
                int maxTotalDistance = int.MinValue;
                foreach (var rendieer in _reindeers)
                {
                    int time = rendieer.FlyTime + rendieer.RestTime;
                    bool isMoving = (i % time) < rendieer.FlyTime;
                    if (isMoving)
                    {
                        scoreBoard[rendieer.Name].Distance += rendieer.Speed;
                    }

                    maxTotalDistance = Math.Max(
                        scoreBoard[rendieer.Name].Distance,
                        maxTotalDistance);
                }

                foreach (var score in scoreBoard)
                {
                    if (score.Value.Distance == maxTotalDistance)
                    {
                        score.Value.Points += 1;
                    }
                }
            }

            return new(scoreBoard.Max(x => x.Value.Points).ToString());
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
