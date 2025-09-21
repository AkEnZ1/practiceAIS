using System;
using System.Collections.Generic;
using System.Linq;
using LogicAndModel;

namespace ConsoleApp
{
    class Program
    {
        static Logic logic = new Logic();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Система управления сотрудниками ===");
                Console.WriteLine("1. Добавить сотрудника");
                Console.WriteLine("2. Просмотреть всех сотрудников");
                Console.WriteLine("3. Найти сотрудника по индексу");
                Console.WriteLine("4. Изменить сотрудника");
                Console.WriteLine("5. Удалить сотрудника");
                Console.WriteLine("6. Рассчитать зарплату");
                Console.WriteLine("7. Добавить стаж");
                Console.WriteLine("8. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        ShowAllEmployees();
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
                        AddWorkExp();
                        break;
                    case "8":
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
            Console.Clear();
            Console.WriteLine("=== Добавление сотрудника ===");

            Console.Write("Введите имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите опыт работы (лет): ");
            int workExp = int.Parse(Console.ReadLine());

            Console.WriteLine("Выберите должность:");
            Console.WriteLine("1. Руководитель");
            Console.WriteLine("2. Менеджер");
            Console.WriteLine("3. Стажер");
            Console.Write("Ваш выбор: ");

            VacancyType vacancy;
            switch (Console.ReadLine())
            {
                case "1": vacancy = VacancyType.Head; break;
                case "2": vacancy = VacancyType.Manager; break;
                case "3": vacancy = VacancyType.Intern; break;
                default: vacancy = VacancyType.Intern; break;
            }

            logic.AddEmployee(name, workExp, vacancy);
            Console.WriteLine("Сотрудник добавлен!");
            Console.ReadLine();
        }

        static void ShowAllEmployees()
        {
            Console.Clear();
            Console.WriteLine("=== Все сотрудники ===");

            var employees = logic.GetEmployees();

            if (employees.Count == 0)
            {
                Console.WriteLine("Сотрудников нет!");
            }
            else
            {
                for (int i = 0; i < employees.Count; i++)
                {
                    Console.WriteLine($"{i}. {employees[i]}");
                }
            }

            Console.ReadLine();
        }

        static void FindEmployeeByIndex()
        {
            Console.Clear();
            Console.WriteLine("=== Поиск сотрудника по индексу ===");

            var employees = logic.GetEmployees();
            if (employees.Count == 0)
            {
                Console.WriteLine("Сотрудников нет!");
                Console.ReadLine();
                return;
            }

            Console.Write("Введите индекс: ");
            int index = int.Parse(Console.ReadLine());

            try
            {
                var employee = logic.GetEmployeeByIndex(index);
                Console.WriteLine($"Найден сотрудник: {employee}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.ReadLine();
        }

        static void UpdateEmployee()
        {
            Console.Clear();
            Console.WriteLine("=== Изменение сотрудника ===");

            var employees = logic.GetEmployees();
            if (employees.Count == 0)
            {
                Console.WriteLine("Сотрудников нет!");
                Console.ReadLine();
                return;
            }

            Console.Write("Введите индекс сотрудника для изменения: ");
            int index = int.Parse(Console.ReadLine());

            Console.Write("Введите новое имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите новый опыт работы (лет): ");
            int workExp = int.Parse(Console.ReadLine());

            Console.WriteLine("Выберите новую должность:");
            Console.WriteLine("1. Руководитель");
            Console.WriteLine("2. Менеджер");
            Console.WriteLine("3. Стажер");
            Console.Write("Ваш выбор: ");

            VacancyType vacancy;
            switch (Console.ReadLine())
            {
                case "1": vacancy = VacancyType.Head; break;
                case "2": vacancy = VacancyType.Manager; break;
                case "3": vacancy = VacancyType.Intern; break;
                default: vacancy = VacancyType.Intern; break;
            }

            if (logic.UpdateEmployee(index, name, vacancy, workExp))
            {
                Console.WriteLine("Сотрудник обновлен!");
            }
            else
            {
                Console.WriteLine("Неверный индекс!");
            }

            Console.ReadLine();
        }

        static void DeleteEmployee()
        {
            Console.Clear();
            Console.WriteLine("=== Удаление сотрудника ===");

            var employees = logic.GetEmployees();
            if (employees.Count == 0)
            {
                Console.WriteLine("Сотрудников нет!");
                Console.ReadLine();
                return;
            }

            Console.Write("Введите индекс сотрудника для удаления: ");
            int index = int.Parse(Console.ReadLine());

            logic.DeleteEmployee(index);
            Console.WriteLine("Сотрудник удален!");
            Console.ReadLine();
        }

        static void CalculateSalary()
        {
            Console.Clear();
            Console.WriteLine("=== Расчет зарплаты ===");

            var employees = logic.GetEmployees();
            if (employees.Count == 0)
            {
                Console.WriteLine("Сотрудников нет!");
                Console.ReadLine();
                return;
            }

            Console.Write("Введите индекс сотрудника: ");
            int index = int.Parse(Console.ReadLine());

            try
            {
                var employee = logic.GetEmployeeByIndex(index);
                double salary = logic.CalculateSalary(employee);
                Console.WriteLine($"Зарплата сотрудника {employee.Name}: {salary:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.ReadLine();
        }
        static void AddWorkExp()
        {
            Console.Clear();
            Console.WriteLine("Введите индекс сотрудника");
            int index = int.Parse(Console.ReadLine());
            var employees = logic.GetEmployees();
            if (index >= 0 && index <= employees.Count)
            {
                logic.AddWorkExp(employees[index]);
                Console.WriteLine($"Сотруднику {employees[index].Name} добавлен 1 год стажа"); Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Несуществующий индекс"); Console.ReadLine();
            }
        }
    }
}