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
                var form = new Form1();
                appController.AttachView(form); 
                Application.Run(form);
            }
        }
    }
}