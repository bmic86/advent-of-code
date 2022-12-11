using AdventOfCode.Year2015.Common;
using AoCHelper;

using Person = AdventOfCode.Year2015.Common.Node;

namespace AdventOfCode.Year2015
{
    public class Day13 : BaseDay
    {
        private readonly List<Person> _people;

        public Day13() => _people = LoadData();

        public override ValueTask<string> Solve_1()
            => new(CalculateAllPossibleChangeOfHappiness().Max().ToString());

        public override ValueTask<string> Solve_2()
        {
            AddMeToPeople();
            return new(CalculateAllPossibleChangeOfHappiness().Max().ToString());
        }

        private void AddMeToPeople()
        {
            var me = new Person("Bartosz");
            foreach (var person in _people)
            {
                person.ConnectionCosts[me.Name] = 0;
                me.ConnectionCosts[person.Name] = 0;
            }
            _people.Add(me);
        }

        private List<Person> LoadData()
        {
            var personToNeighbourMap = new Dictionary<string, Person>();

            foreach (string line in File.ReadAllLines(InputFilePath))
            {
                string[] splitedLine = line.Split(' ');

                Person firstPerson = GetOrAddPersonMapping(personToNeighbourMap, splitedLine[0]);
                Person secondPerson = GetOrAddPersonMapping(personToNeighbourMap, splitedLine[10].TrimEnd('.'));

                int absCost = int.Parse(splitedLine[3]);
                int connectionCost = splitedLine[2] == "lose" ? -absCost : absCost;

                personToNeighbourMap[firstPerson.Name]
                    .ConnectionCosts[secondPerson.Name] = connectionCost;
            }

            return personToNeighbourMap.Select(c => c.Value).ToList();
        }

        private IEnumerable<int> CalculateAllPossibleChangeOfHappiness()
        {
            var people = Permutations.Generate(_people, _people.Count);
            return people.Select(x => x.Aggregate(
                    (costSum: 0, currentIndex: 0),
                    (acc, currentPerson) =>
                    {
                        int count = x.Count();

                        int previousIndex = (acc.currentIndex == 0) ? count - 1 : acc.currentIndex - 1;
                        acc.costSum += currentPerson
                            .ConnectionCosts[x.ElementAt(previousIndex).Name];

                        int nextIndex = (acc.currentIndex == count - 1) ? 0 : acc.currentIndex + 1;
                        acc.costSum += currentPerson
                            .ConnectionCosts[x.ElementAt(nextIndex).Name];

                        return (acc.costSum, acc.currentIndex + 1);

                    }))
                .Select(y => y.costSum);
        }

        private static Person GetOrAddPersonMapping(
            Dictionary<string, Person> personToNeighbourMap,
            string name)
        {
            if (!personToNeighbourMap.TryGetValue(name, out Person? person))
            {
                person = new Person(name);
                personToNeighbourMap[name] = person;
            }

            return person;
        }
    }
}
