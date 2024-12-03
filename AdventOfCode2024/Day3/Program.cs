// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

void Part1(string s)
{
    var regex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)", RegexOptions.Compiled);

    var total = 0;
    var matches = regex.Matches(s);
    foreach (Match match in matches)
    {
        var x = int.Parse(match.Groups[1].Value);
        var y = int.Parse(match.Groups[2].Value);
        total += x * y;
    }

    Console.WriteLine("Part 1: " + total);
}

void Part2(string s)
{
    var regex = new Regex(@"do\(\)|don\'t\(\)|mul\((\d{1,3}),(\d{1,3})\)", RegexOptions.Compiled);

    var total = 0;
    var matches = regex.Matches(s);
    var enabled = true;
    foreach (Match match in matches)
    {
        if (match.Value == "do()")
        {
            enabled = true;
        }
        else if (match.Value == "don't()")
        {
            enabled = false;
        }
        else if (enabled)
        {
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            total += x * y;
        }
    }

    Console.WriteLine("Part 2: " + total);
}

var input = File.ReadAllText("input1.txt");
Part1(input);
Part2(input);