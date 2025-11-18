using System.Collections.Generic;
using DomainModel;

namespace BusinessLogic.Interfaces
{
    public interface ILogic
    {
        // Employee operations
        void AddEmployee(string name, int workExp, VacancyType vacancy);
        List<Employee> GetEmployees();
        Employee GetEmployeeByIndex(int index);
        bool UpdateEmployee(int index, string name, VacancyType vacancy, int workExp);
        void DeleteEmployee(int index);
        List<Employee> GetEmployeesByVacancy(VacancyType vacancy);
        void AddWorkExp(Employee employee);

        // Salary operations
        double CalculateSalary(Employee employee);

        // Statistics operations
        int GetTotalEmployees();
        double GetAverageExperience();
        Dictionary<VacancyType, int> GetVacancyDistribution();
        Employee GetMostExperiencedEmployee();
        double GetTotalSalaryBudget();

        // Services for Presenter
        IEmployeeService EmployeeService { get; }
        ISalaryCalculator SalaryCalculator { get; }
        IStatisticsService StatisticsService { get; }
    }
}