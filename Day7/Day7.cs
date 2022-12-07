var fs = new FileSystem();

System.IO.File.ReadAllText("Input.txt")
    .Trim()
    .Split("\n")
    .ToList()
    .ForEach(line =>
    {
        if (line.StartsWith("$ cd"))
            fs.Cd(line.Split(" ").Last());
        else if (!line.StartsWith("$"))
            fs.AddFileContent(line);
    });

// 1
Console.WriteLine(
    fs.Directories
        .Select(d => d.Size())
        .Where(size => size <= 100_000)
        .Sum()
);

// 2
Console.WriteLine(
    fs.Directories
        .Select(d => d.Size())
        .Where(size => size >= -40_000_000 + fs.Root.Size())
        .Min()
);

class FileSystem
{
    public FileSystem()
    {
        Root = new Directory("/");
        Directories = new HashSet<Directory> { Root };
        _cwd = Root;
    }

    public readonly Directory Root;

    private Directory _cwd;
    public readonly HashSet<Directory> Directories;

    public void Cd(string directory)
    {
        if (directory == "..")
        {
            _cwd = Directories
                .ToList()
                .Find(dir =>
                    dir.Content.Contains(_cwd)
                )!;
        }
        else
        {
            var item = _cwd
                .Content
                .ToList()
                .Find(item => item is Directory d && d.Name == directory)!;
            if (item is Directory dir)
                _cwd = dir;
        }
    }

    public void AddFileContent(string content)
    {
        var parts = content.Split(" ");
        var name = parts.Last();

        if (long.TryParse(parts.First(), out var size))
        {
            _cwd.Add(new File(size));
        }
        else
        {
            var dir = new Directory(name);
            Directories.Add(dir);
            _cwd.Add(dir);
        }
    }
}

interface IDirContent
{
}

class File : IDirContent
{
    public File(long size)
    {
        Size = size;
    }

    public readonly long Size;
}

class Directory : IDirContent
{
    public Directory(string name)
    {
        Content = new HashSet<IDirContent>();
        Name = name;
    }

    public readonly HashSet<IDirContent> Content;
    public readonly string Name;

    public long Size()
    {
        return Content.Aggregate(0L, (total, item) =>
        {
            return item switch
            {
                File file => total + file.Size,
                Directory dir => total + dir.Size(),
                _ => total
            };
        });
    }

    public void Add(IDirContent item)
    {
        Content.Add(item);
    }
}