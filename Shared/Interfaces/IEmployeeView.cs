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
    }
}