var chars = File.ReadAllText("Input.txt")
    .Trim()
    .ToCharArray();

var trees = new List<Tree>();

var row = 0;
var col = 0;
foreach (var c in chars)
{
    if (c == '\n')
    {
        row++;
        col = 0;
    }
    else
    {
        trees.Add(new Tree(int.Parse(c.ToString()), row, col));
        col++;
    }
}

trees = trees
    .Select(tree =>
    {
        var groups = trees
            .Where(t => t.X == tree.X ^ t.Y == tree.Y)
            .GroupBy(t =>
                new LocationData(t.X.CompareTo(tree.X), t.Y.CompareTo(tree.Y))
            )
            .ToList();

        tree.Visible = groups.Count(g => g.Any(t => t.Height >= tree.Height)) < 4;

        if (groups.Count == 4)
            tree.ScenicScore = groups.Select(g =>
                {
                    var orderedGroup = g.OrderBy(t => Math.Abs(tree.Y - t.Y + tree.X - t.X));
                    var count = 0;
                    foreach (var t in orderedGroup)
                    {
                        count++;
                        if (t.Height >= tree.Height) break;
                    }

                    return count;
                }
            ).Aggregate(1, (p, n) => p * n);

        return tree;
    })
    .ToList();

Console.WriteLine(trees.Count(t => t.Visible));
Console.WriteLine(trees.MaxBy(t => t.ScenicScore)?.ScenicScore);

internal class Tree
{
    public readonly int Height;
    public readonly int X;
    public readonly int Y;
    public bool Visible;
    public int ScenicScore;

    public Tree(int height, int x, int y)
    {
        Height = height;
        X = x;
        Y = y;
    }
}

internal record LocationData(int XCompare, int YCompare);