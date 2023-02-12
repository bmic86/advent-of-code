namespace AdventOfCode.Common
{
    public record struct Vector2Int(int X, int Y)
    {
        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
            => new (a.X + b.X, a.Y + b.Y);

        public static Vector2Int operator *(Vector2Int a, int mul)
            => new(a.X * mul, a.Y * mul);

        public int GetManhattanDistanceFromOrigin()
            => Math.Abs(X) + Math.Abs(Y);
    }
}
