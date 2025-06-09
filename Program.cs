using System;
using System.Collections.Generic;
namespace ConsoleApp1
{
    internal class Program
    {
        private static List<string> Tasks = new List<string> ();
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Number of Tasks:");
            int Num = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Your Tasks:");
            for (int i = 0; i < Num; i++)
            {
                Console.Write($"Task {i + 1}: ");
                string taskInput = Console.ReadLine();
                Tasks.Add(taskInput);
            }
        Mainmenu:
            Console.WriteLine("\nWelcome to Our Task Manager");
            Console.WriteLine("Main Menu");
            Console.WriteLine("1: Show Your Tasks");
            Console.WriteLine("2: Update Your Task");
            Console.WriteLine("3: Add Or Delete Task");
            Console.WriteLine("4: Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                ShowTaske:
                    Console.WriteLine("\nYour Tasks:");
                    for (int i = 0; i < Tasks.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {Tasks[i]}");
                    }
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey();
                    Console.Clear();
                    goto Mainmenu;
                case 2:

                    {
                        if (Tasks.Count == 0)
                        {
                            Console.WriteLine("No tasks to edit.");
                            break;
                        }

                        Console.WriteLine("Current Tasks:");
                        for (int i = 0; i < Tasks.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {Tasks[i]}");
                        }

                        Console.Write("\nEnter the number of the task to update: ");
                        int TaskNum = int.Parse(Console.ReadLine());

                        if (TaskNum < 1 || TaskNum > Tasks.Count)
                        {
                            Console.WriteLine("Invalid task number.");
                            break;
                        }

                        Console.WriteLine("\nChoose new state for the task:");
                        Console.WriteLine("1. ✅ Done");
                        Console.WriteLine("2. ⏳ In Progress");
                        Console.WriteLine("3. ❌ Not Started");
                        Console.Write("Enter your choice: ");
                        int stateChoice = int.Parse(Console.ReadLine());

                        string newState = "";

                        switch (stateChoice)
                        {
                            case 1:
                                newState = "✅ Done";
                                break;
                            case 2:
                                newState = "⏳ In Progress";
                                break;
                            case 3:
                                newState = "❌ Not Started";
                                break;
                            default:
                                newState = null;
                                break;
                        }

                        if (newState == null)
                        {
                            Console.WriteLine("Invalid state choice.");
                            break;
                        }


                        string originalTask = Tasks[TaskNum - 1].Split('-')[0];
                        Tasks[TaskNum - 1] = $"{originalTask} - {newState}";

                        Console.WriteLine("✅ Task updated successfully.");

                    }
                    

                    break;
                
                case 3:
                addOrRemove:
                    Console.Clear();
                        Console.WriteLine("1. to add task\n2. to remove task ");
                        string addOrRemoveInput = Console.ReadLine();
                        int addOrRemove = 0;
                        int.TryParse(addOrRemoveInput, out addOrRemove);
                        if (addOrRemove == 1)
                        {
                            Console.WriteLine("Enter the task to add:");
                            string newTask = Console.ReadLine();
                            Tasks.Add(newTask);
                            Console.WriteLine("Task added successfully!");
                        goto Mainmenu;
                        }
                        else if (addOrRemove == 2)
                        {
                        Remove:
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
                                goto Mainmenu;
                            }
                            else
                            {
                                Console.WriteLine("Invalid task number.");
                                goto Remove;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                            goto addOrRemove;

                        }
                case 4:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
