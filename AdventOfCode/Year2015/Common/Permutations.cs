namespace AdventOfCode.Year2015.Common
{
    public static class Permutations
    {
        public static IEnumerable<IEnumerable<T>> Generate<T>(IEnumerable<T> list, int length)
            where T : class
        {
            if (length == 1)
            {
                return list.Select(y => new[] { y });
            }

            return Generate(list, length - 1)
                .SelectMany(x => list.Where(c => !x.Contains(c)),
                    (cities, city) => cities.Concat(new[] { city }));
        }
    }
}
