using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Validators;
using DataAccessLayer;
using DomainModel;
using Ninject.Modules;
using Serilog;

public class SimpleConfigModule : NinjectModule
{
    public override void Load()
    {
        // Логирование
        Bind<ILogger>().ToMethod(ctx =>
            Serilog.Log.Logger).InSingletonScope();

        // Валидаторы
        Bind<EmployeeValidator>().ToSelf().InSingletonScope();
        Bind<EmployeeCreateDtoValidator>().ToSelf().InSingletonScope();

        // Data Access
        Bind<IRepository<Employee>>().To<DapperRepository>().InSingletonScope();

        // Business Services
        Bind<IEmployeeService>().To<EmployeeService>().InSingletonScope();
        Bind<ISalaryCalculator>().To<SalaryCalculator>().InSingletonScope();
        Bind<IStatisticsService>().To<StatisticsService>().InSingletonScope();
        Bind<ILogic>().To<Logic>().InSingletonScope();
    }
}