using Day2;

void Part1(string[] input)
{
    var safeCount = 0;
    foreach (var row in input)
    {
        var levels = row.Split(" ").Select(int.Parse).ToList();
        var safe = Validate(levels);
        if (safe)
        {
            safeCount++;
        }
    }

    Console.WriteLine("Part 1: " + safeCount);
}

void Part2(string[] input)
{
    var safeCount = 0;
    foreach (var row in input)
    {
        var levels = row.Split(" ").Select(int.Parse).ToList();
        var safe = Validate(levels);
        if (safe)
        {
            safeCount++;
            continue;
        }

        // Bruteforce babyyyy
        for (var index = 0; index < levels.Count; index++)
        {
            var dampened = new List<int>(levels);
            dampened.RemoveAt(index);
            if (Validate(dampened))
            {
                safeCount++;
                break;
            }
        }
    }

    Console.WriteLine("Part 2: " + safeCount);
}

var input = await File.ReadAllLinesAsync("input1.txt");
Part1(input);
Part2(input);

bool Validate(List<int> ints)
{
    var rowDirection = Direction.Undetermined;
    var safe = true;
    var previous = ints[0];
    foreach (var level in ints.Skip(1))
    {
        var direction = level > previous ? Direction.Up : Direction.Down;
        if (rowDirection == Direction.Undetermined)
        {
            rowDirection = direction;
        }

        if (direction != rowDirection)
        {
            safe = false;
            break;
        }

        if (Math.Abs(previous - level) is 0 or > 3)
        {
            safe = false;
            break;
        }

        previous = level;
    }

    return safe;
}