using DomainModel;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// Обобщенный интерфейс репозитория для работы с данными
    /// </summary>
    /// <typeparam name="T">Тип сущности, реализующий IDomainObject</typeparam>
    public interface IRepository<T> where T : IDomainObject
    {
        /// <summary>
        /// Добавляет новую сущность в репозиторий
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        void Add(T entity);

        /// <summary>
        /// Удаляет сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        void Delete(int id);

        /// <summary>
        /// Обновляет данные сущности
        /// </summary>
        /// <param name="entity">Сущность с обновленными данными</param>
        void Update(T entity);

        /// <summary>
        /// Получает сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность или null</returns>
        T GetById(int id);

        /// <summary>
        /// Получает все сущности из репозитория
        /// </summary>
        /// <returns>Коллекция всех сущностей</returns>
        IEnumerable<T> GetAll();
    }
}