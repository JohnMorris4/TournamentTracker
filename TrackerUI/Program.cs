using System;
using System.Windows.Forms;
using TrackerLibrary;

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
            TrackerLibrary.GlobalConfig.InitializeConnections(DatabaseType.TextFile);
            // Application.Run(new TournamentDashboardForm());
            Application.Run(new CreateTournamentForm());
        }
    }
}
