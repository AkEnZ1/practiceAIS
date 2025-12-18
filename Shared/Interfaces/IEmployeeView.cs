using System;
using System.Collections.Generic;
using DomainModel;

namespace Shared.Interfaces
{
    public interface IEmployeeView : IView
    {
        void RefreshEmployeeList(List<Employee> employees);
        void ShowEmployeeDetails(Employee employee);
        void ClearEmployeeDetails();

        // Методы для чтения данных (нужны для консоли, для WinForms можно реализовать пустые)
        string ReadString(string prompt);
        int ReadInt(string prompt, int min, int max);
        VacancyType ReadVacancy(string prompt);

        // События
        event Action<string, int, VacancyType> OnAddEmployee;
        event Action<int, string, VacancyType, int> OnUpdateEmployee;
        event Action<int> OnDeleteEmployee;
        event Action<int> OnEmployeeSelected;
        event Action<int> OnCalculateSalary;
        event Action<int> OnAddWorkExperience;
        event Action OnShowStatistics;
        event Action<VacancyType> OnFilterByVacancy;
        event Action OnShowAllEmployees;
        event Action<int> OnFindByIndex;

        // StartupEvent как у одногруппника
        event Action StartupEvent;
    }
}