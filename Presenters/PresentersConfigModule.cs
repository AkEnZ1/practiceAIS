using Ninject.Modules;
using BusinessLogic.Interfaces;

namespace Presenters
{
    public class PresentersConfigModule : NinjectModule
    {
        public PresentersConfigModule()
        {
        }

        public override void Load()
        {
            // Presenter регистрируем без View (View передается через AttachView)
            Bind<EmployeePresenter>().ToSelf().InSingletonScope();

            // ApplicationController для управления приложением
            Bind<ApplicationController>().ToSelf().InSingletonScope();
        }
    }
}