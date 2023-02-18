var positionMap = new Dictionary<int, HashSet<Position>>();

var rope = new List<RopeSegment>(10);

var lines = File.ReadAllText("Input.txt")
    .Trim()
    .Split("\n");

for (var i = 0; i < 10; i++)
{
    rope.Add(new RopeSegment());
    positionMap[i] = new HashSet<Position> {new(0, 0)};
}

foreach (var l in lines)
{
    var parts = l.Trim().Split(" ");
    if (Enum.TryParse(parts[0], false, out Direction direction))
    {
        for (var i = 0; i < int.Parse(parts[1]); i++)
        {
            rope[0].Move(direction);

            for (var j = 1; j < 10; j++)
            {
                var previous = rope[j - 1];
                var current = rope[j];
                
                var shouldMove = Math.Max(
                    Math.Abs(previous.X - current.X),
                    Math.Abs(previous.Y - current.Y)
                ) >= 2;

                if (shouldMove)
                {
                    var xDiff = Math.Abs(previous.X - current.X);
                    var yDiff = Math.Abs(previous.Y - current.Y);

                    var xOffset = 0;
                    var yOffset = 0;

                    if (xDiff >= 2) xOffset = current.X.CompareTo(previous.X);
                    if (yDiff >= 2) yOffset = current.Y.CompareTo(previous.Y);

                    current.MoveAbsolute(previous.X + xOffset, previous.Y + yOffset);
                    positionMap[j].Add(current.CurrentPosition());
                }
            }
        }
    }
}

Console.WriteLine(positionMap[1].Count);
Console.WriteLine(positionMap[9].Count);

internal class RopeSegment
{
    public int X;
    public int Y;

    public void MoveAbsolute(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.L:
                X -= 1;
                break;
            case Direction.R:
                X += 1;
                break;
            case Direction.D:
                Y -= 1;
                break;
            case Direction.U:
                Y += 1;
                break;
        }
    }

    public override string ToString()
    {
        return $"{X}, {Y}";
    }

    public Position CurrentPosition()
    {
        return new Position(X, Y);
    }
}

internal record Position(int X, int Y);

internal enum Direction
{
    R,
    L,
    U,
    D
}