using System;
using System.Collections.Generic;
using System.Linq;
using LogicAndModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий для работы с сотрудниками using Entity Framework
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly EmployeeContext _context;

        /// <summary>
        /// Инициализирует новый экземпляр EntityRepository
        /// </summary>
        public EntityRepository()
        {
            _context = new EmployeeContext();
        }

        /// <summary>
        /// Добавляет нового сотрудника в базу данных
        /// </summary>
        /// <param name="entity">Сотрудник для добавления</param>
        public void Add(T entity)
        {
            // Приводим к конкретному типу для SQLite
            if (entity is Employee employee)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Удаляет сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        public void Delete(int id)
        {
            var entity = _context.Employees.FirstOrDefault(e => e.ID == id);
            if (entity != null)
            {
                _context.Employees.Remove(entity);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Обновляет данные сотрудника
        /// </summary>
        /// <param name="entity">Сотрудник с обновленными данными</param>
        public void Update(T entity)
        {
            if (entity is Employee employee)
            {
                var existing = _context.Employees.FirstOrDefault(e => e.ID == employee.ID);
                if (existing != null)
                {
                    existing.Name = employee.Name;
                    existing.Vacancy = employee.Vacancy;
                    existing.WorkExp = employee.WorkExp;
                    _context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Получает сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Найденный сотрудник или null</returns>
        public T GetById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.ID == id) as T;
        }

        /// <summary>
        /// Получает всех сотрудников из базы данных
        /// </summary>
        /// <returns>Коллекция всех сотрудников</returns>
        public IEnumerable<T> GetAll()
        {
            return _context.Employees.ToList() as IEnumerable<T>;
        }
    }
}