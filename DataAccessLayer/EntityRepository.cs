using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DomainModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий для работы с сущностями using Entity Framework
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Инициализирует новый экземпляр EntityRepository
        /// </summary>
        public EntityRepository()
        {
            _context = new EmployeeContext();
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Добавляет новую сущность в базу данных
        /// </summary>
        /// <param name="entity">Сущность для добавления</param>
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public void Delete(int id)
        {
            var entity = _dbSet.FirstOrDefault(e => e.ID == id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Обновляет данные сущности
        /// </summary>
        /// <param name="entity">Сущность с обновленными данными</param>
        public void Update(T entity)
        {
            var existing = _dbSet.Find(entity.ID);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Получает сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность или null</returns>
        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(e => e.ID == id);
        }

        /// <summary>
        /// Получает все сущности из базы данных
        /// </summary>
        /// <returns>Коллекция всех сущностей</returns>
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// Освобождает ресурсы
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}