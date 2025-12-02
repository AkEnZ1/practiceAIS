using System;
using System.Windows.Forms;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccessLayer;

namespace Presenters
{
    public class ApplicationController : IDisposable
    {
        private readonly IEmployeeService _employeeService;
        private readonly ISalaryCalculator _salaryCalculator;
        private readonly IStatisticsService _statisticsService;
        private EmployeePresenter _employeePresenter;

        public ApplicationController()
        {
            // Создаем зависимости
            var repository = new DapperRepository();
            _employeeService = new EmployeeService(repository);
            _salaryCalculator = new SalaryCalculator();
            _statisticsService = new StatisticsService(repository);
        }

        /// <summary>
        /// ЕДИНАЯ ТОЧКА ВХОДА - запуск Windows Forms приложения
        /// </summary>
        [STAThread]
        public static void RunWindowsApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var controller = new ApplicationController())
            {
                var form = new WindowsFormsApp1.Form1();
                controller.AttachView(form);
                Application.Run(form);
            }
        }

        /// <summary>
        /// ЕДИНАЯ ТОЧКА ВХОДА - запуск консольного приложения
        /// </summary>
        public static void RunConsoleApplication()
        {
            using (var controller = new ApplicationController())
            {
                var consoleView = new ConsoleApp.ConsoleEmployeeView();
                controller.AttachView(consoleView);
                ConsoleApp.Program.RunConsoleMenu(consoleView);
            }
        }

        /// <summary>
        /// Присоединяет View к Presenter
        /// </summary>
        public void AttachView(IEmployeeView view)
        {
            if (_employeePresenter == null)
            {
                _employeePresenter = new EmployeePresenter(_employeeService, _salaryCalculator, _statisticsService);
            }

            _employeePresenter.AttachView(view);
        }

        public void Dispose()
        {
            _employeePresenter?.DetachView();
        }
    }
}
