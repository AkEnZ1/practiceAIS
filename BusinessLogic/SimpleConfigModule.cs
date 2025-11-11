using Ninject.Modules;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
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
    /// Определяет жизненный цикл объектов (Singleton) для оптимального использования ресурсов.
    /// </remarks>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Загружает конфигурацию зависимостей в контейнер Ninject
        /// </summary>
        /// <remarks>
        /// Определяет сопоставления между интерфейсами и их реализациями.
        /// Использует Singleton scope для обеспечения единственного экземпляра сервисов на все приложение.
        /// Централизованная точка управления зависимостями всей бизнес-логики.
        /// </remarks>
        public override void Load()
        {
            Bind<IRepository<Employee>>().To<DapperRepository>().InSingletonScope();
            Bind<IEmployeeService>().To<EmployeeService>().InSingletonScope();
            Bind<ISalaryCalculator>().To<SalaryCalculator>().InSingletonScope();
            Bind<IStatisticsService>().To<StatisticsService>().InSingletonScope();
            Bind<ILogic>().To<Logic>().InSingletonScope(); 
        }
    }
}
