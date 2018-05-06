using System.Data;
using Dapper;
using TrackerLibrary.Models;

//"@PlaceName", "@PrizeAmount", "@PrizePercentage"
namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        //TODO - make the CreatePrize save to the DataBase.
        /// <summary>
        /// Saves a prize to the Database from the Prize Model
        /// </summary>
        /// <param name="model">The prize Information</param>
        /// <returns>The Prize information including the ID</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id",0 , dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }
    }
}
