using CliTaskManager;

TaskManager manager = new TaskManager();

if (args.Length == 0)
{
    PrintUsage();
    return;
}

string command = args[0].ToLower();

switch (command)
{
    case "list":
        manager.ListTasks();
        break;

    case "add":
        if (args.Length < 2 || string.IsNullOrWhiteSpace(args[1]))
        {
            Console.WriteLine("Error: Please provide a description.");
            return;
        }
        manager.AddTask(args[1]);
        break;

    case "complete":
        if (args.Length < 2 || !int.TryParse(args[1], out int compId))
        {
            Console.WriteLine("Error: Please provide a valid numerical ID.");
            return;
        }
        manager.CompleteTask(compId);
        break;

    case "delete":
        if (args.Length < 2 || !int.TryParse(args[1], out int delId))
        {
            Console.WriteLine("Error: Please provide a valid numerical ID.");
            return;
        }
        manager.DeleteTask(delId);
        break;

    default:
        Console.WriteLine($"Unkown command: '{command}'");
        PrintUsage();
        break;
}

void PrintUsage()
{
    Console.WriteLine("\n=================================");
    Console.WriteLine("      CLI Todo Manager       ");
    Console.WriteLine("=================================");
    Console.WriteLine("Usage:");
    Console.WriteLine("  dotnet run list");
    Console.WriteLine("  dotnet run add \"Your task here\"");
    Console.WriteLine("  dotnet run complete [id]");
    Console.WriteLine("  dotnet run delete [id]");
}
