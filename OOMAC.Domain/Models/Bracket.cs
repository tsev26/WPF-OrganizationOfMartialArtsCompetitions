using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OOMAC.Domain.Models
{
    [Table("Bracket")]
    public class Bracket : DomainObject
    {
        [Column("BrcId")]
        public new int Id { get; set; }

        [Column("BrcTrnId")]
        public int TournamentId { get; set; }

        [Column("BrcGroup")]
        public int Group { get; set; }

        [Column("BrcRound")]
        public int Round { get; set; }

        public Tournament Tournament { get; set; }

        public List<Match> Matches { get; set; }

        public string Name
        {
            get
            {
                if (Round == 0)
                {
                    //int minIdOfGroup = Tournament.Brackets.Where(s => s.Round == 0).Min(s => s.Id);
                    //int numberOfGroups = Tournament.Brackets.Where(s => s.Round == 0).Count();
                    //return "Skupina " + ((char)(Id - minIdOfGroup + 65)).ToString();
                    return "Skupina " + ((char)(Group + 65)).ToString();
                }
                int numberOfMatches = Matches.Count;
                switch (numberOfMatches)
                {
                    case 1:
                        return "Finále";
                    case 2:
                        return "Semifinále";
                    case 4:
                        return "Čtvrtfinále";
                    default:
                        return numberOfMatches + "-finále";
                }
            }
        }
    }
}
