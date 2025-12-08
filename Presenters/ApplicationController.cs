using System;
using Ninject;

namespace Presenters
{
    public class ApplicationController : IDisposable
    {
        private readonly IKernel _kernel;

        public ApplicationController()
        {
            _kernel = new StandardKernel(
                new BusinessLogic.SimpleConfigModule(),
                new PresentersConfigModule()
            );
        }

        public IApplicationPresenter CreatePresenter()
        {
            return _kernel.Get<IApplicationPresenter>();
        }

        public void Dispose()
        {
            _kernel?.Dispose();
        }
    }
}