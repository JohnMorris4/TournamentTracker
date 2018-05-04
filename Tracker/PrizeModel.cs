namespace TrackerLibrary
{
    public class PrizeModel
    {
        /// <summary>
        /// Set the unique Id for the prize.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Initializes hte placenumber of of the prize
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// initializes the place name
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// Creates the Prize amount vs percentage
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// percentage vs the amount
        /// </summary>
        public double PrizePercentage { get; set; }

        public PrizeModel()
        {


        }

        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage)
        {
            PlaceName = placeName;

            //int placeNumberValue 
        }
    }

}
