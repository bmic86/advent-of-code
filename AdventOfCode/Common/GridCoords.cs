namespace AdventOfCode.Common
{
    public record struct GridCoords(int X, int Y)
    {
        public int GetManhattanDistanceFromOrigin()
            => Math.Abs(X) + Math.Abs(Y);
    }
}
