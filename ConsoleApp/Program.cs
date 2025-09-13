using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BusinessLogic;
using System.Net.Http.Headers;

namespace ConsoleApp
{
    class Program
    {
        Logic logic = new Logic();
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("=== Система управления сотрудниками ===");
                Console.WriteLine("1. Добавить сотрудника");
                Console.WriteLine("2. Просмотреть всех сотрудников");
                Console.WriteLine("3. Найти сотрудника по индексу");
                Console.WriteLine("4. Изменить сотрудника");
                Console.WriteLine("5. Удалить сотрудника");
                Console.WriteLine("6. Рассчитать зарплату");
                Console.WriteLine("7. Выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        GetEmployees();
                        break;
                    case "3":
                        FindEmployeeByIndex();
                        break;
                    case "4":
                        UpdateEmployee();
                        break;
                    case "5":
                        DeleteEmployee();
                        break;
                    case "6":
                        CalculateSalary();
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        Console.ReadLine();
                        break;
                }
            }
        }
        static void AddEmployee()
        {

        }
        static void ShowAllEmployees()
        {
            Console.Clear();
            Console.WriteLine("=== Все сотрудники ===");

            List<Employee> employees = logic.GetEmployees();

            if (employees.Count == 0)
            {
                Console.WriteLine("Сотрудников нет!");
            }
            else
            {
                for (int i = 0; i < employees.Count; i++)
                {
                    Console.WriteLine($"{i}. {employees[i].Name} - {employees[i].Vacancy} ({employees[i].WorkExp} лет опыта)");
                }
            }

            Console.ReadKey();
        }
    }
}
