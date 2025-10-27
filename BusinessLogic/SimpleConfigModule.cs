using Ninject.Modules;
using DataAccessLayer;
using DomainModel;

namespace BusinessLogic
{
    /// <summary>
    /// Модуль конфигурации Ninject для настройки зависимостей приложения
    /// </summary>
    /// <remarks>
    /// Реализует принцип инверсии зависимостей (DIP) путем связывания абстракций с конкретными реализациями.
    /// Позволяет централизованно управлять зависимостями в приложении.
    /// </remarks>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Загружает конфигурацию зависимостей в контейнер Ninject
        /// </summary>
        /// <remarks>
        /// Определяет сопоставления между интерфейсами и их реализациями.
        /// Использует Singleton scope для обеспечения единственного экземпляра репозитория на все приложение.
        /// Выбор реализации репозитория (Dapper или Entity Framework) осуществляется здесь.
        /// </remarks>
        public override void Load()
        {
            // Регистрируем зависимости как Singleton для обеспечения единственного экземпляра

            // Для использования Dapper в качестве ORM:
            Bind<IRepository<Employee>>().To<DapperRepository>().InSingletonScope();

            // Альтернативная реализация через Entity Framework:
            // Bind<IRepository<Employee>>().To<EntityRepository<Employee>>().InSingletonScope();

            // Регистрируем бизнес-логику
            Bind<Logic>().ToSelf();
        }
    }
}