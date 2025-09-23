using System;
using System.Collections.Generic;
using System.Linq;
using LogicAndModel;

namespace ConsoleApp
{
    class Program
    {
        static Logic logic = new Logic();
        
        /// <summary>
        /// Главная точка входа в приложение. Запускает меню управления сотрудниками.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
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

        /// <summary>
        /// Добавляет нового сотрудника через консольный интерфейс.
        /// Запрашивает у пользователя имя, опыт работы и должность.
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
            switch (Console.ReadLine())
            {
                case "1": vacancy = VacancyType.Head; break;
                case "2": vacancy = VacancyType.Manager; break;
                case "3": vacancy = VacancyType.Intern; break;
                default: 
                    Console.WriteLine("Неверный выбор должности! Установлена должность Стажера.");
                    vacancy = VacancyType.Intern; 
                    break;
            }

            logic.AddEmployee(name, workExp, vacancy);
            Console.WriteLine("Сотрудник добавлен!");
            Console.ReadLine();
        }

        /// <summary>
        /// Отображает список всех сотрудников в системе.
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
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Находит и отображает сотрудника по указанному индексу.
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
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Неверный формат индекса!");
                Console.ReadLine();
                return;
            }

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
        /// Обновляет информацию о сотруднике по указанному индексу.
        /// Запрашивает новые данные у пользователя.
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
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Неверный формат индекса!");
                Console.ReadLine();
                return;
            }

            Console.Write("Введите новое имя: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Имя не может быть пустым!");
                Console.ReadLine();
                return;
            }

            Console.Write("Введите новый опыт работы (лет): ");
            if (!int.TryParse(Console.ReadLine(), out int workExp) || workExp < 0)
            {
                Console.WriteLine("Опыт работы должен быть неотрицательным числом!");
                Console.ReadLine();
                return;
            }

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
                default: 
                    Console.WriteLine("Неверный выбор должности! Установлена должность Стажера.");
                    vacancy = VacancyType.Intern; 
                    break;
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
        /// Удаляет сотрудника из системы по указанному индексу.
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
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Неверный формат индекса!");
                Console.ReadLine();
                return;
            }

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
        /// Рассчитывает и отображает зарплату для выбранного сотрудника.
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
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Неверный формат индекса!");
                Console.ReadLine();
                return;
            }

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
        /// Добавляет один год стажа выбранному сотруднику.
        /// </summary>
        static void AddWorkExp()
        {
            Console.Clear();
            Console.WriteLine("=== Добавление стажа ===");
            
            var employees = logic.GetEmployees();
            if (employees.Count == 0)
            {
                Console.WriteLine("Сотрудников нет!");
                Console.ReadLine();
                return;
            }

            Console.Write("Введите индекс сотрудника: ");
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Неверный формат индекса!");
                Console.ReadLine();
                return;
            }

            try
            {
                var employee = logic.GetEmployeeByIndex(index);
                logic.AddWorkExp(employee);
                Console.WriteLine($"Сотруднику {employee.Name} добавлен 1 год стажа");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            
            Console.ReadLine();
        }
    }
}
