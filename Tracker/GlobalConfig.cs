using System.Collections.Generic;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();

        public static void InitializeConnections(bool database, bool textFiles)
        {
            if (database)
            {
                //TODO - Setup the SQL connection properly
                SqlConnector sql = new SqlConnector();
                Connections.Add(sql);

            }

            if (textFiles)
            {
                //TODO - Setup the connection string to the textfiles
                TextConnection text = new TextConnection();
                Connections.Add(text);
            }
        }
    }
}
