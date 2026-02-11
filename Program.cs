using System;

class Program
{
    static void Main()
    {
        TaskManager manager = new TaskManager();
        bool running = true;

        while (running)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--- TASK MANAGER ---");
            Console.WriteLine("1. Добави задача");
            Console.WriteLine("2. Покажи всички задачи");
            Console.WriteLine("3. Маркирай като завършена");
            Console.WriteLine("4. Изтрий задача");
            Console.WriteLine("5. Сортирай по име");
            Console.WriteLine("6. Сортирай по статус");
            Console.WriteLine("7. Изход");

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
                    int completeIndex = int.Parse(Console.ReadLine()) - 1;
                    manager.CompleteTask(completeIndex);
                    break;

                case "4":
                    Console.Write("Номер: ");
                    int deleteIndex = int.Parse(Console.ReadLine()) - 1;
                    manager.DeleteTask(deleteIndex);
                    break;

                case "5":
                    manager.SortByName();
                    break;

                case "6":
                    manager.SortByStatus();
                    break;

                case "7":
                    running = false;
                    break;
            }
        }
    }

    static void ShowTasks(TaskManager manager)
    {
        var tasks = manager.GetAllTasks();

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
