using BusinessLogic.Interfaces;
using DataAccessLayer;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    /// <summary>
    /// Сервис для управления сотрудниками
    /// </summary>
    /// <remarks>
    /// Реализует бизнес-логику работы с сотрудниками.
    /// Следует принципу единственной ответственности - отвечает только за операции с сотрудниками.
    /// </remarks>
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;

        /// <summary>
        /// Инициализирует новый экземпляр EmployeeService
        /// </summary>
        /// <param name="repository">Репозиторий для работы с данными</param>
        public EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Добавляет нового сотрудника в систему
        /// </summary>
        public void AddEmployee(string name, int workExp, VacancyType vacancy)
        {
            var employee = new Employee()
            {
                Name = name,
                WorkExp = workExp,
                Vacancy = vacancy,
            };
            _repository.Add(employee);
        }

        /// <summary>
        /// Получает список всех сотрудников
        /// </summary>
        public List<Employee> GetEmployees() => _repository.GetAll().ToList();

        /// <summary>
        /// Получает сотрудника по индексу в списке
        /// </summary>
        public Employee GetEmployeeByIndex(int index)
        {
            var employees = GetEmployees();
            if (index >= 0 && index < employees.Count)
                return employees[index];

            throw new ArgumentOutOfRangeException(nameof(index), "Нет сотрудника с заданным индексом");
        }

        /// <summary>
        /// Обновляет данные сотрудника
        /// </summary>
        public bool UpdateEmployee(int index, string name, VacancyType vacancy, int workExp)
        {
            var employee = GetEmployeeByIndex(index);
            employee.Name = name;
            employee.WorkExp = workExp;
            employee.Vacancy = vacancy;
            _repository.Update(employee);
            return true;
        }

        /// <summary>
        /// Удаляет сотрудника по индексу
        /// </summary>
        public void DeleteEmployee(int index)
        {
            var employee = GetEmployeeByIndex(index);
            _repository.Delete(employee.ID);
        }

        /// <summary>
        /// Получает сотрудников по указанной должности
        /// </summary>
        public List<Employee> GetEmployeesByVacancy(VacancyType vacancy)
            => _repository.GetAll().Where(e => e.Vacancy == vacancy).ToList();

        /// <summary>
        /// Добавляет один год стажа сотруднику
        /// </summary>
        public void AddWorkExp(Employee employee)
        {
            employee.WorkExp++;
            _repository.Update(employee);
        }
    }
}