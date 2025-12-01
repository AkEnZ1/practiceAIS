using Ninject.Modules;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccessLayer;
using DomainModel;

namespace BusinessLogic
{
    public class SimpleConfigModule : NinjectModule
    {
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