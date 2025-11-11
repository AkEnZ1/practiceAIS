using BusinessLogic.Interfaces;
using DomainModel;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class Logic : ILogic
    {
        private readonly IEmployeeService _employeeService;
        private readonly ISalaryCalculator _salaryCalculator;
        private readonly IStatisticsService _statisticsService;

        public Logic(
            IEmployeeService employeeService, 
            ISalaryCalculator salaryCalculator,
            IStatisticsService statisticsService)
        {
            _employeeService = employeeService;
            _salaryCalculator = salaryCalculator;
            _statisticsService = statisticsService;
        }

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

        public double CalculateSalary(Employee employee)
            => _salaryCalculator.CalculateSalary(employee);

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
