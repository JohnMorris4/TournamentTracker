using System.Configuration;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public const string PrizesFile = "PrizeModels.csv";
        public const string PeopleFile = "PersonModels.csv";
        public const string TeamFile = "TeamModels.csv";
        public const string TournamentFile = "TournamentModels.csv";
        public const string MatchupFile = "MatchupModels.csv";
        public const string MatchupEntryFile = "MatchupEntryModels.csv";


        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnections(DatabaseType dbType)
        {
            //switch(dbType)
            //{
            //    case DatabaseType.Sql:
            //        break;
            //    case DatabaseType.TextFile:
            //        break;
            //    default:
            //        break;
            //}
            if (dbType == DatabaseType.Sql)
            {
                //TODO - Setup the SQL connection properly
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }
            else if (dbType == DatabaseType.TextFile)
            {
                //TODO - Setup the connection string to the textfiles
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }

        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
