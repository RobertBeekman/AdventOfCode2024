using System.Drawing;

namespace Day6;

public class Map
{
    public Map(char[][] input)
    {
        Width = input[0].Length;
        Height = input.Length;
        Obstacles = [];

        for (var y = 0; y < input.Length; y++)
        {
            var row = input[y];
            for (var x = 0; x < row.Length; x++)
            {
                var column = row[x];
                if (column == '#')
                {
                    Obstacles.Add(new Point(x + 1, y + 1));
                }
                else if (column == '^')
                {
                    Guard = new Guard(this, x + 1, y + 1, Direction.Up);
                }
                else if (column == 'v')
                {
                    Guard = new Guard(this, x + 1, y + 1, Direction.Down);
                }
                else if (column == '<')
                {
                    Guard = new Guard(this, x + 1, y + 1, Direction.Left);
                }
                else if (column == '>')
                {
                    Guard = new Guard(this, x + 1, y + 1, Direction.Right);
                }
            }
        }

        if (Guard == null)
        {
            throw new Exception("No guard was found");
        }
    }

    public HashSet<Point> Obstacles { get; }
    public Guard Guard { get; }
    public int Width { get; }
    public int Height { get; }

    public bool InBounds(Point position)
    {
        return position.X >= 1 && position.X <= Width && position.Y >= 1 && position.Y <= Height;
    }
}