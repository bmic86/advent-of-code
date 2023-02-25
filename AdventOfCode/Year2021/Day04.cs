using AoCHelper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Year2021
{
    public class Day04 : BaseDay
    {
        private class Board
        {
            public Board(int[,] data) => Data = data;

            public int[,] Data { get; private set; }
            public bool[,] Marks { get; private set; } = new bool[BoardSize, BoardSize];
        }

        private const int BoardSize = 5;
        private readonly char[] _numberSeparators = new[] { ' ', '\n' };

        private readonly IReadOnlyList<int> _numbers;
        private readonly IReadOnlyList<int[,]> _boardsData;

        public Day04()
            => (_numbers, _boardsData) = LoadData();

        public override ValueTask<string> Solve_1()
        {
            IReadOnlyList<Board> boards = CreateBoards();

            foreach (var number in _numbers)
            {
                MarkNumberOnAllBoards(number, boards);
                List<Board> winningBoards = GetWinningBoards(boards);
                if (winningBoards.Any())
                {
                    int score = GetBoardScore(winningBoards.First()) * number;
                    return new(score.ToString());
                }
            }

            return new("No result found.");
        }

        public override ValueTask<string> Solve_2()
        {
            List<Board> boards = CreateBoards();
            Board? lastWinningBoard = null;
            int? lastWinningNumber = null;

            foreach (var number in _numbers)
            {
                MarkNumberOnAllBoards(number, boards);
                List<Board> winningBoards = GetWinningBoards(boards);
                if (winningBoards.Any())
                {
                    foreach (var winningBoard in winningBoards)
                    {
                        boards.Remove(winningBoard);
                    }

                    lastWinningBoard = winningBoards.Last();
                    lastWinningNumber = number;
                }
            }

            if (lastWinningBoard != null && lastWinningNumber != null)
            {
                int score = GetBoardScore(lastWinningBoard) * lastWinningNumber.Value;
                return new(score.ToString());
            }

            return new("No result found.");
        }

        private List<Board> CreateBoards()
            => _boardsData.Select(data => new Board(data)).ToList();

        private static int GetBoardScore(Board board)
        {
            int score = 0;
            for (int y = 0; y < BoardSize; ++y)
            {
                for (int x = 0; x < BoardSize; ++x)
                {
                    if (!board.Marks[x, y])
                    {
                        score += board.Data[x, y];
                    }
                }
            }
            return score;
        }

        private static List<Board> GetWinningBoards(IReadOnlyList<Board> boards)
        {
            List<Board> result = new();

            Span<int> rows = stackalloc int[BoardSize];
            Span<int> columns = stackalloc int[BoardSize];

            foreach (var board in boards)
            {
                rows.Clear();
                columns.Clear();

                for (int y = 0; y < BoardSize; ++y)
                {
                    for (int x = 0; x < BoardSize; ++x)
                    {
                        if (board.Marks[y, x])
                        {
                            rows[y]++;
                            columns[x]++;
                        }
                    }
                }

                if (rows.Contains(BoardSize) || columns.Contains(BoardSize))
                {
                    result.Add(board);
                }
            }

            return result;
        }

        private static void MarkNumberOnAllBoards(
            int number,
            IReadOnlyList<Board> boards)
        {
            foreach (var board in boards)
            {
                for (int y = 0; y < BoardSize; ++y)
                {
                    for (int x = 0; x < BoardSize; ++x)
                    {
                        if (board.Data[y, x] == number)
                        {
                            board.Marks[y, x] = true;
                        }
                    }
                }
            }
        }

        private (List<int> Numbers, List<int[,]> BoardsData) LoadData()
        {
            string[] splitted = File
                .ReadAllText(InputFilePath)
                .Split("\n\n");

            List<int> numbers = splitted.First()
                .Split(',')
                .Select(int.Parse)
                .ToList();

            var boards = splitted.Skip(1)
                .Select(board =>
                {
                    var data = board.Split(
                        _numberSeparators,
                        StringSplitOptions.RemoveEmptyEntries);

                    return ParseBoardData(data);
                })
                .ToList();

            return (numbers, boards);
        }

        private static int[,] ParseBoardData(string[] data)
        {
            int stringIndex = 0;
            var boardData = new int[BoardSize, BoardSize];

            for (int y = 0; y < BoardSize; ++y)
            {
                for (int x = 0; x < BoardSize; ++x)
                {
                    int value = int.Parse(data[stringIndex++]);
                    boardData[y, x] = value;
                }
            }

            return boardData;
        }
    }
}
