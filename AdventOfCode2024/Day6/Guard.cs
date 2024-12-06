using System.Drawing;
using System.Runtime.CompilerServices;

namespace Day6;

public class Guard
{
    public Guard(Map map, int x, int y, Direction direction)
    {
        Map = map;
        StartPosition = new Point(x, y);
        StartDirection = direction;
        Position = StartPosition;
        Direction = StartDirection;

        PrepareVisited();
    }

    public Map Map { get; }
    public Point StartPosition { get; }
    public Direction StartDirection { get; }
    public Point Position { get; set; }
    public Direction Direction { get; set; }

    public Dictionary<Point, HashSet<Direction>> Visited { get; } = [];

    private void PrepareVisited()
    {
        for (var x = 1; x <= Map.Width; x++)
        {
            for (var y = 1; y <= Map.Height; y++)
            {
                Visited[new Point(x, y)] = [];
            }
        }
    }

    public bool Move()
    {
        var currentDirects = Visited[Position];

        // Loop detected
        if (!currentDirects.Add(Direction))
        {
            return true;
        }

        // Determine desired next position
        var next = GetNextPosition();

        // Check for collision
        var rotations = 0;
        while (Map.Obstacles.Contains(next) && rotations < 4)
        {
            Turn();
            currentDirects.Add(Direction);
            next = GetNextPosition();
            rotations++;
        }

        if (rotations == 4)
        {
            throw new Exception($"Stuck at {Position} :c");
        }

        Position = next;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Turn()
    {
        Direction = Direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => Direction
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Point GetNextPosition()
    {
        return Direction switch
        {
            Direction.Up => Position with {Y = Position.Y - 1},
            Direction.Down => Position with {Y = Position.Y + 1},
            Direction.Left => Position with {X = Position.X - 1},
            _ => Position with {X = Position.X + 1}
        };
    }

    public void Reset()
    {
        Position = StartPosition;
        Direction = StartDirection;

        foreach (var value in Visited.Values)
        {
            value.Clear();
        }
    }
}