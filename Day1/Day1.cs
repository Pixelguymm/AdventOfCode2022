var groups = File.ReadAllText("Input.txt")
    .Trim()
    .Split("\n\n")
    .Select(x =>
        x.Split("\n").Select(int.Parse)
    );

var calories = groups.Select(g => g.Sum()).ToList();

// 1
Console.WriteLine(calories.Max());

// 2
Console.WriteLine(calories.OrderDescending().Take(3).Sum());
