using System;
using System.Collections.Generic;
using System.Threading;
namespace ConsoleApp1
{
    internal class Program
    {
        private static List<string> Tasks = new List<string>();

        static void Main(string[] args)
        {

            welcome();
            Console.WriteLine("\nWelcome to Our Task Manager");
            Console.Clear();
            Mainmenu();
   
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        static void welcome()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t*****************************************");
            Console.WriteLine("\t\t\t\t\t*                                       *");
            Console.WriteLine("\t\t\t\t\t*  📖✨Hello! Organize your tasks ✨📖  *");
            Console.WriteLine("\t\t\t\t\t*                                       *");
            Console.WriteLine("\t\t\t\t\t*****************************************");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\t\t\t\t\t\tWelcome to the Task Manager!");
            Console.ResetColor();
            Console.ReadKey();
            //Console.WriteLine("Please enter the number of tasks you want to manage.");
            //Console.Write("Number of tasks: ");
            //int Num = int.Parse(Console.ReadLine());
            //Console.ResetColor();
            //Console.WriteLine("Enter Your Tasks:");
            //for (int i = 0; i < Num; i++)
            //{
            //    Console.Write($"Task {i + 1}: ");
            //    string taskInput = Console.ReadLine();
            //    Tasks.Add(taskInput);
            //}
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        static void Mainmenu()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("1: Show Your Tasks");
            Console.WriteLine("2: Update Your Task");
            Console.WriteLine("3: Add Or Delete Task");
            Console.WriteLine("4: Exit");
            choice();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        static void choice()
        {
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    {
                        ShowTasks();
                        break;
                    }

                case 2:
                    {
                        UpdateTask();
                        break;
                    }
                case 3:
                    addOrRemoveTask();
                    break;
                case 4:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    Mainmenu();
                    break;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        static void ShowTasks()
        {
            Console.Clear();
            PrintHeader("Your Tasks");

            if (Tasks.Count == 0)
            {
                Console.WriteLine("❌ No tasks available.");
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
                Console.Clear();
                Mainmenu();
            }

            for (int i = 0; i < Tasks.Count; i++)
            {
                string[] parts = Tasks[i].Split('-');
                string title = parts[0].Trim();
                string status = parts.Length > 1 ? parts[1].Trim() : "❌ Not Started";
                string percentText = parts.Length > 2 ? parts[2].Trim().Replace("%", "") : "0";
                int percent = int.TryParse(percentText, out int p) ? p : 0;

                int filled = percent / 10;
                string bar = new string('▓', filled) + new string('░', 10 - filled);

                if (status.Contains("✅")) Console.ForegroundColor = ConsoleColor.Green;
                else if (status.Contains("⏳")) Console.ForegroundColor = ConsoleColor.Yellow;
                else Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($"📌 {i + 1}. {title}");
                Console.WriteLine($"    ├─ Status  : {status}");
                Console.WriteLine($"    └─ Progress: [{bar}] {percent}%\n");

                Console.ResetColor();
            }

            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
            Console.Clear();
            Mainmenu();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        static void UpdateTask()
        {
            Console.Clear();
            PrintHeader("Update Task");

            if (Tasks.Count == 0)
            {
                Console.WriteLine("❌ No tasks to edit.");
                Console.Clear();
                Mainmenu();
            }

            for (int i = 0; i < Tasks.Count; i++)
                Console.WriteLine($"{i + 1}. {Tasks[i]}");

            Console.Write("Enter the task number to update: ");
            if (!int.TryParse(Console.ReadLine(), out int taskNum) || taskNum < 1 || taskNum > Tasks.Count)
            {
                Console.WriteLine("❌ Invalid task number.");
                Console.Clear();
                UpdateTask();
            }
            Console.WriteLine("Choose new state for the task:");
            Console.WriteLine("1. ✅ Done");
            Console.WriteLine("2. ⏳ In Progress");
            Console.WriteLine("3. ❌ Not Started");
            Console.Write("Enter your choice: ");
            if (!int.TryParse(Console.ReadLine(), out int stateChoice) || stateChoice < 1 || stateChoice > 3)
            {
                Console.WriteLine("❌ Invalid state choice.");
                UpdateTask();
            }

            string newState = stateChoice == 1 ? "✅ Done" :
                              stateChoice == 2 ? "⏳ In Progress" : "❌ Not Started";

            int percent = 0;
            if (stateChoice != 3)
            {
                if (stateChoice == 1)
                {
                    percent = 100; // Done tasks are always 100%
                }
                else
                {
                    Console.WriteLine("Enter progress percent (0-100):");
                    bool inValidPercent = true;
                    while (inValidPercent)
                    {
                        if (!int.TryParse(Console.ReadLine(), out percent) || percent < 0 || percent > 100)
                        {
                            Console.WriteLine("❌ Invalid percent.");
                        }
                        else
                        {
                            inValidPercent = false;
                        }
                    }

                }
                Console.Write("Updating: ");
                Console.ForegroundColor = stateChoice == 1 ? ConsoleColor.Green : ConsoleColor.Yellow;
                for (int i = 0; i < percent / 10; i++)
                {
                    Console.Write("▓");
                    Thread.Sleep(80);
                }
                Console.ResetColor();
                Console.WriteLine($" {percent}%");
            }

            string originalTask = Tasks[taskNum - 1].Split('-')[0];
            Tasks[taskNum - 1] = $"{originalTask} - {newState} - {percent}%";
            Console.WriteLine("✅ Task updated successfully.");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
            Console.Clear();
            Mainmenu();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        static void PrintHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n" + new string('═', 50));
            Console.WriteLine($"🧠 {title}");
            Console.WriteLine(new string('═', 50));
            Console.ResetColor();
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------               
        static void addOrRemoveTask()
        {
            Console.Clear();
            Console.WriteLine("1. to add task\t2. to remove task ");
            string addOrRemoveInput = Console.ReadLine();
            int addOrRemove = 0;
            int.TryParse(addOrRemoveInput, out addOrRemove);
            if (addOrRemove == 1)
            {
                Console.WriteLine("Enter the task to add:");
                string newTask = Console.ReadLine();
                Tasks.Add(newTask);
                Console.WriteLine("Task added successfully!");
                Console.Clear();
                Mainmenu();
            }
            else if (addOrRemove == 2)
            {
                Console.Clear();
                Console.WriteLine("Your Tasks:");
                for (int i = 0; i < Tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {Tasks[i]}");
                }
                Console.WriteLine("\nEnter the task number to remove:");
                int removeIndex = int.Parse(Console.ReadLine()) - 1;
                if (removeIndex >= 0 && removeIndex < Tasks.Count)
                {
                    Tasks.RemoveAt(removeIndex);
                    Console.WriteLine("Task removed successfully!");
                    Console.Clear();
                    Mainmenu();
                }
                else
                {
                    bool isValidTaskeNum = true;
                    while (!isValidTaskeNum)
                    {
                        Console.WriteLine("Invalid task number. Please enter a valid task number:");
                        removeIndex = int.Parse(Console.ReadLine()) - 1;
                        if (removeIndex >= 0 && removeIndex < Tasks.Count)
                        {
                            isValidTaskeNum = true;
                            Tasks.RemoveAt(removeIndex);
                            Console.WriteLine("Task removed successfully!");
                            Console.WriteLine("Press any key to return to main menu...");
                            Console.ReadKey();
                            Console.Clear();
                            Mainmenu();
                        }
                    }    
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                Console.Clear();
                Mainmenu();
            }
            Mainmenu();
        }
    }
}
