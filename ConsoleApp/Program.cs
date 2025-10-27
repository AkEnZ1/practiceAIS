using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using BusinessLogic;
using DataAccessLayer;
using Ninject;

namespace ConsoleApp
{
    /// <summary>
    /// Консольное приложение для управления сотрудниками
    /// </summary>
    /// <remarks>
    /// Реализует пользовательский интерфейс для работы с системой управления сотрудниками.
    /// Использует Dependency Injection через Ninject для управления зависимостями.
    /// </remarks>
    class Program
    {
        /// <summary>
        /// Экземпляр бизнес-логики для работы с сотрудниками
        /// </summary>
        static Logic logic;

        /// <summary>
        /// Точка входа в приложение
        /// </summary>
        /// <param name="args">Аргументы командной строки</param>
        /// <remarks>
        /// Инициализирует DI-контейнер, настраивает зависимости и запускает главное меню приложения.
        /// </remarks>
        static void Main(string[] args)
        {
            // Создаем IoC контейнер и настраиваем зависимости
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());

            // Получаем экземпляр Logic через DI контейнер
            logic = ninjectKernel.Get<Logic>();

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
        /// <remarks>
        /// Запрашивает у пользователя данные сотрудника: имя, опыт работы и должность.
        /// Валидирует введенные данные перед сохранением.
        /// </remarks>
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
        /// Отображает список всех сотрудников в системе
        /// </summary>
        /// <remarks>
        /// Показывает таблицу с ID, именами, должностями и опытом работы всех сотрудников.
        /// Также отображает общее количество сотрудников и список их ID.
        /// </remarks>
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

                Console.WriteLine($"\nВсего сотрудников в базе: {employees.Count}");
                var ids = employees.Select(e => e.ID).ToList();
                Console.WriteLine($"ID сотрудников: {string.Join(", ", ids)}");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Находит сотрудника по его индексу в списке
        /// </summary>
        /// <remarks>
        /// Запрашивает у пользователя индекс сотрудника и отображает его данные.
        /// Проверяет корректность введенного индекса и наличие сотрудников в системе.
        /// </remarks>
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
        /// Обновляет данные существующего сотрудника
        /// </summary>
        /// <remarks>
        /// Позволяет изменить имя, опыт работы и должность выбранного сотрудника.
        /// Проверяет корректность введенных данных перед обновлением.
        /// </remarks>
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

            Console.Write("Введите новый опыт работы (лет): ");
            if (!int.TryParse(Console.ReadLine(), out int workExp))
            {
                Console.WriteLine("Неверный формат опыта работы!");
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
                    Console.WriteLine("Неверный выбор должности!");
                    Console.ReadLine();
                    return;
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
        /// <remarks>
        /// Удаляет сотрудника по указанному индексу после подтверждения пользователя.
        /// Проверяет наличие сотрудников в системе и корректность индекса.
        /// </remarks>
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
        /// Рассчитывает зарплату для выбранного сотрудника
        /// </summary>
        /// <remarks>
        /// Расчет зарплаты основан на опыте работы и должности сотрудника.
        /// Формула: опыт_работы × коэффициент_должности × 10000
        /// </remarks>
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
        /// Добавляет один год стажа выбранному сотруднику
        /// </summary>
        /// <remarks>
        /// Увеличивает опыт работы сотрудника на 1 год и сохраняет изменения в базе данных.
        /// Показывает предыдущее и новое значение опыта работы.
        /// </remarks>
        static void AddWorkExp()
        {
            Console.Clear();
            Console.WriteLine("Введите индекс сотрудника");
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                Console.WriteLine("Неверный формат индекса!");
                Console.ReadLine();
                return;
            }

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