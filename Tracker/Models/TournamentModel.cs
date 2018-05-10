using System.Collections.Generic;

namespace TrackerLibrary.Models
{
    public class TournamentModel
    {
        /// <summary>
        /// The unique Identifier for the Tournament
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Creates the Initial Tournament Name
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// Sets the Entry Fee
        /// </summary>
        public decimal EntryFee { get; set; }

        /// <summary>
        /// Pulls the Entered teams from the Team Model
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; } = new List<TeamModel>();

        /// <summary>
        /// Sets the prize amount
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();

        /// <summary>
        /// Creates the rounds per the match of Matchup
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();
    }
}
