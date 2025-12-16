using BusinessLogic;
using BusinessLogic.Interfaces;
using ConsoleApp;
using DomainModel;
using Ninject;
using System;
using System.Text;
using WindowsFormsApp1;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Presenter
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ВЫБОР ИНТЕРФЕЙСА ===");
                Console.WriteLine("1. Windows Forms");
                Console.WriteLine("2. Консоль");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите: ");

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        RunWinForms();
                        break;

                    case ConsoleKey.D2:
                        RunConsole();
                        break;

                    case ConsoleKey.D0:
                        return;
                }
            }
        }

        private static void RunWinForms()
        {
            var kernel = new StandardKernel(new SimpleConfigModule());
            var logic = kernel.Get<ILogic>();

            // Создаем WinForms View
            var view = new WindowsFormsApp1.Form1();
            var presenter = new EmployeeWinFormsPresenter(view, logic);

            // Вызываем Start() как у одногруппника
            view.Start();

            // Запуск формы
            System.Windows.Forms.Application.Run(view);
        }

        private static void RunConsole()
        {
            var kernel = new StandardKernel(new SimpleConfigModule());
            var logic = kernel.Get<ILogic>();

            var view = new EmployeeConsoleView();
            var presenter = new EmployeeConsolePresenter(view, logic);

            // Запуск консольного приложения
            view.Run();
        }
    }
}