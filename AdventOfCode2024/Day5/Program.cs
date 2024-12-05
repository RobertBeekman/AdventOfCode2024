using System.Diagnostics;

bool IsValid(List<int> pages, int current, Dictionary<int, HashSet<int>> bigger, Dictionary<int, HashSet<int>> smaller)
{
    var page = pages[current];

    // Validate previous pages
    for (var previous = 0; previous < current; previous++)
    {
        if (bigger.TryGetValue(page, out var biggerPages) && biggerPages.Contains(pages[previous]))
        {
            return false;
        }
    }

    // Validate next pages
    for (var next = current + 1; next < pages.Count; next++)
    {
        if (smaller.TryGetValue(page, out var smallerPages) && smallerPages.Contains(pages[next]))
        {
            return false;
        }
    }

    return true;
}

void Part1(List<(int x, int y)> rules, List<List<int>> pageSets)
{
    var result = 0;
    var bigger = new Dictionary<int, HashSet<int>>();
    foreach (var xGroups in rules.GroupBy(r => r.x))
    {
        bigger[xGroups.Key] = xGroups.Select(g => g.y).ToHashSet();
    }

    var smaller = new Dictionary<int, HashSet<int>>();
    foreach (var yGroups in rules.GroupBy(r => r.y))
    {
        smaller[yGroups.Key] = yGroups.Select(g => g.x).ToHashSet();
    }

    foreach (var pageSet in pageSets)
    {
        var valid = true;
        for (var current = 0; current < pageSet.Count; current++)
        {
            if (!IsValid(pageSet, current, bigger, smaller))
            {
                valid = false;
                break;
            }
        }

        if (valid)
        {
            result += pageSet[pageSet.Count / 2];
        }
    }

    Console.WriteLine("Part 1: " + result);
}

void Part2(List<(int x, int y)> rules, List<List<int>> pageSets)
{
    var result = 0;
    var bigger = new Dictionary<int, HashSet<int>>();
    foreach (var xGroups in rules.GroupBy(r => r.x))
    {
        bigger[xGroups.Key] = xGroups.Select(g => g.y).ToHashSet();
    }

    var smaller = new Dictionary<int, HashSet<int>>();
    foreach (var yGroups in rules.GroupBy(r => r.y))
    {
        smaller[yGroups.Key] = yGroups.Select(g => g.x).ToHashSet();
    }

    foreach (var pageSet in pageSets)
    {
        var valid = true;
        for (var current = 0; current < pageSet.Count; current++)
        {
            if (!IsValid(pageSet, current, bigger, smaller))
            {
                valid = false;
                break;
            }
        }

        if (!valid)
        {
            // Order correctly
            var original = new List<int>(pageSet);
            foreach (var page in original)
            {
                if (smaller.TryGetValue(page, out var smallerPages))
                {
                    pageSet[smallerPages.Count(p => original.Contains(p))] = page;
                }
                else
                {
                    pageSet[0] = page;
                }
            }
            
            result += pageSet[pageSet.Count / 2];
        }
    }

    Console.WriteLine("Part 2: " + result);
}

var input = File.ReadAllLines("input1.txt");
var rules = new List<(int x, int y)>();
var pageSets = new List<List<int>>();
foreach (var line in input)
{
    if (line.Contains('|'))
    {
        var split = line.Split('|');
        rules.Add((int.Parse(split[0]), int.Parse(split[1])));
    }
    else if (line.Contains(','))
    {
        pageSets.Add(line.Split(',').Select(int.Parse).ToList());
    }
}

var sw = Stopwatch.StartNew();
Part1(rules, pageSets);
Console.WriteLine($"Part 1 took {sw.ElapsedMilliseconds} ms");

sw.Restart();
Part2(rules, pageSets);
Console.WriteLine($"Part 2 took {sw.ElapsedMilliseconds} ms");