using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace zadacha_irina_i_karina
{
    public class TaskManager
    {
        private List<TaskItem> tasks = new List<TaskItem>();
        private string filePath;
        private string currentUser;

        public string CurrentUser => currentUser;

        public TaskManager() : this("default") { }

        public TaskManager(string username)
        {
            Login(username);
        }

        public void Login(string username)
        {
            if (!string.IsNullOrEmpty(currentUser))
            {
                SaveToFile();
            }

            currentUser = string.IsNullOrWhiteSpace(username) ? "default" : username.Trim();
            filePath = GetFilePathForUser(currentUser);
            tasks = new List<TaskItem>();
            LoadFromFile();
        }

        public void AddTask(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            tasks.Add(new TaskItem(name.Trim()));
            SaveToFile();
        }

        public List<TaskItem> GetAllTasks()
        {
            return tasks.ToList();
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
            SaveToFile();
        }

        public void SortByStatus()
        {
            tasks = tasks.OrderBy(t => t.IsCompleted).ToList();
            SaveToFile();
        }

        public List<TaskItem> SearchByName(string query)
        {
            if (string.IsNullOrEmpty(query))
                return GetAllTasks();

            return tasks
                .Where(t => !string.IsNullOrEmpty(t.Name) &&
                            t.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public List<TaskItem> SearchByStatus(bool isCompleted)
        {
            return tasks.Where(t => t.IsCompleted == isCompleted).ToList();
        }

        private void SaveToFile()
        {
            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var writer = new StreamWriter(filePath, false))
                {
                    foreach (var task in tasks)
                    {
                        var safeName = (task.Name ?? string.Empty).Replace("|", "¦");
                        writer.WriteLine($"{safeName}|{task.IsCompleted}");
                    }
                }
            }
            catch (Exception)
            {
                // silently ignore IO errors for now
            }
        }

        private void LoadFromFile()
        {
            tasks.Clear();

            try
            {
                if (!File.Exists(filePath))
                    return;

                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var parts = line.Split('|');
                    if (parts.Length < 2)
                        continue;

                    var namePart = parts[0].Replace("¦", "|");
                    bool isCompleted;
                    if (!bool.TryParse(parts[1], out isCompleted))
                        isCompleted = false;

                    var task = new TaskItem(namePart) { IsCompleted = isCompleted };
                    tasks.Add(task);
                }
            }
            catch (Exception)
            {
                // ignore load errors for now
            }
        }

        private static string GetFilePathForUser(string username)
        {
            var safe = SanitizeFileName(username);
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_files");
            var filename = safe + "_tasks.txt";
            return Path.Combine(folder, filename);
        }

        private static string SanitizeFileName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "default";

            var invalid = Path.GetInvalidFileNameChars();
            var chars = name.Select(c => invalid.Contains(c) ? '_' : c).ToArray();
            var result = new string(chars).Trim();
            return string.IsNullOrEmpty(result) ? "default" : result;
        }
    }
}
