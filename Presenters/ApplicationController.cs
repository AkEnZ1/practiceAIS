using System;
using Ninject;
using Shared.Interfaces;

namespace Presenters
{
    public class ApplicationController : IDisposable
    {
        private EmployeePresenter _employeePresenter;

        public ApplicationController()
        {
            // Простая реализация без Ninject если проблемы
        }

        /// <summary>
        /// Присоединяет View к Presenter (упрощенная версия)
        /// </summary>
        public void AttachView(IEmployeeView view)
        {
            if (_employeePresenter != null)
                throw new InvalidOperationException("Presenter уже создан");

            // Создаем Presenter вручную (без DI)
            // Нужно получить зависимости для конструктора EmployeePresenter
            // Это временное решение

            // Пока возвращаем заглушку
            // TODO: Реализовать полноценную инициализацию
        }

        public void Dispose()
        {
            _employeePresenter?.DetachView();
        }
    }
}