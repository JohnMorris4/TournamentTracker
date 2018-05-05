using System;
using System.Windows.Forms;

namespace TrackerUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Initilize the database connections
            TrackerLibrary.GlobalConfig.InitializeConnections(true, true);
            // Application.Run(new TournamentDashboardForm());
            Application.Run(new CreatePrizeForm());
        }
    }
}
