using Ninject.Modules;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccessLayer;
using DomainModel;

namespace BusinessLogic
{
    /// <summary>
    /// Модуль конфигурации Ninject для слоя бизнес-логики.
    /// Определяет связи между интерфейсами и их реализациями
    /// в соответствии с принципом инверсии зависимостей.
    /// </summary>
    /// <remarks>
    /// Конфигурирует все зависимости бизнес-слоя:
    /// - Репозитории для доступа к данным
    /// - Сервисы бизнес-логики
    /// - Фасад бизнес-логики (ILogic)
    /// Использует синглтон-область видимости для всех компонентов,
    /// так как они являются статическими сервисами без состояния.
    /// </remarks>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Загружает конфигурацию связей для слоя бизнес-логики
        /// </summary>
        public override void Load()
        {
            // Data Access
            Bind<IRepository<Employee>>().To<DapperRepository>().InSingletonScope();

            // Services
            Bind<IEmployeeService>().To<EmployeeService>().InSingletonScope();
            Bind<ISalaryCalculator>().To<SalaryCalculator>().InSingletonScope();
            Bind<IStatisticsService>().To<StatisticsService>().InSingletonScope();

            // Main business logic facade
            Bind<ILogic>().To<Logic>().InSingletonScope();
        }
    }
}