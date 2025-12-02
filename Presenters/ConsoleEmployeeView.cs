using System;
using System.Collections.Generic;
using DomainModel;
using Shared.Interfaces;

namespace Presenters
{
    /// <summary>
    /// Консольное представление для приложения
    /// </summary>
    public class ConsoleEmployeeView : IEmployeeView
    {
        private List<Employee> _currentEmployees = new List<Employee>();

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
}
