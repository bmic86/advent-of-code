using AoCHelper;

namespace AdventOfCode.Year2017
{
    public class Day04 : BaseDay
    {
        private readonly string[][] _passphrases;

        public Day04() => _passphrases = LoadData();

        public override ValueTask<string> Solve_1()
        {
            int result = 0;
            HashSet<string> uniqueWords = new();

            foreach (var passphrase in _passphrases)
            {
                result += AllWordsUnique(passphrase, uniqueWords);
            }

            return new(result.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            int result = 0;
            HashSet<string> uniqueWords = new();

            foreach (string[] passphrase in _passphrases)
            {
                var sorted = passphrase
                    .Select(word => string.Concat(word.Order()));

                result += AllWordsUnique(sorted, uniqueWords);
            }

            return new(result.ToString());
        }

        private static int AllWordsUnique(
            IEnumerable<string> passphrase,
            HashSet<string> uniqueWords)
        {
            uniqueWords.Clear();

            foreach (string word in passphrase)
            {
                if (uniqueWords.Contains(word))
                {
                    return 0;
                }

                uniqueWords.Add(word);
            }

            return 1;
        }

        private string[][] LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(line => line.Split(' '))
                .ToArray();
    }
}
