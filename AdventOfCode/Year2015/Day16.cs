using AoCHelper;
using System.Collections.Generic;

namespace AdventOfCode.Year2015
{
    public class Day16 : BaseDay
    {
        private record AuntSue(int Number, Dictionary<string, int> Params);

        private readonly IReadOnlyDictionary<string, int> _queryParams = new Dictionary<string, int>()
        {
            { "children", 3 },
            { "cats", 7 },
            { "samoyeds", 2 },
            { "pomeranians", 3 },
            { "akitas", 0 },
            { "vizslas", 0 },
            { "goldfish", 5 },
            { "trees", 3 },
            { "cars", 2 },
            { "perfumes", 1 }
        };

        private readonly IReadOnlyList<AuntSue> _auntSues;

        public Day16() => _auntSues = LoadData();

        public override ValueTask<string> Solve_1()
        {
            AuntSue result = _auntSues.First(sue =>
                sue.Params.All(p => p.Value == _queryParams[p.Key]));

            return new(result.Number.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            AuntSue result = _auntSues.First(sue =>
                sue.Params.All(p => p.Key switch
                    {
                        "cats" or "trees" => p.Value > _queryParams[p.Key],
                        "pomeranians" or "goldfish" => p.Value < _queryParams[p.Key],
                        _ => p.Value == _queryParams[p.Key]
                    })
                );

            return new(result.Number.ToString());
        }

        private List<AuntSue> LoadData()
            => File.ReadAllLines(InputFilePath)
                .Select(line =>
                {
                    string[] segments = line.Split(',', ':');
                    int number = int.Parse(segments[0].Split(' ')[1]);

                    Dictionary<string, int> parameters = new();
                    for (int i = 1; i < segments.Length; i += 2)
                    {
                        parameters[segments[i].Trim()] = int.Parse(segments[i + 1]);
                    }

                    return new AuntSue(number, parameters);
                })
                .ToList();
    }
}
