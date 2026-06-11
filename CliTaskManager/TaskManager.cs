using System.Text.Json;

namespace CliTaskManager;

public class TaskManager
{
    private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.json");
    private List<TodoTask> _tasks = new();

    public TaskManager()
    {
        LoadTasks();
    }

    public void AddTask(string description)
    {
        int newId = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;
        _tasks.Add(new TodoTask { Id = newId, Description = description });
        SaveTasks();
        Console.WriteLine($"Task added successfully! (ID: {newId})");
    }

    public void ListTasks()
    {
        if (_tasks.Count == 0)
        {
            Console.WriteLine(" No tasks found.");
            return;
        }

        Console.WriteLine("\nCurrent Tasks:");
        foreach (var task in _tasks)
        {
            string status = task.IsCompleted ? "[X]" : "[ ]";
            Console.WriteLine($"{task.Id}. {status} {task.Description}");
        }
    }

    public void CompleteTask(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.IsCompleted = true;
            SaveTasks();
            Console.WriteLine($"Task {id} marked as complete!");
        }
        else
        {
            Console.WriteLine($"Task with ID {id} not found.");
        }
    }

    public void DeleteTask(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            _tasks.Remove(task);
            SaveTasks();
            Console.WriteLine($" Task {id} deleted.");
        }
        else
        {
            Console.WriteLine($"Task with ID {id} not found.");
        }
    }

    private void LoadTasks()
    {
        if (File.Exists(_filePath))
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                _tasks = JsonSerializer.Deserialize<List<TodoTask>>(json) ?? new List<TodoTask>();
            }
            catch
            {
                _tasks = new List<TodoTask>();
            }
        }
    }

    private void SaveTasks()
    {
        string json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
