var turns = File.ReadAllText("Input.txt")
    .Trim()
    .Split("\n")
    .Select(line => line.Split(" "))
    .Select(pair => new Turn(pair.First(), pair.Last()))
    .ToList();

// 1
Console.WriteLine(turns.Select(t => t.Result()).Sum());

// 2
Console.WriteLine(turns.Select(t => t.Move()).Sum());

class Turn
{
    public Turn(string opponent, string player)
    {
        _opponentValue = "ABC".IndexOf(opponent, StringComparison.Ordinal);
        _playerValue = "XYZ".IndexOf(player, StringComparison.Ordinal);
    }

    private readonly int _opponentValue;
    private readonly int _playerValue;

    public long Result()
    {
        // Magic equation
        var match = _playerValue - _opponentValue;
        var points = (match + 4) % 3 * 3;
        
        return _playerValue + 1 + points;
    }

    public long Move()
    {
        // Magic equation 2: Electric Boogaloo
        var move = (_opponentValue + _playerValue + 2) % 3;
        
        return 3 * _playerValue + move + 1;
    }
}
