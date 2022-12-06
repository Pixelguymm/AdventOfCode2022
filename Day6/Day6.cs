var chars = File.ReadAllText("Input.txt").Trim().ToList();

// 1
Console.WriteLine(chars.FindMark(4));

// 2
Console.WriteLine(chars.FindMark(14));

static class Extensions
{
    public static int FindMark(this List<char> characters, int length)
    {
        for (var i = 0; i <= characters.Count; i++)
        {
            if (characters.GetRange(i, length).Distinct().Count() == length)
            {
                return i + length;
            }
        }

        return 0;
    }
}