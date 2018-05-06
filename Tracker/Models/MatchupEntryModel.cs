
namespace TrackerLibrary.Models
{
    public class MatchupEntryModel
    {
        /// <summary>
        /// Represents One team in the matchup.
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        /// <summary>
        /// Represents the SCORE for the team
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Represents the matchup that
        ///  this team came from.
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
    }
}
