using System.Collections.Generic;

namespace TrackerLibrary
{
    public class TeamModel
    {
        /// <summary>
        /// Creates the list of TeamMembers
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
        /// <summary>
        /// Creates the Team Name
        /// </summary>
        public string TeamName { get; set; }

    }
}
