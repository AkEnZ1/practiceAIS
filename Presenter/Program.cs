using BusinessLogic;
using BusinessLogic.Interfaces;
using ConsoleApp;
using DomainModel;
using Ninject;
using Shared.Interfaces;
using System;
using System.Text;
using System.Windows.Forms;

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

        /// <summary>
        /// Запуск WinForms-интерфейса
        /// </summary>
        private static void RunWinForms()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var kernel = new StandardKernel(new SimpleConfigModule());
            var logic = kernel.Get<ILogic>();

            // Form1 должен реализовывать IEmployeeView
            IEmployeeView view = new WindowsFormsApp1.Form1();

            var presenter = new EmployeePresenter(view, logic);

            // Запуск формы
            Application.Run((Form)view);
        }

        /// <summary>
        /// Запуск консольного интерфейса
        /// </summary>
        private static void RunConsole()
        {
            var kernel = new StandardKernel(new SimpleConfigModule());
            var logic = kernel.Get<ILogic>();

            // EmployeeConsoleView должен реализовывать IEmployeeView
            IEmployeeView view = new EmployeeConsoleView();

            var presenter = new EmployeePresenter(view, logic);

            // Управление жизненным циклом — внутри View
            ((EmployeeConsoleView)view).Run();
        }
    }
}