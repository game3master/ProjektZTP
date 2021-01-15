using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KCK_Window_project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool closeProgram = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            while (closeProgram == false)
            {
                Application.Run(new StartGame());
                Application.Run(new Game());
                Application.Run(new EndGame());
                closeProgram = EndGame.GetDecision();
            }
        }
    }
}
