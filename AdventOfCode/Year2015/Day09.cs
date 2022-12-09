using AoCHelper;

namespace AdventOfCode.Year2015
{
    public record Connection(string CityName, int Cost);

    public class City
    {
        public City(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public List<Connection> Connections { get; set; } = new List<Connection>();
    }

    public class Day09 : BaseDay
    {
        private readonly List<City> _cities;

        public Day09() => _cities = LoadData();

        public override ValueTask<string> Solve_1()
            => new(CalculateAllPosiblePathCosts().Min().ToString());

        public override ValueTask<string> Solve_2()
            => new(CalculateAllPosiblePathCosts().Max().ToString());

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
                    .Connections
                    .Add(new Connection(secondCity.Name, connectionCost));

                nameToCityMap[secondCity.Name]
                    .Connections
                    .Add(new Connection(firstCity.Name, connectionCost));
            }

            return nameToCityMap.Select(c => c.Value).ToList();
        }

        private IEnumerable<int> CalculateAllPosiblePathCosts()
            => GenerateCitiesPermutations(_cities, _cities.Count)
                .Select(x => x.Aggregate(
                    (costSum: 0, lastCity: (City?)null),
                    (acc, currentCity) =>
                    {
                        if (acc.lastCity != null)
                        {
                            int cost = currentCity
                                .Connections
                                .First(con => con.CityName == acc.lastCity.Name)
                                .Cost;

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

        private static IEnumerable<IEnumerable<City>> GenerateCitiesPermutations(
            IEnumerable<City> list,
            int length)
        {
            if (length == 1)
            {
                return list.Select(city => new[] { city });
            }

            return GenerateCitiesPermutations(list, length - 1)
                .SelectMany(x => list.Where(c => !x.Contains(c)),
                    (cities, city) => cities.Concat(new[] { city }));
        }
    }
}
