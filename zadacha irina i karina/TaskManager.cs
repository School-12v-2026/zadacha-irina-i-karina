using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zadacha_irina_i_karina;

public class TaskManager
{
    private List<TaskItem> tasks = new List<TaskItem>();
    private string filePath = "tasks.txt";

    public TaskManager()
    {
        LoadFromFile();
    }

    public void AddTask(string name)
    {
        tasks.Add(new TaskItem(name));
        SaveToFile();
    }

    public List<TaskItem> GetAllTasks()
    {
        return tasks;
    }

    public void CompleteTask(int index)
    {
        if (index >= 0 && index < tasks.Count)
        {
            tasks[index].IsCompleted = true;
            SaveToFile();
        }
    }

    public void DeleteTask(int index)
    {
        if (index >= 0 && index < tasks.Count)
        {
            tasks.RemoveAt(index);
            SaveToFile();
        }
    }

    public void SortByName()
    {
        tasks = tasks.OrderBy(t => t.Name).ToList();
    }

    public void SortByStatus()
    {
        tasks = tasks.OrderBy(t => t.IsCompleted).ToList();
    }

    private void SaveToFile()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var task in tasks)
            {
                writer.WriteLine($"{task.Name}|{task.IsCompleted}");
            }
        }
    }

    private void LoadFromFile()
    {
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                TaskItem task = new TaskItem(parts[0]);
                task.IsCompleted = bool.Parse(parts[1]);
                tasks.Add(task);
            }
        }
    }
}
