using System.Collections.Generic;

namespace TrackerLibrary.Models
{

    public class MatchupModel
    {
        /// <summary>
        /// MatchUp Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Setup the Initial Matchup per round of play
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();

        /// <summary>
        /// Defines the Winner of each round of play
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// Creates the initial MatchupRound of play
        /// </summary>
        public int MatchupRound { get; set; }
    }
}
