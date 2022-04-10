using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OOMAC.Domain.Models
{
    [Table("Match")]
    public class Match : DomainObject
    {
        [Column("MatId")]
        public new int Id { get; set; }


        public Contestant ContestantA { get; set; }


        [Column("MatScoreAId")]
        public string ScoreContestantAString { get; set; }


        public Contestant ContestantB { get; set; }

        [Column("MatScoreBId")]
        public string ScoreContestantBString { get; set; }


        public Bracket Bracket { get; set; }

        public int ScoreContestantA => CountScore(ScoreContestantAString);
        public int ScoreContestantB => CountScore(ScoreContestantBString);

        public Contestant Winner
        {
            get
            {
                if (ScoreContestantA == 2) return ContestantA;
                if (ScoreContestantB == 2) return ContestantB;
                return null;
            }
        }

        public bool HasWinner => Winner != null;

        private static int CountScore(string scoreString)
        {
            int countScore = 0;
            countScore += scoreString.Count(f => f == 'M');
            countScore += scoreString.Count(f => f == 'D');
            countScore += scoreString.Count(f => f == 'K');
            countScore += scoreString.Count(f => f == 'T');

            return countScore;
        }

    }
}
