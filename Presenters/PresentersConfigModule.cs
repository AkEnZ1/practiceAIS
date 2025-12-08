using Ninject.Modules;

namespace Presenters
{
    public class PresentersConfigModule : NinjectModule
    {
        public override void Load()
        {
            // Регистрируем презентер как главный координатор приложения
            Bind<IApplicationPresenter>().To<EmployeePresenter>().InSingletonScope();

            // Контроллер приложения
            Bind<ApplicationController>().ToSelf().InSingletonScope();
        }
    }
}