namespace AdventOfCode.Year2015.Common
{
    public class Node
    {
        public Node(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public Dictionary<string, int> ConnectionCosts { get; set; } = new Dictionary<string, int>();
    }
}
