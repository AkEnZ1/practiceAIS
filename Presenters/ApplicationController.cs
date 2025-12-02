using System;
using System.Windows.Forms;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccessLayer;

namespace Presenters
{
    /// <summary>
    /// Единая точка входа в приложение - управляет запуском всех компонентов
    /// </summary>
    public class ApplicationController : IDisposable
    {
        private IEmployeeService _employeeService;
        private ISalaryCalculator _salaryCalculator;
        private IStatisticsService _statisticsService;
        private EmployeePresenter _presenter;

        /// <summary>
        /// Запуск приложения с Windows Forms интерфейсом
        /// </summary>
        [STAThread]
        public static void RunWindowsApplication()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                using (var controller = new ApplicationController())
                {
                    controller.Initialize();
                    
                    // Создаем Windows Forms View
                    var viewType = Type.GetType("WindowsFormsApp1.Form1, WindowsFormsApp1");
                    if (viewType == null)
                        throw new TypeLoadException("Не удалось загрузить Windows Forms приложение");
                    
                    var view = (IEmployeeView)Activator.CreateInstance(viewType);
                    
                    // Присоединяем View к Presenter
                    controller.AttachView(view);
                    
                    // Запускаем приложение
                    Application.Run((Form)view);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка запуска приложения: {ex.Message}", 
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Запуск приложения с консольным интерфейсом
        /// </summary>
        public static void RunConsoleApplication()
        {
            try
            {
                using (var controller = new ApplicationController())
                {
                    controller.Initialize();
                    
                    // Создаем консольное View
                    var view = new ConsoleEmployeeView();
                    
                    // Присоединяем View к Presenter
                    controller.AttachView(view);
                    
                    // Запускаем консольное меню
                    ConsoleMenuRunner.Run(view);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запуска приложения: {ex.Message}");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Инициализация сервисов
        /// </summary>
        private void Initialize()
        {
            var repository = new DapperRepository();
            _employeeService = new EmployeeService(repository);
            _salaryCalculator = new SalaryCalculator();
            _statisticsService = new StatisticsService(repository);
        }

        /// <summary>
        /// Присоединяет View к Presenter
        /// </summary>
        public void AttachView(IEmployeeView view)
        {
            if (_presenter != null)
            {
                // Если уже есть Presenter, отвязываем его
                DetachView();
            }
            
            _presenter = new EmployeePresenter(view, _employeeService, _salaryCalculator, _statisticsService);
        }

        /// <summary>
        /// Отсоединяет View от Presenter
        /// </summary>
        public void DetachView()
        {
            if (_presenter != null)
            {
                // Здесь можно добавить логику отписки от событий, если нужно
                _presenter = null;
            }
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            DetachView();
            
            // Освобождаем сервисы, если они реализуют IDisposable
            if (_employeeService is IDisposable disposableEmployeeService)
                disposableEmployeeService.Dispose();
                
            if (_statisticsService is IDisposable disposableStatisticsService)
                disposableStatisticsService.Dispose();
        }
    }
}
