using System;
using System.Collections.Generic;
using System.Linq;
using LogicAndModel;
using DataAccessLayer;

namespace ConsoleApp
{
    /// <summary>
    /// Главный класс консольного приложения для управления сотрудниками
    /// </summary>
    class Program
    {
        static Logic logic;

        /// <summary>
        /// Точка входа в приложение
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        static void Main(string[] args)
        {
            IRepository<Employee> repository = new DapperRepository();
            logic = new Logic(repository);

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

        /// <summary>
        /// Добавляет нового сотрудника в систему
        /// </summary>
        static void AddEmployee()
        {
            Console.Clear();
            Console.WriteLine("=== Добавление сотрудника ===");

            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Имя не может быть пустым!");
                Console.ReadLine();
                return;
            }

            int workExp;
            Console.Write("Введите опыт работы (лет): ");
            if (!int.TryParse(Console.ReadLine(), out workExp) || workExp < 0)
            {
                Console.WriteLine("Опыт работы должен быть неотрицательным числом!");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Выберите должность:");
            Console.WriteLine("1. Руководитель");
            Console.WriteLine("2. Менеджер");
            Console.WriteLine("3. Стажер");
            Console.Write("Ваш выбор: ");

            VacancyType vacancy;
            string vacancyChoice = Console.ReadLine();

            switch (vacancyChoice)
            {
                case "1":
                    vacancy = VacancyType.Head;
                    break;
                case "2":
                    vacancy = VacancyType.Manager;
                    break;
                case "3":
                    vacancy = VacancyType.Intern;
                    break;
                default:
                    Console.WriteLine("Неверный выбор должности! Выберите 1, 2 или 3.");
                    Console.ReadLine();
                    return;
            }

            logic.AddEmployee(name, workExp, vacancy);
            Console.WriteLine("Сотрудник добавлен!");
            Console.ReadLine();
        }

        /// <summary>
        /// Отображает список всех сотрудников
        /// </summary>
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

                // Дополнительная информация о БД
                Console.WriteLine($"\nВсего сотрудников в базе: {employees.Count}");

                // Проверка ID сотрудников (должны быть уникальными)
                var ids = employees.Select(e => e.ID).ToList();
                Console.WriteLine($"ID сотрудников: {string.Join(", ", ids)}");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Находит сотрудника по индексу в списке
        /// </summary>
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

        /// <summary>
        /// Обновляет данные существующего сотрудника
        /// </summary>
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

            try
            {
                if (logic.UpdateEmployee(index, name, vacancy, workExp))
                {
                    Console.WriteLine("Сотрудник обновлен!");
                }
                else
                {
                    Console.WriteLine("Неверный индекс!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Удаляет сотрудника из системы
        /// </summary>
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

            try
            {
                logic.DeleteEmployee(index);
                Console.WriteLine("Сотрудник удален!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Рассчитывает зарплату для выбранного сотрудника
        /// </summary>
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

        /// <summary>
        /// Добавляет один год стажа выбранному сотруднику
        /// </summary>
        static void AddWorkExp()
        {
            Console.Clear();
            Console.WriteLine("Введите индекс сотрудника");
            int index = int.Parse(Console.ReadLine());

            try
            {
                var employee = logic.GetEmployeeByIndex(index);
                int oldExp = employee.WorkExp;
                logic.AddWorkExp(employee);
                Console.WriteLine($"Сотруднику {employee.Name} добавлен 1 год стажа");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.ReadLine();
            }
        }
    }
}