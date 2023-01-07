using AoCHelper;

namespace AdventOfCode.Year2018
{
    public class Day01 : BaseDay
    {
        private readonly int[] _frequencyChanges;

        public Day01()
            => _frequencyChanges = LoadData();


        public override ValueTask<string> Solve_1()
            => new(_frequencyChanges.Sum().ToString());

        public override ValueTask<string> Solve_2()
        {
            HashSet<int> uniqueFrequencies = new HashSet<int>();
            int currentFrequency = 0;

            bool run = true;
            while (run)
            {
                foreach (int change in _frequencyChanges)
                {
                    currentFrequency += change;
                    if (uniqueFrequencies.Contains(currentFrequency))
                    {
                        run = false;
                        break;
                    }
                    uniqueFrequencies.Add(currentFrequency);
                }
            }

            return new(currentFrequency.ToString());
        }

        private int[] LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(int.Parse)
                .ToArray();
    }
}
