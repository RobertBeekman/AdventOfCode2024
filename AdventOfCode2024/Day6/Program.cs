﻿// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Drawing;
using Day6;

void Part1(char[][] chars)
{
    var map = new Map(chars);
    while (map.InBounds(map.Guard.Position))
    {
        map.Guard.Move();
    }

    Console.WriteLine($"Part 1: {map.Guard.Visited.Count(v => v.Value.Count > 0)}");
}

void Part2(char[][] chars)
{
    var map = new Map(chars);
    var positions = 0;
    
    // Gather relevant positions
    while (map.InBounds(map.Guard.Position))
    {
        map.Guard.Move();
    }
    var relevant = map.Guard.Visited.Where(v => v.Value.Count > 0).Select(v => v.Key).ToList();
    map.Guard.Reset();
    
    foreach (var position in relevant)
    {
        // If there is already an obstacle or the guard, skip
        if (map.Obstacles.Contains(position))
        {
            continue;
        }

        if (map.Guard.Position == position)
        {
            continue;
        }

        map.Obstacles.Add(position);
        if (FindLoop())
        {
            positions++;
        }

        map.Guard.Reset();
        map.Obstacles.Remove(position);
    }

    bool FindLoop()
    {
        while (map.InBounds(map.Guard.Position))
        {
            if (map.Guard.Move())
            {
                return true;
            }
        }

        return false;
    }

    Console.WriteLine($"Part 2: {positions}");
}

var input = File.ReadAllLines("input1.txt").Select(l => l.ToArray()).ToArray();

var sw = Stopwatch.StartNew();
Part1(input);
Console.WriteLine($"Part 1 took {sw.ElapsedMilliseconds} ms");

sw.Restart();
Part2(input);
Console.WriteLine($"Part 2 took {sw.ElapsedMilliseconds} ms");