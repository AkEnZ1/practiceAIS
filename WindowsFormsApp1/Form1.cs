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
            // Единая точка входа через ApplicationController
            ApplicationController.RunWindowsApplication();
        }
    }
}
