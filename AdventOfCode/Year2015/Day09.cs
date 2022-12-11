using AdventOfCode.Year2015.Common;
using AoCHelper;

using City = AdventOfCode.Year2015.Common.Node;

namespace AdventOfCode.Year2015
{
    public class Day09 : BaseDay
    {
        private readonly List<City> _cities;

        public Day09() => _cities = LoadData();

        public override ValueTask<string> Solve_1()
            => new(CalculateAllPossiblePathCosts().Min().ToString());

        public override ValueTask<string> Solve_2()
            => new(CalculateAllPossiblePathCosts().Max().ToString());

        private List<City> LoadData()
        {
            var nameToCityMap = new Dictionary<string, City>();

            foreach (string line in File.ReadAllLines(InputFilePath))
            {
                string[] splitedLine = line.Split(' ');

                City firstCity = GetOrAddCityMapping(nameToCityMap, splitedLine[0]);
                City secondCity = GetOrAddCityMapping(nameToCityMap, splitedLine[2]);

                int connectionCost = int.Parse(splitedLine[4]);

                nameToCityMap[firstCity.Name]
                    .ConnectionCosts[secondCity.Name] = connectionCost;

                nameToCityMap[secondCity.Name]
                    .ConnectionCosts[firstCity.Name] = connectionCost;
            }

            return nameToCityMap.Select(c => c.Value).ToList();
        }

        private IEnumerable<int> CalculateAllPossiblePathCosts()
            => Permutations.Generate(_cities, _cities.Count)
                .Select(x => x.Aggregate(
                    (costSum: 0, lastCity: (City?)null),
                    (acc, currentCity) =>
                    {
                        if (acc.lastCity != null)
                        {
                            int cost = currentCity
                                .ConnectionCosts[acc.lastCity.Name];

                            return (acc.costSum + cost, currentCity);
                        }

                        return (0, currentCity);
                    }))
                .Select(y => y.costSum);

        private static City GetOrAddCityMapping(
            Dictionary<string, City> nameToCityMap,
            string cityName)
        {
            if (!nameToCityMap.TryGetValue(cityName, out City? city))
            {
                city = new City(cityName);
                nameToCityMap[cityName] = city;
            }

            return city;
        }
    }
}
