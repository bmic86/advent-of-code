using AoCHelper;

namespace AdventOfCode.Year2015
{
    public class Day05 : BaseDay
    {
        private readonly IReadOnlySet<char> _vovels = new HashSet<char>()
        {
            'a', 'e', 'i', 'o', 'u'
        };

        private readonly IReadOnlyList<string> _disallowedPhrases = new List<string>()
        {
            "ab", "cd", "pq", "xy"
        };

        public override ValueTask<string> Solve_1()
        {
            string[] inputLines = File.ReadAllLines(InputFilePath);

            int niceStringsCount = inputLines.Count(line => IsNice(line));
            return new(niceStringsCount.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            string[] inputLines = File.ReadAllLines(InputFilePath);

            int niceStringsCount = inputLines.Count(line => IsNice2(line));
            return new(niceStringsCount.ToString());
        }

        private bool IsNice(string str)
        {
            if (ContainsDisallowedPhrase(str))
            {
                return false;
            }

            int vovelsCount = 0;
            bool isTwoInARow = false;
            char? lastLetter = null;

            foreach (char c in str)
            {
                if (_vovels.Contains(c))
                {
                    vovelsCount++;
                }

                if (lastLetter.HasValue && lastLetter == c)
                {
                    isTwoInARow = true;
                }

                lastLetter = c;
            }

            return (vovelsCount >= 3) && isTwoInARow;
        }

        private bool ContainsDisallowedPhrase(string str)
            => _disallowedPhrases.Any(x => str.Contains(x));

        private static bool IsNice2(string str)
            => HasDoublePair(str) && HasOneLetterBetweenPair(str);

        private static bool HasDoublePair(string str)
        {
            for (int i = 0; i < str.Length - 2; i++)
            {
                if (str[(i + 2)..].Contains(str.Substring(i, 2)))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasOneLetterBetweenPair(string str)
        {
            for (int i = 2; i < str.Length; i++)
            {
                if (str[i] == str[i - 2])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
