using System;
using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.Interfaces;
using DomainModel;
using Ninject;
using Presenters;
using Shared.Interfaces;

namespace ConsoleApp
{
    public class ConsoleEmployeeView : IEmployeeView
    {
        private EmployeePresenter _presenter;
        private List<Employee> _currentEmployees = new List<Employee>();

        public ConsoleEmployeeView(ILogic logic)
        {
            _presenter = new EmployeePresenter(
                this,
                logic.EmployeeService,
                logic.SalaryCalculator,
                logic.StatisticsService
            );
        }
        public void RefreshEmployeeList(List<Employee> employees)
        {
            _currentEmployees = employees;
            Console.WriteLine("\n=== СПИСОК СОТРУДНИКОВ ===");
            if (employees.Count == 0)
            {
                Console.WriteLine("Сотрудников нет!");
                return;
            }

            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine($"{i}. {employees[i]}");
            }
            Console.WriteLine($"Всего: {employees.Count} сотрудников");
        }

        public void ShowEmployeeDetails(Employee employee)
        {
            Console.WriteLine($"\nДЕТАЛИ: {employee}");
        }

        public void ClearEmployeeDetails()
        {
            // Не используется в консоли
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine($"\nℹ️  {message}");
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        public void ShowError(string error)
        {
            Console.WriteLine($"\n❌ ОШИБКА: {error}");
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

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

        // Методы для вызова событий из консоли
        public void InvokeAddEmployee(string name, int workExp, VacancyType vacancy)
        {
            OnAddEmployee?.Invoke(name, workExp, vacancy);
        }

        public void InvokeUpdateEmployee(int index, string name, VacancyType vacancy, int workExp)
        {
            OnUpdateEmployee?.Invoke(index, name, vacancy, workExp);
        }

        public void InvokeDeleteEmployee(int index)
        {
            OnDeleteEmployee?.Invoke(index);
        }

        public void InvokeCalculateSalary(int index)
        {
            OnCalculateSalary?.Invoke(index);
        }

        public void InvokeAddWorkExperience(int index)
        {
            OnAddWorkExperience?.Invoke(index);
        }

        public void InvokeShowStatistics()
        {
            OnShowStatistics?.Invoke();
        }

        public void InvokeFilterByVacancy(VacancyType vacancy)
        {
            OnFilterByVacancy?.Invoke(vacancy);
        }

        public void InvokeShowAllEmployees()
        {
            OnShowAllEmployees?.Invoke();
        }

        public void InvokeFindByIndex(int index)
        {
            OnFindByIndex?.Invoke(index);
        }
    }

    class Program
    {
        static ConsoleEmployeeView _consoleView;

        static void Main(string[] args)
        {
            // Инициализация DI
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            var logic = ninjectKernel.Get<ILogic>();

            // Создаем Console View
            _consoleView = new ConsoleEmployeeView(logic);

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                ShowMainMenu();

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowAddEmployeeMenu();
                        break;
                    case "2":
                        _consoleView.InvokeShowAllEmployees();
                        break;
                    case "3":
                        ShowFindByIndexMenu();
                        break;
                    case "4":
                        ShowUpdateEmployeeMenu();
                        break;
                    case "5":
                        ShowDeleteEmployeeMenu();
                        break;
                    case "6":
                        ShowCalculateSalaryMenu();
                        break;
                    case "7":
                        ShowAddWorkExpMenu();
                        break;
                    case "8":
                        _consoleView.InvokeShowStatistics();
                        break;
                    case "9":
                        ShowFilterMenu();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        _consoleView.ShowError("Неверный выбор!");
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

        static void ShowAddEmployeeMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ СОТРУДНИКА ===");

            Console.Write("Введите имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите опыт работы (лет): ");
            if (!int.TryParse(Console.ReadLine(), out int workExp) || workExp < 0)
            {
                _consoleView.ShowError("Опыт работы должен быть неотрицательным числом!");
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
                    _consoleView.ShowError("Неверный выбор должности!");
                    return;
            }

            _consoleView.InvokeAddEmployee(name, workExp, vacancy);
        }

        static void ShowFindByIndexMenu()
        {
            Console.Write("Введите индекс: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                _consoleView.InvokeFindByIndex(index);
            }
            else
            {
                _consoleView.ShowError("Неверный формат индекса!");
            }
        }

        static void ShowUpdateEmployeeMenu()
        {
            Console.Write("Введите индекс сотрудника для изменения: ");
            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                _consoleView.ShowError("Неверный формат индекса!");
                return;
            }

            Console.Write("Введите новое имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите новый опыт работы (лет): ");
            if (!int.TryParse(Console.ReadLine(), out int workExp))
            {
                _consoleView.ShowError("Неверный формат опыта работы!");
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
                    _consoleView.ShowError("Неверный выбор должности!");
                    return;
            }

            _consoleView.InvokeUpdateEmployee(index, name, vacancy, workExp);
        }

        static void ShowDeleteEmployeeMenu()
        {
            Console.Write("Введите индекс сотрудника для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                _consoleView.InvokeDeleteEmployee(index);
            }
            else
            {
                _consoleView.ShowError("Неверный формат индекса!");
            }
        }

        static void ShowCalculateSalaryMenu()
        {
            Console.Write("Введите индекс сотрудника: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                _consoleView.InvokeCalculateSalary(index);
            }
            else
            {
                _consoleView.ShowError("Неверный формат индекса!");
            }
        }

        static void ShowAddWorkExpMenu()
        {
            Console.Write("Введите индекс сотрудника: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                _consoleView.InvokeAddWorkExperience(index);
            }
            else
            {
                _consoleView.ShowError("Неверный формат индекса!");
            }
        }

        static void ShowFilterMenu()
        {
            Console.WriteLine("Фильтр по должности:");
            Console.WriteLine("1. Руководители");
            Console.WriteLine("2. Менеджеры");
            Console.WriteLine("3. Стажеры");
            Console.Write("Ваш выбор: ");

            switch (Console.ReadLine())
            {
                case "1": _consoleView.InvokeFilterByVacancy(VacancyType.Head); break;
                case "2": _consoleView.InvokeFilterByVacancy(VacancyType.Manager); break;
                case "3": _consoleView.InvokeFilterByVacancy(VacancyType.Intern); break;
                default: _consoleView.ShowError("Неверный выбор!"); break;
            }
        }
    }
}