using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOMAC.Domain.Models
{
    [Table("Match")]
    public class Match : DomainObject
    {
        [Column("MatId")]
        public new int Id { get; set; }

        [Column("MatWinId")]
        public int WinnerId { get; set; }
        public Contestant Winner { get; set; }

        public List<MatchupEntry> Entries { get; set; }

        public Pool Pools { get; set; }

        public Bracket Brackets { get; set; }

    }
}
