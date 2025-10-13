using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicAndModel
{
    /// <summary>
    /// Бизнес-логика для работы с сотрудниками
    /// </summary>
    public class Logic
    {
        private IRepository<Employee> _repository;

        /// <summary>
        /// Инициализирует новый экземпляр Logic с указанным репозиторием
        /// </summary>
        /// <param name="repository">Репозиторий для работы с данными</param>
        public Logic(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Инициализирует новый экземпляр Logic без репозитория
        /// </summary>
        public Logic()
        {
            _repository = null;
        }

        /// <summary>
        /// Устанавливает репозиторий для работы с данными
        /// </summary>
        /// <param name="repository">Репозиторий для работы с данными</param>
        public void SetRepository(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Добавляет нового сотрудника
        /// </summary>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="workExp">Опыт работы в годах</param>
        /// <param name="vacancy">Должность</param>
        public void AddEmployee(string name, int workExp, VacancyType vacancy)
        {
            if (_repository == null)
                throw new InvalidOperationException("Репозиторий не установлен");

            Employee employee = new Employee()
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
        /// <returns>Список сотрудников</returns>
        public List<Employee> GetEmployees()
        {
            if (_repository == null)
                throw new InvalidOperationException("Репозиторий не установлен");

            return _repository.GetAll().ToList();
        }

        /// <summary>
        /// Получает сотрудника по индексу в списке
        /// </summary>
        /// <param name="index">Индекс сотрудника в списке</param>
        /// <returns>Найденный сотрудник</returns>
        public Employee GetEmployeeByIndex(int index)
        {
            if (_repository == null)
                throw new InvalidOperationException("Репозиторий не установлен");

            var employees = _repository.GetAll().ToList();
            if (index >= 0 && index < employees.Count)
            {
                return employees[index];
            }
            throw new Exception("Нет сотрудника с заданным индексом");
        }

        /// <summary>
        /// Получает сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Найденный сотрудник или null</returns>
        public Employee GetEmployeeById(int id)
        {
            if (_repository == null)
                throw new InvalidOperationException("Репозиторий не установлен");

            return _repository.GetById(id);
        }

        /// <summary>
        /// Обновляет данные сотрудника
        /// </summary>
        /// <param name="index">Индекс сотрудника в списке</param>
        /// <param name="name">Новое имя</param>
        /// <param name="vacancy">Новая должность</param>
        /// <param name="workExp">Новый опыт работы</param>
        /// <returns>True если обновление успешно, иначе False</returns>
        public bool UpdateEmployee(int index, string name, VacancyType vacancy, int workExp)
        {
            if (_repository == null)
                throw new InvalidOperationException("Репозиторий не установлен");

            var employees = _repository.GetAll().ToList();
            if (index >= 0 && index < employees.Count)
            {
                var employee = employees[index];
                employee.Name = name;
                employee.WorkExp = workExp;
                employee.Vacancy = vacancy;
                _repository.Update(employee);
                return true;
            }
            throw new Exception("Такого индекса нет");
        }

        /// <summary>
        /// Удаляет сотрудника по индексу
        /// </summary>
        /// <param name="index">Индекс сотрудника в списке</param>
        public void DeleteEmployee(int index)
        {
            if (_repository == null)
                throw new InvalidOperationException("Репозиторий не установлен");

            var employees = _repository.GetAll().ToList();
            if (index >= 0 && index < employees.Count)
            {
                var employee = employees[index];
                _repository.Delete(employee.ID);
            }
            else
            {
                throw new Exception("Неверный индекс для удаления");
            }
        }

        /// <summary>
        /// Рассчитывает зарплату сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        /// <returns>Рассчитанная зарплата</returns>
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
        /// Получает сотрудников по должности
        /// </summary>
        /// <param name="vacancy">Тип должности</param>
        /// <returns>Список сотрудников с указанной должностью</returns>
        public List<Employee> GetEmployeesByVacancy(VacancyType vacancy)
        {
            if (_repository == null)
                throw new InvalidOperationException("Репозиторий не установлен");

            return _repository.GetAll().Where(e => e.Vacancy == vacancy).ToList();
        }

        /// <summary>
        /// Добавляет один год стажа сотруднику
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        public void AddWorkExp(Employee employee)
        {
            if (_repository == null)
                throw new InvalidOperationException("Репозиторий не установлен");

            employee.WorkExp++;
            _repository.Update(employee);
        }
    }
}