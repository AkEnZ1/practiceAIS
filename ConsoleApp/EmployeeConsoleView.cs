using DomainModel;
using Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    /// <summary>
    /// Консольное представление (View) для работы с сотрудниками.
    /// Реализует интерфейс IEmployeeView и предоставляет текстовый интерфейс для взаимодействия с пользователем.
    /// </summary>
    /// <remarks>
    /// Класс обрабатывает ввод пользователя через консоль, инициирует соответствующие события
    /// и отображает данные о сотрудниках, полученные от презентера или контроллера.
    /// </remarks>
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

        /// <summary>
        /// Событие, инициируемое при запуске приложения для первоначальной загрузки данных.
        /// </summary>
        public event Action StartupEvent;

        /// <summary>
        /// Запускает главный цикл консольного приложения.
        /// </summary>
        /// <remarks>
        /// Отображает меню, обрабатывает выбор пользователя и вызывает соответствующие события.
        /// Цикл продолжается до выбора пользователем пункта "Выход".
        /// </remarks>
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
                            ReadVacancy("Должность:")
                        );
                        break;

                    case ConsoleKey.D3:
                        OnFindByIndex?.Invoke(ReadInt("Индекс:", 0, int.MaxValue));
                        break;

                    case ConsoleKey.D4:
                        OnUpdateEmployee?.Invoke(
                            ReadInt("Индекс сотрудника:", 0, int.MaxValue),
                            ReadString("Новое имя:"),
                            ReadVacancy("Должность:"),
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
                        OnFilterByVacancy?.Invoke(ReadVacancy("Фильтр по должности:"));
                        break;

                    case ConsoleKey.D0:
                        return;
                }
            }
        }

        /// <summary>
        /// Обновляет список сотрудников в консольном выводе.
        /// </summary>
        /// <param name="employees">Список сотрудников для отображения.</param>
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

        /// <summary>
        /// Отображает подробную информацию о выбранном сотруднике.
        /// </summary>
        /// <param name="employee">Сотрудник, детали которого нужно показать.</param>
        public void ShowEmployeeDetails(Employee employee)
        {
            Console.WriteLine($"\nДЕТАЛИ: {employee}");
            Pause();
        }

        /// <summary>
        /// Очищает область с деталями сотрудника. В консольной реализации не используется.
        /// </summary>
        public void ClearEmployeeDetails()
        {
            // Не используется в консоли
        }

        /// <summary>
        /// Отображает информационное сообщение пользователю.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public void ShowMessage(string message)
        {
            Console.WriteLine($"\nℹ️ {message}");
            Pause();
        }

        /// <summary>
        /// Отображает сообщение об ошибке.
        /// </summary>
        /// <param name="error">Текст ошибки.</param>
        public void ShowError(string error)
        {
            Console.WriteLine($"\n❌ ОШИБКА: {error}");
            Pause();
        }

        /// <summary>
        /// Считывает строковое значение с консоли с выводом приглашения.
        /// </summary>
        /// <param name="prompt">Приглашение для ввода.</param>
        /// <returns>Введённая пользователем строка.</returns>
        public string ReadString(string prompt)
        {
            Console.Write(prompt + " ");
            return Console.ReadLine() ?? string.Empty;
        }

        /// <summary>
        /// Считывает целое число в заданном диапазоне с консоли.
        /// </summary>
        /// <param name="prompt">Приглашение для ввода.</param>
        /// <param name="min">Минимально допустимое значение.</param>
        /// <param name="max">Максимально допустимое значение.</param>
        /// <returns>Введённое пользователем целое число.</returns>
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

        /// <summary>
        /// Считывает с консоли выбор должности (VacancyType) из доступных вариантов.
        /// </summary>
        /// <param name="prompt">Приглашение для выбора.</param>
        /// <returns>Выбранный тип вакансии.</returns>
        public VacancyType ReadVacancy(string prompt)
        {
            Console.WriteLine(prompt);
            Console.WriteLine("1. Руководитель");
            Console.WriteLine("2. Менеджер");
            Console.WriteLine("3. Стажер");
            Console.Write("Выберите (1-3): ");

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

        /// <summary>
        /// Приостанавливает выполнение программы до нажатия любой клавиши.
        /// </summary>
        private static void Pause()
        {
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
