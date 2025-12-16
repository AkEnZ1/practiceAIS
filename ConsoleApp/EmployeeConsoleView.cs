using DomainModel;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class EmployeeConsoleView : IEmployeeView
    {
        // События IEmployeeView
        public event Action<string, int, VacancyType> OnAddEmployee;
        public event Action<int, string, VacancyType, int> OnUpdateEmployee;
        public event Action<int> OnDeleteEmployee;
        public event Action<int> OnEmployeeSelected;
        public event Action<int> OnCalculateSalary;
        public event Action<int> OnAddWorkExperience;
        public event Action OnShowStatistics;
        public event Action<VacancyType> OnFilterByVacancy;
        public event Action OnShowAllEmployees;
        public event Action<int> OnFindByIndex;

        // Добавляем StartupEvent как у одногруппника
        public event Action StartupEvent;

        public void Run()
        {
            StartupEvent?.Invoke();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Показать всех сотрудников");
                Console.WriteLine("2. Добавить сотрудника");
                Console.WriteLine("3. Найти по индексу");
                Console.WriteLine("4. Редактировать сотрудника");
                Console.WriteLine("5. Удалить сотрудника");
                Console.WriteLine("6. Рассчитать зарплату");
                Console.WriteLine("7. Добавить стаж");
                Console.WriteLine("8. Статистика");
                Console.WriteLine("9. Фильтр по должности");
                Console.WriteLine("0. Выход");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        OnShowAllEmployees?.Invoke();
                        break;

                    case ConsoleKey.D2:
                        OnAddEmployee?.Invoke(
                            ReadString("Имя:"),
                            ReadInt("Стаж (лет):", 0, 100),
                            ReadVacancy()
                        );
                        break;

                    case ConsoleKey.D3:
                        OnFindByIndex?.Invoke(ReadInt("Индекс:", 0, int.MaxValue));
                        break;

                    case ConsoleKey.D4:
                        OnUpdateEmployee?.Invoke(
                            ReadInt("Индекс сотрудника:", 0, int.MaxValue),
                            ReadString("Новое имя:"),
                            ReadVacancy(),
                            ReadInt("Новый стаж (лет):", 0, 100)
                        );
                        break;

                    case ConsoleKey.D5:
                        OnDeleteEmployee?.Invoke(ReadInt("Индекс сотрудника:", 0, int.MaxValue));
                        break;

                    case ConsoleKey.D6:
                        OnCalculateSalary?.Invoke(ReadInt("Индекс сотрудника:", 0, int.MaxValue));
                        break;

                    case ConsoleKey.D7:
                        OnAddWorkExperience?.Invoke(ReadInt("Индекс сотрудника:", 0, int.MaxValue));
                        break;

                    case ConsoleKey.D8:
                        OnShowStatistics?.Invoke();
                        break;

                    case ConsoleKey.D9:
                        Console.WriteLine("Должность: 1-Руководитель, 2-Менеджер, 3-Стажер");
                        OnFilterByVacancy?.Invoke(ReadVacancy());
                        break;

                    case ConsoleKey.D0:
                        return;
                }
            }
        }

        // Реализация IEmployeeView
        public void RefreshEmployeeList(List<Employee> employees)
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК СОТРУДНИКОВ ===");

            if (employees.Count == 0)
            {
                Console.WriteLine("Сотрудников нет");
                return;
            }

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee}");
            }

            Console.WriteLine($"\nВсего: {employees.Count} сотрудников");
            Pause();
        }

        public void ShowEmployeeDetails(Employee employee)
        {
            Console.WriteLine($"\nДЕТАЛИ: {employee}");
            Pause();
        }

        public void ClearEmployeeDetails()
        {
            // Не используется в консоли
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine($"\nℹ️ {message}");
            Pause();
        }

        public void ShowError(string error)
        {
            Console.WriteLine($"\n❌ ОШИБКА: {error}");
            Pause();
        }

        // Вспомогательные методы как у одногруппника
        public string ReadString(string prompt)
        {
            Console.Write(prompt + " ");
            return Console.ReadLine() ?? string.Empty;
        }

        public int ReadInt(string prompt, int min, int max)
        {
            int value;
            do
            {
                Console.Write(prompt + " ");
            }
            while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max);

            return value;
        }

        private VacancyType ReadVacancy()
        {
            Console.Write("Должность (1-Руководитель, 2-Менеджер, 3-Стажер): ");

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("1 (Руководитель)");
                        return VacancyType.Head;
                    case ConsoleKey.D2:
                        Console.WriteLine("2 (Менеджер)");
                        return VacancyType.Manager;
                    case ConsoleKey.D3:
                        Console.WriteLine("3 (Стажер)");
                        return VacancyType.Intern;
                }
            }
        }

        private static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}