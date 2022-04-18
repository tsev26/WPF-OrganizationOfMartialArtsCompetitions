
using System.ComponentModel.DataAnnotations;

namespace OOMAC.Domain.Models.Calculating
{
    public class TournamentSummary : DomainObject
    {
        public int TrnId { get; set; }
        public Tournament Tournament { get; set; }

        public int NumberOfContestants { get; set; }

        public bool HasStarted { get; set; }

        public int CurrentBracketId { get; set; }
        public Bracket CurrentBracket { get; set; }

        public string Winner { get; set; }
    }
}
