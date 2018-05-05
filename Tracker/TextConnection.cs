namespace TrackerLibrary
{
    public class TextConnection : IDataConnection
    {
        //TODO Wire up the connection to the text file
        public PrizeModel CreatePrize(PrizeModel model)
        {
            model.Id = 1;

            return model;
        }
    }
}
