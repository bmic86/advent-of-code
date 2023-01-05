using AoCHelper;
using System.Text;

namespace AdventOfCode.Year2015
{
    public class Day19 : BaseDay
    {
        private record ReplacementPair(string Old, string New);

        private readonly IReadOnlyList<ReplacementPair> _replacementPairs;
        private readonly string _inputMolecule;

        public Day19()
            => (_replacementPairs, _inputMolecule) = LoadData();

        public override ValueTask<string> Solve_1()
            => new(CalculateUniqueMoleculesCount().ToString());

        public override ValueTask<string> Solve_2()
        {
            throw new NotImplementedException();
        }

        private int CalculateUniqueMoleculesCount()
        {
            HashSet<string> uniqueMolecules = new HashSet<string>();

            foreach (var pair in _replacementPairs)
            {
                int index = 0;
                while ((index = _inputMolecule.IndexOf(pair.Old, index)) != -1)
                {
                    var stringBuilder = new StringBuilder(_inputMolecule);
                    stringBuilder.Replace(pair.Old, pair.New, index, pair.Old.Length);
                    index += pair.Old.Length;

                    uniqueMolecules.Add(stringBuilder.ToString());
                }
            }

            return uniqueMolecules.Count;
        }

        private (List<ReplacementPair>, string) LoadData()
        {
            List<ReplacementPair> replacementPairs = new();

            string[] lines = File.ReadAllLines(InputFilePath);
            foreach (string replacementLine in lines.Take(lines.Length - 2))
            {
                string[] splitted = replacementLine
                    .Split("=>", StringSplitOptions.RemoveEmptyEntries);

                replacementPairs.Add(new(splitted[0].Trim(), splitted[1].Trim()));
            }

            return new(replacementPairs, lines.Last());
        }
    }
}
