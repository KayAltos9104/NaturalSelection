using System;
using BlackWitchEngine;
using NaturalSelectionLogic;
using System.Windows.Forms;

namespace NaturalSelectionUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmMain());
            var presenter = new GameCyclePresenter(new GameCycle(), new FrmMain()); // Dependency Injection
            presenter.ViewRun();
        }
    }
}
