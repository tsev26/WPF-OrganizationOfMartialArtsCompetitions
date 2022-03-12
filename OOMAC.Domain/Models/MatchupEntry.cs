
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OOMAC.Domain.Models
{
    [Table("MatchupEntry")]
    public class MatchupEntry : DomainObject
    {
        [Column("MaeId")]
        public new int Id { get; set; }

        [Column("MaeConId")]
        public int ContestantId { get; set; }
        public Contestant Contestant { get; set; }

        [Column("MaeScore")]
        public string ScoreString { get; set; }

        public int Score => CountScore(ScoreString);
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
