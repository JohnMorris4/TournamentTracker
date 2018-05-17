
namespace TrackerLibrary.Models
{
    public class MatchupEntryModel
    {
        public int Id { get; set; }

        /// <summary>
        /// The unique Id for the Team competing
        /// </summary>
        public int TeamCompetingId { get; set; }

        /// <summary>
        /// Represents One team in the matchup.
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        /// <summary>
        /// Represents the SCORE for the team
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// the uniique id for the parent matchup
        /// </summary>
        public int ParentMatchupId { get; set; }


        /// <summary>
        /// Represents the matchup that
        ///  this team came from.
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }
    }
}
