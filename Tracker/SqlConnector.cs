namespace TrackerLibrary
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
            model.Id = 1;

            return model;
        }
    }
}
