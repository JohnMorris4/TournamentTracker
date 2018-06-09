using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        public static void CreateRounds(TournamentModel model)
        {
            List<TeamModel> randomTeams = RandomTeamOrder(model.EnteredTeams);
            int rounds = FindNumberOfRounds(randomTeams.Count);
            int byes = NumberOfByes(rounds, randomTeams.Count);

            model.Rounds.Add(CreateFirstRound(byes, randomTeams));

            CreateOtherRounds(model, rounds);

            UpdateTournamentResults(model);
        }

        public static void UpdateTournamentResults(TournamentModel model)
        {
            int startingRound = model.CheckCurrentRound();

            List<MatchupModel> toScore = new List<MatchupModel>();

            foreach (List<MatchupModel> round in model.Rounds)
            {
                foreach (MatchupModel rm in round)
                {
                    if (rm.Winner == null && (rm.Entries.Any(x => x.Score != 0) || rm.Entries.Count == 1))
                    {
                        toScore.Add(rm);
                    }
                }
            }

            MarkWinnerInMatchups(toScore);

            AdvanceWinners(toScore, model);

            toScore.ForEach(x => GlobalConfig.Connection.UpdateMatchup(x));
            int endingRound = model.CheckCurrentRound();

            if (endingRound > startingRound)
            {
                model.AlertUsersToNewRound();
            }

        }

        public static void AlertUsersToNewRound(this TournamentModel model)
        {
            int currentRoundNumber = model.CheckCurrentRound();
            List<MatchupModel> currentRound =
                model
                    .Rounds
                    .Where(x => x.First()
                    .MatchupRound == currentRoundNumber)
                    .First();

            foreach (MatchupModel matchup in currentRound)
            {
                foreach (MatchupEntryModel me in matchup.Entries)
                {
                    foreach (PersonModel person in me.TeamCompeting.TeamMembers)
                    {
                        AlertPersonToNewRound(person, me.TeamCompeting.TeamName,
                            matchup.Entries.Where(x => x.TeamCompeting != me.TeamCompeting).FirstOrDefault());
                    }
                }
            }
        }

        private static void AlertPersonToNewRound(PersonModel person, string teamName, MatchupEntryModel competitor)
        {
            if (person.EmailAddress.Length > 0)
            {
                string to = "";
                string subject = "";

                StringBuilder body = new StringBuilder();

                if (competitor != null)
                {
                    subject = $"You have a new matchup with { competitor.TeamCompeting.TeamName }";

                    body.AppendLine("<h1>You have a new matchup</h1>");
                    body.Append("<strong>Competitor: </strong>");
                    body.Append(competitor.TeamCompeting.TeamName);
                    body.AppendLine();
                    body.AppendLine();
                    body.AppendLine("Have an enjoyable match");
                    body.AppendLine("~ Tournament Tracker ~");
                }
                else
                {
                    subject = "You have a bye week this round.";

                    body.AppendLine("Thank You, enjoy your round off");
                    body.AppendLine("~ Tournament Tracker ~");
                }


                to = person.EmailAddress;

                EmailLogic.SendEmail(to, subject, body.ToString());
            }

            if (person.CellphoneNumber.Length > 0)
            {
                TextingLogic.SendSMSMessage(person.CellphoneNumber, $"You have a new matchup with { competitor.TeamCompeting.TeamName }"); 
            }
        }

        private static int CheckCurrentRound(this TournamentModel model)
        {
            int output = 1;
            foreach (List<MatchupModel> round in model.Rounds)
            {
                if (round.All(x => x.Winner != null))
                {
                    output += 1;
                }
                else
                {
                    return output;
                }
            }

            CompleteTournament(model);
            return output - 1;
        }

        private static void CompleteTournament(TournamentModel model)
        {
            GlobalConfig.Connection.CompleteTournament(model);
            TeamModel winners = model.Rounds.Last().First().Winner;
            TeamModel runnerUp = model.Rounds.Last().First().Entries.Where(x => x.TeamCompeting != winners).First()
                .TeamCompeting;

            decimal winnerPrize = 0;
            decimal runnerUpPrize = 0;

            if (model.Prizes.Count > 0)
            {
                decimal totalIncome = model.EnteredTeams.Count * model.EntryFee;

                PrizeModel firstPlacePrize = model.Prizes.Where(x => x.PlaceNumber == 1).FirstOrDefault();
                PrizeModel secondPlacePrize = model.Prizes.Where(x => x.PlaceNumber == 2).FirstOrDefault();
                if (firstPlacePrize != null)
                {
                    winnerPrize = firstPlacePrize.CalculatePrizePayout(totalIncome);
                }

                if (secondPlacePrize != null)
                {
                    runnerUpPrize = secondPlacePrize.CalculatePrizePayout(totalIncome);
                }
            }
            //Complete the tournament
            //string to = "";
            string subject = "";

            StringBuilder body = new StringBuilder();


            subject = $"In {model.TournamentName},  { winners.TeamName } has won";

            body.AppendLine("<h1>We have a Winner</h1>");
            body.AppendLine("<h6>Congratulations to the winners of this team</h6>");
            body.AppendLine(Environment.NewLine);

            if (winnerPrize > 0)
            {
                body.AppendLine($"{winners.TeamName} won this match and will recieve ${winnerPrize}");
            }

            if (runnerUpPrize > 0)
            {
                body.AppendLine($"{runnerUp.TeamName} is the runner up of this match and will recieve ${runnerUpPrize}");
            }
            body.AppendLine();
            body.AppendLine();
            body.AppendLine("Have an enjoyable match");
            body.AppendLine("~ Tournament Tracker ~");

            subject = "You have a bye week this round.";

            body.AppendLine("Thank You, for using out system");
            body.AppendLine("~ Tournament Tracker ~");



            List<string> bcc = new List<string>();
            foreach (TeamModel t in model.EnteredTeams)
            {
                foreach (PersonModel p in t.TeamMembers)
                {
                    if (p.EmailAddress.Length > 0)
                    {
                        bcc.Add(p.EmailAddress);
                    }
                }
            }

            

            EmailLogic.SendEmail(new List<string>(), bcc, subject, body.ToString());

            model.CompleteTournament();
        }

        private static decimal CalculatePrizePayout(this PrizeModel prize, decimal totalIncome)
        {
            decimal output = 0;

            if (prize.PrizeAmount > 0)
            {
                output = prize.PrizeAmount;
            }
            else
            {
                output = Decimal.Multiply(totalIncome, Convert.ToDecimal(prize.PrizePercentage / 100));
            }

            return output;
        }
        private static void AdvanceWinners(List<MatchupModel> models, TournamentModel tournament)
        {
            foreach (MatchupModel m in models)
            {
                //model comes from TorunamentModel
                foreach (List<MatchupModel> round in tournament.Rounds)
                {
                    foreach (MatchupModel rm in round)
                    {
                        foreach (MatchupEntryModel me in rm.Entries)
                        {
                            if (me.ParentMatchup != null)
                            {
                                if (me.ParentMatchup.Id == m.Id)
                                {
                                    me.TeamCompeting = m.Winner;
                                    //Updating the Round of the MatchupModel
                                    GlobalConfig.Connection.UpdateMatchup(rm);
                                }
                            }
                        }
                    }
                }
            }
        }
        private static void MarkWinnerInMatchups(List<MatchupModel> models)
        {
            string greaterWins = ConfigurationManager.AppSettings["greaterWins"];

            foreach (MatchupModel m in models)
            {
                // Checks for bye week
                if (m.Entries.Count == 1)
                {
                    m.Winner = m.Entries[0].TeamCompeting;
                    continue;
                }

                if (greaterWins == "0")
                {
                    if (m.Entries[0].Score < m.Entries[1].Score)
                    {
                        m.Winner = m.Entries[0].TeamCompeting;
                    }
                    else if (m.Entries[1].Score < m.Entries[0].Score)
                    {
                        m.Winner = m.Entries[1].TeamCompeting;
                    }
                    else
                    {
                        throw new Exception("We do not allow ties in this application");
                    }
                }
                else
                {
                    if (m.Entries.Count == 1)
                    {
                        m.Winner = m.Entries[0].TeamCompeting;
                        continue;
                    }

                    if (greaterWins == "0")
                    {
                        if (m.Entries[0].Score > m.Entries[1].Score)
                        {
                            m.Winner = m.Entries[0].TeamCompeting;
                        }
                        else if (m.Entries[1].Score > m.Entries[0].Score)
                        {
                            m.Winner = m.Entries[1].TeamCompeting;
                        }
                        else
                        {
                            throw new Exception("We do not allow ties in this application");
                        }
                    }
                }
            }

        }
        private static void CreateOtherRounds(TournamentModel model, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currentRound = new List<MatchupModel>();
            MatchupModel currMatchup = new MatchupModel();

            while (round <= rounds)
            {
                foreach (MatchupModel match in previousRound)
                {
                    currMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = match });
                    if (currMatchup.Entries.Count > 1)
                    {
                        currMatchup.MatchupRound = round;
                        currentRound.Add(currMatchup);
                        currMatchup = new MatchupModel();
                    }
                }

                model.Rounds.Add(currentRound);
                previousRound = currentRound;

                currentRound = new List<MatchupModel>();
                round += 1;
            }
        }

        private static List<MatchupModel> CreateFirstRound(int byes, List<TeamModel> teams)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel curr = new MatchupModel();

            foreach (TeamModel team in teams)
            {
                curr.Entries.Add(new MatchupEntryModel { TeamCompeting = team });

                if (byes > 0 || curr.Entries.Count > 1)
                {
                    curr.MatchupRound = 1;
                    output.Add(curr);
                    curr = new MatchupModel();

                    if (byes > 0)
                    {
                        byes -= 1;
                    }
                }
            }

            return output;
        }

        private static int NumberOfByes(int rounds, int numberOfTeams)
        {
            int output = 0;
            int totalTeams = 1;

            for (int i = 1; i <= rounds; i++)
            {
                totalTeams *= 2;
            }

            output = totalTeams - numberOfTeams;

            return output;
        }

        private static int FindNumberOfRounds(int teamCount)
        {
            int output = 1;
            int val = 2;

            while (val < teamCount)
            {
                output += 1;

                val *= 2;
            }

            return output;
        }

        private static List<TeamModel> RandomTeamOrder(List<TeamModel> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}
