﻿using System.Collections.Generic;

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
        /// Setsup the Winner by Id
        /// </summary>
        public int WinnerId { get; set; }

        /// <summary>
        /// Defines the Winner of each round of play
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// Creates the initial MatchupRound of play
        /// </summary>
        public int MatchupRound { get; set; }

        public string DisplayName
        {
            get
            {
                string output = "";

                foreach (MatchupEntryModel me in Entries)
                {
                    if (me.TeamCompeting != null)
                    {
                        if (output.Length == 0)
                        {
                            output = me.TeamCompeting.TeamName;
                        }
                        else
                        {
                            output += $" vs. { me.TeamCompeting.TeamName }";
                        }
                    }
                    else
                    {
                        output = "Matchup Not Yet Determined";
                        break;
                    }
                }

                return output;
            }
        }
    }
}
