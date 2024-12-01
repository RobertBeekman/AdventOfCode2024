using System.Diagnostics;

void Part1(List<int> column1, List<int> column2)
{
    var answer = column1.Order().Zip(column2.Order(), (a, b) => Math.Abs(a - b)).Sum();
    Console.WriteLine("Part 1 answer: " + answer);
}

void Part2(List<int> column1, List<int> column2)
{
    var column2Counts = column2.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
    var answer = 0;
    foreach (var i in column1)
    {
        if (column2Counts.TryGetValue(i, out var count))
            answer += i * count;
    }

    Console.WriteLine("Part 2 answer: " + answer);
}

var input = File.ReadAllLines("input1.txt");
    
var column1 = new List<int>(input.Length);
var column2 = new List<int>(input.Length);

foreach (var row in input)
{
    var parts = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    column1.Add(int.Parse(parts[0]));
    column2.Add(int.Parse(parts[1]));
}

var sw = Stopwatch.StartNew();
Part1(column1, column2);
Console.WriteLine($"Part 1 took {sw.ElapsedMilliseconds} ms");

sw.Restart();
Part2(column1, column2);
Console.WriteLine($"Part 2 took {sw.ElapsedMilliseconds} ms");