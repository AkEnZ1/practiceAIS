using System;
using System.Windows.Forms;
using Presenter;
namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// Делегирует запуск Presenter'у как точке входа.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Presenter - единая точка входа в приложение
            Presenters.EmployeePresenter.RunApplication();
        }
    }
}
