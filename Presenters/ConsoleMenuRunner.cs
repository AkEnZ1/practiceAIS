using System;
using DomainModel;

namespace Presenters
{
    /// <summary>
    /// Запускатель консольного меню
    /// </summary>
    public static class ConsoleMenuRunner
    {
        public static void Run(ConsoleEmployeeView consoleView)
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
            Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ СОТРУДНИКАМИ ===");
            Console.WriteLine("1. Добавить сотрудника");
            Console.WriteLine("2. Показать всех сотрудников");
            Console.WriteLine("3. Найти по индексу");
            Console.WriteLine("4. Изменить сотрудника");
            Console.WriteLine("5. Удалить сотрудника");
            Console.WriteLine("6. Рассчитать зарплату");
            Console.WriteLine("7. Добавить стаж");
            Console.WriteLine("8. Статистика");
            Console.WriteLine("9. Фильтр по должности");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");
        }

        static void ShowAddEmployeeMenu(ConsoleEmployeeView consoleView)
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ СОТРУДНИКА ===");

            Console.Write("Введите имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите опыт работы (лет): ");
            if (!int.TryParse(Console.ReadLine(), out int workExp) || workExp < 0)
            {
                consoleView.ShowError("Опыт работы должен быть неотрицательным числом!");
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
                    consoleView.ShowError("Неверный выбор должности!");
                    return;
            }

            consoleView.InvokeAddEmployee(name, workExp, vacancy);
        }

        static void ShowFindByIndexMenu(ConsoleEmployeeView consoleView)
        {
            Console.Write("Введите индекс: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                consoleView.InvokeFindByIndex(index);
            }
            else
            {
                consoleView.ShowError("Неверный формат индекса!");
            }
        }

        static void ShowUpdateEmployeeMenu(ConsoleEmployeeView consoleView)
        {
            Console.Write("Введите индекс сотрудника для изменения: ");
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                consoleView.ShowError("Неверный формат индекса!");
                return;
            }

            Console.Write("Введите новое имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите новый опыт работы (лет): ");
            if (!int.TryParse(Console.ReadLine(), out int workExp))
            {
                consoleView.ShowError("Неверный формат опыта работы!");
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
                    consoleView.ShowError("Неверный выбор должности!");
                    return;
            }

            consoleView.InvokeUpdateEmployee(index, name, vacancy, workExp);
        }

        static void ShowDeleteEmployeeMenu(ConsoleEmployeeView consoleView)
        {
            Console.Write("Введите индекс сотрудника для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                consoleView.InvokeDeleteEmployee(index);
            }
            else
            {
                consoleView.ShowError("Неверный формат индекса!");
            }
        }

        static void ShowCalculateSalaryMenu(ConsoleEmployeeView consoleView)
        {
            Console.Write("Введите индекс сотрудника: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                consoleView.InvokeCalculateSalary(index);
            }
            else
            {
                consoleView.ShowError("Неверный формат индекса!");
            }
        }

        static void ShowAddWorkExpMenu(ConsoleEmployeeView consoleView)
        {
            Console.Write("Введите индекс сотрудника: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                consoleView.InvokeAddWorkExperience(index);
            }
            else
            {
                consoleView.ShowError("Неверный формат индекса!");
            }
        }

        static void ShowFilterMenu(ConsoleEmployeeView consoleView)
        {
            Console.WriteLine("Фильтр по должности:");
            Console.WriteLine("1. Руководители");
            Console.WriteLine("2. Менеджеры");
            Console.WriteLine("3. Стажеры");
            Console.Write("Ваш выбор: ");

            switch (Console.ReadLine())
            {
                case "1": consoleView.InvokeFilterByVacancy(VacancyType.Head); break;
                case "2": consoleView.InvokeFilterByVacancy(VacancyType.Manager); break;
                case "3": consoleView.InvokeFilterByVacancy(VacancyType.Intern); break;
                default: consoleView.ShowError("Неверный выбор!"); break;
            }
        }
    }
}
