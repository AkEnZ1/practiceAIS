using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BusinessLogic
{
    public class Logic
    {
        List<Employee> Employees = new List<Employee>();
        public void AddEmployee(string name, int workExp, VacancyType vacancy)
        {
            Employee employee = new Employee()
            {
                Name = name,
                WorkExp = workExp,
                Vacancy = vacancy,
            };
            Employees.Add(employee);          
        }
        public List<Employee> GetEmployees()
        {
            return Employees;
        }
        public Employee GetEmployeeByIndex(int index)
        {
            if (index >= 0 &&  index < Employees.Count)
            {
                return Employees[index];
            }
            throw new Exception("Нет сотрудника с заданным индексом");         
        }
        public bool UpdateEmployee(int index, string name, VacancyType vacancy, int workExp)
        {
            if (index >= 0 && index <= Employees.Count)
            {
                Employees[index].Name = name;
                Employees[index].WorkExp = workExp;
                Employees[index].Vacancy = vacancy;
                return true;
            }
            throw new Exception("Такого индекса нет");
        } 
        public void DeleteEmployee(int index)
        {
            Employees.RemoveAt(index);
        }
        public double CalculateSalary(Employee employee)
        {
            var multipliers = new Dictionary<VacancyType, double>()
            {
                { VacancyType.Head, 1.5 },
                { VacancyType.Manager, 1.25 },
                { VacancyType.Intern, 1.1 }
            };
            if (multipliers.TryGetValue(employee.Vacancy, out double vacancyMultiplier))
            {
                return employee.WorkExp * vacancyMultiplier;
            }
            else
            {
                return employee.WorkExp;
            }
        }
    }
}
