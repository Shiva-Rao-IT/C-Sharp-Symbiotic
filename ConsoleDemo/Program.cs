/*
Console.WriteLine("Hello, World!");

Console.Write("What is your name :  ");
string name = Console.ReadLine() ?? string.Empty;
Console.WriteLine($"Your name is {name}");

Console.ReadLine(); */

using System;
using System.Collections.Generic;

namespace ConsoleDemo
{
    class ProjectTask
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        public ProjectTask(string title)
        {
            Title = title;
            IsCompleted = false;
        }

        public override string ToString()
        {
            string status = IsCompleted
            ? "\x1b[32m[DONE]\x1b[0m   "
            : "\x1b[31m[PENDING]\x1b[0m";
            return $"{status} - {Title}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<ProjectTask> taskList = new List<ProjectTask>();
            bool running = true;

            Console.WriteLine("=== C# Task Manager v1.0 ===");

            while (running)
            {
                Console.WriteLine("\nOptions: [1] Add Task | [2] View Tasks | [3] Complete Task | [4] Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter task description: ");
                        string desc = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(desc))
                        {
                            taskList.Add(new ProjectTask(desc));
                            Console.WriteLine("Task added successfully!");
                        }
                        break;

                    case "2":
                        Console.WriteLine("\n--- Current Tasks ---");
                        if (taskList.Count == 0) Console.WriteLine("No tasks found.");
                        for (int i = 0; i < taskList.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {taskList[i]}");
                        }
                        break;

                    case "3":
                        Console.Write("Enter task number to mark complete: ");
                        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= taskList.Count)
                        {
                            taskList[index - 1].IsCompleted = true;
                            Console.WriteLine("Task updated!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid task number.");
                        }
                        break;

                    case "4":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid selection. Try again.");
                        break;
                }
            }
        }
    }
}