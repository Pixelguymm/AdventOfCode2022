var rucksacks = File.ReadAllText("Input.txt")
    .Trim()
    .Split("\n")
    .Select(line =>
        new Rucksack(line.Trim())
    )
    .ToList();


// 1
Console.WriteLine(
    rucksacks.Select(r => GetPriority(r.Offender())).Sum()
);

// 2
Console.WriteLine(
    Enumerable.Range(0, rucksacks.Count / 3).Aggregate(new List<char>(), (groups, i) =>
        {
            var group = rucksacks[i * 3].ItemSet;
            group.IntersectWith(rucksacks[i * 3 + 1].ItemSet);
            group.IntersectWith(rucksacks[i * 3 + 2].ItemSet);

            groups.Add(group.ToList()[0]);
            return groups;
        }
    ).Select(GetPriority).Sum()
);

int GetPriority(char c)
{
    const int offset = 'A' - 1;

    var upper = c <= 'Z' ? 1 : 0;
    c = c.ToString().ToUpper()[0];
    return c - offset + upper * 26;
}

class Rucksack
{
    public Rucksack(string items)
    {
        _items = items;
    }

    private readonly string _items;

    public char Offender()
    {
        var halfLen = _items.Length / 2;
        var comp1 = _items[..halfLen];
        var comp2 = _items[halfLen..];

        var intersection = comp1.ToHashSet();
        intersection.IntersectWith(comp2.ToHashSet());
        return intersection.ToList()[0];
    }

    public HashSet<char> ItemSet => _items.ToHashSet();
}