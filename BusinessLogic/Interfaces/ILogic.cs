using DomainModel;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// Интерфейс фасада бизнес-логики приложения
    /// </summary>
    public interface ILogic
    {
        void AddEmployee(string name, int workExp, VacancyType vacancy);
        List<Employee> GetEmployees();
        Employee GetEmployeeByIndex(int index);
        bool UpdateEmployee(int index, string name, VacancyType vacancy, int workExp);
        void DeleteEmployee(int index);
        List<Employee> GetEmployeesByVacancy(VacancyType vacancy);
        void AddWorkExp(Employee employee);
        double CalculateSalary(Employee employee);
        int GetTotalEmployees();
        double GetAverageExperience();
        Dictionary<VacancyType, int> GetVacancyDistribution();
        Employee GetMostExperiencedEmployee();
        double GetTotalSalaryBudget();
    }
}
