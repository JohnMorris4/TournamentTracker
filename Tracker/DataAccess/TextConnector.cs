
using System.Collections.Generic;
using System.Linq;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";

        //TODO Wire up the connection to the text file
        public PrizeModel CreatePrize(PrizeModel model)
        {
            // Load Text file and Convert the text to List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            //Find ID
            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            
            model.Id = currentId;

            //Add new record with the new ID
            prizes.Add(model);
            
            //Convert the Prizes to List<String>
            //Save the list<string> to the text file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }
    }
}
