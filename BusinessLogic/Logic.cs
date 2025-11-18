using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccessLayer;
using DomainModel;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class Logic : ILogic
    {
        private readonly IEmployeeService _employeeService;
        private readonly ISalaryCalculator _salaryCalculator;
        private readonly IStatisticsService _statisticsService;

        public Logic(IRepository<Employee> repository)
        {
            _employeeService = new EmployeeService(repository);
            _salaryCalculator = new SalaryCalculator();
            _statisticsService = new StatisticsService(repository);
        }

        // Services for Presenter
        public IEmployeeService EmployeeService => _employeeService;
        public ISalaryCalculator SalaryCalculator => _salaryCalculator;
        public IStatisticsService StatisticsService => _statisticsService;

        // Employee operations
        public void AddEmployee(string name, int workExp, VacancyType vacancy)
            => _employeeService.AddEmployee(name, workExp, vacancy);

        public List<Employee> GetEmployees()
            => _employeeService.GetEmployees();

        public Employee GetEmployeeByIndex(int index)
            => _employeeService.GetEmployeeByIndex(index);

        public bool UpdateEmployee(int index, string name, VacancyType vacancy, int workExp)
            => _employeeService.UpdateEmployee(index, name, vacancy, workExp);

        public void DeleteEmployee(int index)
            => _employeeService.DeleteEmployee(index);

        public List<Employee> GetEmployeesByVacancy(VacancyType vacancy)
            => _employeeService.GetEmployeesByVacancy(vacancy);

        public void AddWorkExp(Employee employee)
            => _employeeService.AddWorkExp(employee);

        // Salary operations
        public double CalculateSalary(Employee employee)
            => _salaryCalculator.CalculateSalary(employee);

        // Statistics operations
        public int GetTotalEmployees()
            => _statisticsService.GetTotalEmployees();

        public double GetAverageExperience()
            => _statisticsService.GetAverageExperience();

        public Dictionary<VacancyType, int> GetVacancyDistribution()
            => _statisticsService.GetVacancyDistribution();

        public Employee GetMostExperiencedEmployee()
            => _statisticsService.GetMostExperiencedEmployee();

        public double GetTotalSalaryBudget()
        {
            var employees = GetEmployees();
            double total = 0;
            foreach (var employee in employees)
            {
                total += CalculateSalary(employee);
            }
            return total;
        }
    }
}