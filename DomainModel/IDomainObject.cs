using System;

namespace DomainModel
{
    /// <summary>
    /// Интерфейс для объектов домена с идентификатором
    /// </summary>
    public interface IDomainObject
    {
        /// <summary>
        /// Уникальный идентификатор объекта
        /// </summary>
        int ID { get; set; }
    }
}