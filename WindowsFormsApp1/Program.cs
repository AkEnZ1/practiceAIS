using System;
using System.Windows.Forms;
using Presenters;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var appController = new ApplicationController())
            {
                // Создаем презентер
                var presenter = appController.CreatePresenter();

                // Создаем форму
                var form = new Form1();

                // Привязываем View к презентеру
                presenter.AttachView(form);

                // Запускаем приложение
                Application.Run(form);

                // Отсоединяем View при закрытии
                presenter.DetachView();
            }
        }
    }
}