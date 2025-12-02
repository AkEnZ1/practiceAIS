using System;
using System.Collections.Generic;
using BusinessLogic.Interfaces;
using DomainModel;
using Shared.Interfaces;

namespace Presenters
{
    public class EmployeePresenter
    {
        private IEmployeeView _view;
        private readonly IEmployeeService _employeeService;
        private readonly ISalaryCalculator _salaryCalculator;
        private readonly IStatisticsService _statisticsService;

        public EmployeePresenter(
            IEmployeeService employeeService,
            ISalaryCalculator salaryCalculator,
            IStatisticsService statisticsService)
        {
            _employeeService = employeeService;
            _salaryCalculator = salaryCalculator;
            _statisticsService = statisticsService;
        }

        public void AttachView(IEmployeeView view)
        {
            if (_view != null)
                throw new InvalidOperationException("View уже присоединен к Presenter");

            _view = view;
            SubscribeToViewEvents();
        }

        public void DetachView()
        {
            if (_view == null) return;

            UnsubscribeFromViewEvents();
            _view = null;
        }

        // ... остальной код Presenter остается БЕЗ ИЗМЕНЕНИЙ ...
        // Все обработчики HandleAddEmployee, HandleUpdateEmployee и т.д.
    }
}
