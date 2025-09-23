using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAndModel
{
    public class Logic
    {
        List<Employee> Employees = new List<Employee>();
        /// <summary>
        /// Добавляет нового сотрудника в список.
        /// </summary>
        /// <param name="name">Имя сотрудника.</param>
        /// <param name="workExp">Опыт работы в годах.</param>
        /// <param name="vacancy">Должность сотрудника.</param>

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
        /// <summary>
        /// Возвращает сотрудника по указанному индексу в списке.
        /// </summary>
        /// <param name="index">Индекс сотрудника в списке (начиная с 0).</param>
        /// <returns>Объект Employee по указанному индексу.</returns>
        /// <exception cref="Exception">Выбрасывается, если индекс выходит за границы списка.</exception>
        public Employee GetEmployeeByIndex(int index)
        {
            if (index >= 0 && index < Employees.Count)
            {
                return Employees[index];
            }
            throw new Exception("Нет сотрудника с заданным индексом");
        }
        /// <summary>
        /// Обновляет информацию о сотруднике по указанному индексу.
        /// </summary>
        /// <param name="index">Индекс сотрудника в списке.</param>
        /// <param name="name">Новое имя сотрудника.</param>
        /// <param name="vacancy">Новая должность сотрудника.</param>
        /// <param name="workExp">Новый опыт работы в годах.</param>
        /// <returns>true, если обновление прошло успешно.</returns>
        /// <exception cref="Exception">Выбрасывается, если индекс выходит за границы списка.</exception>
        public bool UpdateEmployee(int index, string name, VacancyType vacancy, int workExp)
        {
            if (index >= 0 && index < Employees.Count)
            {
                Employees[index].Name = name;
                Employees[index].WorkExp = workExp;
                Employees[index].Vacancy = vacancy;
                return true;
            }
            throw new Exception("Такого индекса нет");
        }
        /// <summary>
        /// Удаляет сотрудника из списка по указанному индексу.
        /// </summary>
        /// <param name="index">Индекс сотрудника для удаления.</param>
        /// <exception cref="Exception">Выбрасывается, если индекс выходит за границы списка.</exception>
        public void DeleteEmployee(int index)
        {
            if (index >= 0 && index < Employees.Count)
            {
                Employees.RemoveAt(index);
            }
            else
            {
                throw new Exception("Неверный индекс для удаления");
            }
        }
        /// <summary>
        /// Вычисляет заработную плату сотрудника на основе опыта работы и должности.
        /// </summary>
        /// <param name="employee">Сотрудник, для которого рассчитывается зарплата.</param>
        /// <returns>
        /// Размер заработной платы. 
        /// Базовая формула: опыт работы × коэффициент должности × 10000.
        /// </returns>
        /// <remarks>
        /// Коэффициенты должностей:
        /// Head - 1.5, Manager - 1.25, Intern - 1.1
        /// </remarks>
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
                return employee.WorkExp * vacancyMultiplier * 10000;
            }
            else
            {
                return employee.WorkExp * 10000;
            }
        }
        /// <summary>
        /// Увеличивает опыт работы сотрудника на 1 год.
        /// </summary>
        /// <param name="employee">Сотрудник, чей опыт работы увеличивается.</param>
        public void AddWorkExp(Employee employee)
        {
            employee.WorkExp++;
        }
    }
}
