var parts = File.ReadAllText("Input.txt")
    .TrimEnd()
    .Split("\n\n");

var stacks = GetStacks(parts.First());

var moves = parts.Last()
    .Split("\n")
    .Select(Move.From)
    .ToList();

// 1
Console.WriteLine(
    string.Join(
        "",
        moves
            .Aggregate(stacks, (endStacks, move) =>
                {
                    var count = Math.Min(move.count, endStacks[move.source].Count);
                    for (var _ = 0; _ < count; _++)
                    {
                        endStacks[move.destination].Add(endStacks[move.source].Last());
                        endStacks[move.source].RemoveAt(endStacks[move.source].Count - 1);
                    }

                    return endStacks;
                }
            )
            .Select(stack => stack.Value.Last().Name)
    )
);

// C# is kinda dumb sometimes
stacks = GetStacks(parts.First());

// 2
Console.WriteLine(
    string.Join(
        "",
        moves
            .Aggregate(stacks, (endStacks, move) =>
                {
                    var count = Math.Min(move.count, endStacks[move.source].Count);
                    var removeIndex = endStacks[move.source].Count - count;
                    while (endStacks[move.source].Count > removeIndex)
                    {
                        endStacks[move.destination].Add(endStacks[move.source][removeIndex]);
                        endStacks[move.source].RemoveAt(removeIndex);
                    }

                    return endStacks;
                }
            )
            .Select(stack => stack.Value.Last().Name)
    )
);

Dictionary<int, List<Container>> GetStacks(string input) => input
    .Split("\n")
    .SkipLast(1)
    .Reverse()
    .Aggregate(new List<Container>(), (containers, line) =>
    {
        for (var i = 0; i < line.Length; i++)
        {
            var chr = line[i];
            if (chr is >= 'A' and <= 'Z')
            {
                containers.Add(new Container(chr, (i - 1) / 4 + 1));
            }
        }

        return containers;
    })
    .GroupBy(c => c.Stack)
    .OrderBy(g => g.Key)
    .ToDictionary(g => g.Key, g => g.ToList());


class Container
{
    public Container(char name, int stack)
    {
        Name = name;
        Stack = stack;
    }

    public char Name;
    public int Stack { get; }
}

class Move
{
    public Move(int count, int source, int destination)
    {
        this.count = count;
        this.source = source;
        this.destination = destination;
    }

    public int count;
    public int source;
    public int destination;

    public static Move From(string input)
    {
        var parts = input.Split(" ");

        return new Move(
            int.Parse(parts[1]),
            int.Parse(parts[3]),
            int.Parse(parts[5])
        );
    }
}