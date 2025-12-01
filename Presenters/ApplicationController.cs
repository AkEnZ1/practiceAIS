// Presenters/ApplicationController.cs - УПРОЩЕННЫЙ
using System;
using BusinessLogic;
using BusinessLogic.Interfaces;
using Ninject;
using Shared.Interfaces;

namespace Presenters
{
    public class ApplicationController : IDisposable
    {
        private readonly IKernel _kernel;
        private EmployeePresenter _employeePresenter;

        public ApplicationController()
        {
            _kernel = new StandardKernel(
                new BusinessLogic.SimpleConfigModule(),
                new PresentersConfigModule()
            );
        }

        /// <summary>
        /// Присоединяет View к Presenter
        /// </summary>
        public void AttachView(IEmployeeView view)
        {
            if (_employeePresenter == null)
            {
                _employeePresenter = _kernel.Get<EmployeePresenter>();
            }

            _employeePresenter.AttachView(view);
        }

        public void Dispose()
        {
            _employeePresenter?.DetachView();
            _kernel?.Dispose();
        }
    }
}