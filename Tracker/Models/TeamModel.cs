using System.Collections.Generic;

namespace TrackerLibrary.Models
{
    public class TeamModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Creates the Team Name
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Creates the list of TeamMembers
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();


    }
}
