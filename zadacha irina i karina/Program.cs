using System;
using System.Collections.Generic;
using zadacha_irina_i_karina;

class Program
{
    static void Main()
    {
        Console.Write("Потребителско име: ");
        string username = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(username))
            username = "default";

        TaskManager manager = new TaskManager(username);
        bool running = true;

        while (running)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--- TASK MANAGER (потребител: {0}) ---", manager.CurrentUser);
            Console.WriteLine("1. Добави задача");
            Console.WriteLine("2. Покажи всички задачи");
            Console.WriteLine("3. Маркирай като завършена");
            Console.WriteLine("4. Изтрий задача");
            Console.WriteLine("5. Сортирай по име");
            Console.WriteLine("6. Сортирай по статус");
            Console.WriteLine("7. Търсене по име");
            Console.WriteLine("8. Търсене по статус");
            Console.WriteLine("9. Смени потребител");
            Console.WriteLine("10. Изход");

            Console.ResetColor();
            Console.Write("Избор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Име на задача: ");
                    string name = Console.ReadLine();
                    manager.AddTask(name);
                    break;

                case "2":
                    ShowTasks(manager);
                    break;

                case "3":
                    Console.Write("Номер: ");
                    if (int.TryParse(Console.ReadLine(), out int completeIndex))
                    {
                        manager.CompleteTask(completeIndex - 1);
                    }
                    break;

                case "4":
                    Console.Write("Номер: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteIndex))
                    {
                        manager.DeleteTask(deleteIndex - 1);
                    }
                    break;

                case "5":
                    manager.SortByName();
                    break;

                case "6":
                    manager.SortByStatus();
                    break;

                case "7":
                    Console.Write("Търсене (име): ");
                    string query = Console.ReadLine();
                    var results = manager.SearchByName(query);
                    ShowTasks(manager, results);
                    break;

                case "8":
                    Console.WriteLine("1 = Завършени, 2 = Незавършени");
                    Console.Write("Избор: ");
                    string statusChoice = Console.ReadLine();
                    if (statusChoice == "1")
                    {
                        ShowTasks(manager, manager.SearchByStatus(true));
                    }
                    else if (statusChoice == "2")
                    {
                        ShowTasks(manager, manager.SearchByStatus(false));
                    }
                    break;

                case "9":
                    Console.Write("Ново потребителско име: ");
                    string newUser = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newUser))
                        newUser = "default";
                    manager.Login(newUser);
                    break;

                case "10":
                    running = false;
                    break;
            }
        }
    }

    static void ShowTasks(TaskManager manager)
    {
        var tasks = manager.GetAllTasks();
        ShowTasks(manager, tasks);
    }

    static void ShowTasks(TaskManager manager, List<TaskItem> tasks)
    {
        if (tasks == null || tasks.Count == 0)
        {
            Console.WriteLine("Няма задачи.");
            return;
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].IsCompleted)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"{i + 1}. {tasks[i].Name} - {(tasks[i].IsCompleted ? "Завършена" : "Незавършена")}");
        }

        Console.ResetColor();
    }
}
