var pairs = File.ReadAllText("Input.txt")
    .Trim()
    .Split("\n")
    .Select(line =>
        line
            .Split(",")
            .Select(range =>
                range
                    .Split("-")
                    .Select(int.Parse)
                    .ToList()
            )
            .Select(bounds => bounds.First()..bounds.Last())
            .ToList()
    )
    .ToList();

// 1
Console.WriteLine(pairs.Count(pair =>
    pair.First().Contains(pair.Last()) || pair.Last().Contains(pair.First())
));

// 2
Console.WriteLine(pairs.Count(pair =>
    pair.First().OverlapsWith(pair.Last())
));


static class Extensions
{
    // Why does this need to be inside a class?
    public static bool Contains(this Range first, Range second)
    {
        return first.Start.Value <= second.Start.Value && first.End.Value >= second.End.Value;
    }

    static bool Contains(this Range range, int value)
    {
        return range.Start.Value <= value && value <= range.End.Value;
    }

    public static bool OverlapsWith(this Range first, Range second)
    {
        return
            first.Contains(second.Start.Value) || first.Contains(second.End.Value) ||
            second.Contains(first.Start.Value) || second.Contains(first.End.Value);
    }
}