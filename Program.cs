using ComputerPartsServerGUI;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ComputerPartsServer
{
    class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}