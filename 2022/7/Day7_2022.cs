namespace AdventOfCode;

internal class Day7_2022 : ISolution
{
    public async Task<string> SolvePartOne(string inputFile)
    {
        var input = File.ReadLines(inputFile);

        var rootDirectory = CreateFileSystem(input);

        return SumLessThan100000(rootDirectory).ToString();
    }

    private static int SumLessThan100000(DirectoryNode directory) => (directory.Size > 100000 ? 0 : directory.Size) +
            directory.Children.OfType<DirectoryNode>().Sum(SumLessThan100000);

    public async Task<string> SolvePartTwo(string inputFile)
    {
        var input = File.ReadLines(inputFile);

        var rootDirectory = CreateFileSystem(input);

        var spaceNeeded = rootDirectory.Size - 40000000;

        return SmallestDirectoryToFree(rootDirectory, spaceNeeded).ToString();
    }

    private int SmallestDirectoryToFree(DirectoryNode directory, int spaceNeeded)
    {
        var childDirectories = directory.Children.OfType<DirectoryNode>();
        return Math.Min(directory.Size >= spaceNeeded ? directory.Size : int.MaxValue,
            childDirectories.Any() ? childDirectories.Min(child => SmallestDirectoryToFree(child, spaceNeeded)) : int.MaxValue);
    }

    private static DirectoryNode CreateFileSystem(IEnumerable<string> input)
    {
        var lines = input.GetEnumerator();

        lines.MoveNext();
        lines.MoveNext();

        var rootDirectory = new DirectoryNode("/", null);
        var currentDirectory = rootDirectory;
        
        while (lines.Current != null)
        {
            outerLoop: if (lines.Current[0] != '$') throw new Exception("Only commands should appear here!");

            switch(lines.Current[2..4])
            {
                case "cd":
                    var path = lines.Current[5..];
                    currentDirectory = path switch
                    {
                        "/" => rootDirectory,
                        ".." => currentDirectory!.Parent,
                        _ => currentDirectory!.GetChild(path) as DirectoryNode
                    };
                    lines.MoveNext();
                    break;

                case "ls":
                    while (lines.MoveNext())
                    {
                        var sections = lines.Current.Split(" ");
                        switch (sections[0])
                        {
                            case "$":
                                goto outerLoop;
                            case "dir":
                                currentDirectory!.AddChild(new DirectoryNode(sections[1], currentDirectory));
                                break;
                            default:
                                currentDirectory!.AddChild(new FileNode(sections[1], currentDirectory, int.Parse(sections[0])));
                                break;
                        }
                    }
                    break;

                default:
                    throw new Exception($"Invalid command encountered: {lines.Current}");
            }
        }

        return rootDirectory;
    }
}