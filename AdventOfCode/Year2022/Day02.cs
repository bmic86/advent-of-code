using AoCHelper;

namespace AdventOfCode.Year2022
{
    public class Day02 : BaseDay
    {
        private enum Shape
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3,
        }

        private enum GameResult
        {
            Lose = 0,
            Tie = 3,
            Win = 6,
        }

        private record Shapes(Shape Opponent, Shape My);
        private record GameRound(Shape OpponentShape, GameResult MyResult);

        private readonly IReadOnlyList<string[]> _encodedGames;

        public Day02() => _encodedGames = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int result = _encodedGames
                .Select(encoded =>
                {
                    Shape opponentShape = DecodeShape(encoded[0]);
                    Shape myShape = DecodeShape(encoded[1]);

                    return new Shapes(opponentShape, myShape);
                })
                .Sum(shapes => (int)GetMyRoundResult(shapes) + (int)shapes.My);

            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int result = _encodedGames
                .Select(encoded =>
                {
                    Shape opponentShape = DecodeShape(encoded[0]);
                    GameResult myResult = DecodeGameResult(encoded[1]);

                    return new GameRound(opponentShape, myResult);
                })
                .Sum(gameRound => (int)gameRound.MyResult + (int)GetMyShape(gameRound));

            return new(result.ToString());
        }

        private static Shape GetMyShape(GameRound gameRound)
            => gameRound switch
            {
                (Shape.Rock, GameResult.Lose) => Shape.Scissors,
                (Shape.Paper, GameResult.Lose) => Shape.Rock,
                (Shape.Scissors, GameResult.Lose) => Shape.Paper,

                (Shape.Rock, GameResult.Win) => Shape.Paper,
                (Shape.Paper, GameResult.Win) => Shape.Scissors,
                (Shape.Scissors, GameResult.Win) => Shape.Rock,

                _ => gameRound.OpponentShape,
            };

        private static GameResult GetMyRoundResult(Shapes shapes)
            => shapes switch
            {
                (Shape.Rock, Shape.Scissors) => GameResult.Lose,
                (Shape.Paper, Shape.Rock) => GameResult.Lose,
                (Shape.Scissors, Shape.Paper) => GameResult.Lose,

                (Shape.Rock, Shape.Paper) => GameResult.Win,
                (Shape.Paper, Shape.Scissors) => GameResult.Win,
                (Shape.Scissors, Shape.Rock) => GameResult.Win,

                _ => GameResult.Tie,
            };

        private List<string[]> LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(line => line.Split(' '))
                .ToList();

        private static Shape DecodeShape(string value)
            => value switch
            {
                "A" or "X" => Shape.Rock,
                "B" or "Y" => Shape.Paper,
                "C" or "Z" => Shape.Scissors,
                _ => throw new ArgumentException($"Invalid value `{value}`."),
            };

        private static GameResult DecodeGameResult(string value)
            => value switch
            {
                "X" => GameResult.Lose,
                "Y" => GameResult.Tie,
                "Z" => GameResult.Win,
                _ => throw new ArgumentException($"Invalid value `{value}`."),
            };
    }
}
