using Ninject.Modules;
using BusinessLogic.Interfaces;
using Shared.Interfaces;

namespace Presenters
{
    public class PresentersConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<EmployeePresenter>().ToSelf().InSingletonScope();
        }
    }
}