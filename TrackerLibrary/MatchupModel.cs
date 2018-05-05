﻿using System.Collections.Generic;

namespace TrackerLibrary
{
    public class MatchupModel
    {
        /// <summary>
        /// Setup the Initial Matchup per round of play
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        /// <summary>
        /// Defines the Winner of each round of play
        /// </summary>
        public TeamModel Winner { get; set; }
        /// <summary>
        /// Creates the initial Round of play
        /// </summary>
        public int Round { get; set; }

    }
}