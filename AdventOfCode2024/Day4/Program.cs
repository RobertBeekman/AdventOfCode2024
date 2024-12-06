using System.Diagnostics;

var xmas = "XMAS".ToArray();
bool IsMatch(char[] arr)
{
    for (var i = 0; i < 4; i++)
    {
        if (arr[i] != xmas[i])
        {
            return false;
        }
    }

    return true;
}

char GetChar(char[][] matrix, int x, int y)
{
    if (x < 0 || y < 0 || x >= matrix[0].Length || y >= matrix.Length)
    {
        return default;
    }

    return matrix[y][x];
}

void Part1(char[][] matrix)
{
    var matches = 0;
    var height = matrix.Length;
    var width = matrix[0].Length;

    for (var x = 0; x < width; x++)
    {
        for (var y = 0; y < height; y++)
        {
            if (GetChar(matrix, x, y) != 'X')
            {
                continue;
            }

            // Left-to-right
            if (IsMatch([GetChar(matrix, x, y), GetChar(matrix, x + 1, y), GetChar(matrix, x + 2, y), GetChar(matrix, x + 3, y)]))
            {
                matches++;
            }

            // Right-to-left
            if (IsMatch([GetChar(matrix, x, y), GetChar(matrix, x - 1, y), GetChar(matrix, x - 2, y), GetChar(matrix, x - 3, y)]))
            {
                matches++;
            }

            // Top-to-bottom
            if (IsMatch([GetChar(matrix, x, y), GetChar(matrix, x, y + 1), GetChar(matrix, x, y + 2), GetChar(matrix, x, y + 3)]))
            {
                matches++;
            }

            // Bottom-to-top
            if (IsMatch([GetChar(matrix, x, y), GetChar(matrix, x, y - 1), GetChar(matrix, x, y - 2), GetChar(matrix, x, y - 3)]))
            {
                matches++;
            }

            // Top-left-to-bottom-right
            if (IsMatch([GetChar(matrix, x, y), GetChar(matrix, x + 1, y + 1), GetChar(matrix, x + 2, y + 2), GetChar(matrix, x + 3, y + 3)]))
            {
                matches++;
            }

            // Bottom-right-to-top-left
            if (IsMatch([GetChar(matrix, x, y), GetChar(matrix, x - 1, y - 1), GetChar(matrix, x - 2, y - 2), GetChar(matrix, x - 3, y - 3)]))
            {
                matches++;
            }

            // Top-right-to-bottom-left
            if (IsMatch([GetChar(matrix, x, y), GetChar(matrix, x - 1, y + 1), GetChar(matrix, x - 2, y + 2), GetChar(matrix, x - 3, y + 3)]))
            {
                matches++;
            }

            // Bottom-left-to-top-right
            if (IsMatch([GetChar(matrix, x, y), GetChar(matrix, x + 1, y - 1), GetChar(matrix, x + 2, y - 2), GetChar(matrix, x + 3, y - 3)]))
            {
                matches++;
            }
        }
    }

    Console.WriteLine("Part 1: " + matches);
}

void Part2(char[][] matrix)
{
    var matches = 0;
    var height = matrix.Length;
    var width = matrix[0].Length;

    for (var x = 0; x < width; x++)
    {
        for (var y = 0; y < height; y++)
        {
            bool foundBackslash = false;
            bool foundSlash = false;
            if (GetChar(matrix, x, y) != 'A')
            {
                continue;
            }

            // M
            //  A
            //   S
            if (GetChar(matrix, x - 1, y - 1) == 'M' && GetChar(matrix, x + 1, y + 1) == 'S')
            {
                foundBackslash = true;
            }

            // S
            //  A
            //   M
            if (GetChar(matrix, x - 1, y - 1) == 'S' && GetChar(matrix, x + 1, y + 1) == 'M')
            {
                foundBackslash = true;
            }

            //   S
            //  A
            // M
            if (GetChar(matrix, x - 1, y + 1) == 'M' && GetChar(matrix, x + 1, y - 1) == 'S')
            {
                foundSlash = true;
            }

            //   M
            //  A
            // S
            if (GetChar(matrix, x - 1, y + 1) == 'S' && GetChar(matrix, x + 1, y - 1) == 'M')
            {
               foundSlash = true;
            }
            
            if (foundBackslash && foundSlash)
            {
                matches++;
            }
        }
    }

    Console.WriteLine("Part 2: " + matches);
}

var input = File.ReadAllLines("input1.txt").Select(x => x.ToArray()).ToArray();
var sw = Stopwatch.StartNew();
Part1(input);
Console.WriteLine($"Part 1 took {sw.ElapsedMilliseconds}ms");
sw.Restart();
Part2(input);
Console.WriteLine($"Part 2 took {sw.ElapsedMilliseconds}ms");