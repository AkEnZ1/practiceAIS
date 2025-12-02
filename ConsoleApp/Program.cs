using System;
using Presenters;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запуск через единую точку входа
            ApplicationController.RunConsoleApplication();
        }

        // Делаем метод публичным и статическим для вызова из ApplicationController
        public static void RunConsoleMenu(ConsoleEmployeeView consoleView)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                ShowMainMenu();

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowAddEmployeeMenu(consoleView);
                        break;
                    case "2":
                        consoleView.InvokeShowAllEmployees();
                        break;
                    case "3":
                        ShowFindByIndexMenu(consoleView);
                        break;
                    case "4":
                        ShowUpdateEmployeeMenu(consoleView);
                        break;
                    case "5":
                        ShowDeleteEmployeeMenu(consoleView);
                        break;
                    case "6":
                        ShowCalculateSalaryMenu(consoleView);
                        break;
                    case "7":
                        ShowAddWorkExpMenu(consoleView);
                        break;
                    case "8":
                        consoleView.InvokeShowStatistics();
                        break;
                    case "9":
                        ShowFilterMenu(consoleView);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        consoleView.ShowError("Неверный выбор!");
                        break;
                }
            }
        }

        static void ShowMainMenu()
        {
            // ... код меню остается без изменений ...
        }

        static void ShowAddEmployeeMenu(ConsoleEmployeeView consoleView)
        {
            // ... код меню остается без изменений ...
        }

        // ... остальные методы меню с добавлением параметра ConsoleEmployeeView ...
    }
}
