using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAndModel
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