using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OOMAC.Domain.Models
{
    [Table("Match")]
    public class Match : DomainObject
    {
        [Column("MatId")]
        public new int Id { get; set; }

        public int? ContestantAId { get; set; }
        public Contestant? ContestantA { get; set; }

        public int? ContestantBId { get; set; }
        public Contestant? ContestantB { get; set; }

        [Column("MatScoreA")]
        [DefaultValue("")]
        public string ScoreContestantAString { get; set; }

        [Column("MatScoreB")]
        [DefaultValue("")]
        public string ScoreContestantBString { get; set; }

        public Bracket Bracket { get; set; }

        [Column("MatScoreAInt")]
        [DefaultValue("0")]
        public int ScoreContestantA { get; set; }

        [Column("MatScoreBInt")]
        [DefaultValue("0")]
        public int ScoreContestantB { get; set; }

        public Contestant Winner
        {
            get
            {
                if (ScoreContestantA == 2 || ScoreContestantAString.Contains("Ht")) return ContestantA;
                if (ScoreContestantB == 2 || ScoreContestantBString.Contains("Ht")) return ContestantB;
                return null;
            }
        }

        public int ContestantWinnerId
        {
            get
            {
                if (ScoreContestantA == 2 || ScoreContestantAString.Contains("Ht")) return ContestantAId ?? -1;
                if (ScoreContestantB == 2 || ScoreContestantBString.Contains("Ht")) return ContestantBId ?? -1;
                return -1;
            }
        }

        //public bool HasWinner => Winner != null;

        public bool HasWinner
        {
            get
            {
                return ScoreContestantA == 2 || (ScoreContestantAString ?? "").Contains("Ht") || ScoreContestantB == 2 || ScoreContestantBString.Contains("Ht");
            }
        }

        public bool HasFinished => HasWinner || (ScoreContestantAString ?? "").Contains("x") || ScoreContestantBString.Contains("x");

        private static int CountScore(string scoreString)
        {
            if (scoreString == null) return 0;
            int countScore = 0;
            countScore += scoreString.Count(f => f == 'M');
            countScore += scoreString.Count(f => f == 'D');
            countScore += scoreString.Count(f => f == 'K');
            countScore += scoreString.Count(f => f == 'T');

            return countScore;
        }

        public string Name
        {
            get
            {
                string contestantA = ContestantA == null ? "" : ContestantA.LastName + " " + ContestantA.FirstName.Substring(0, 1);
                string contestantB = ContestantB == null ? "" : ContestantB.LastName + " " + ContestantB.FirstName.Substring(0, 1);
                string contestantAScore = ScoreContestantA.ToString();
                string contestantBScore = ScoreContestantB.ToString();

                return contestantA + " " + contestantAScore + " - " + contestantBScore + " " + contestantB;
            }
        }
    }
}
