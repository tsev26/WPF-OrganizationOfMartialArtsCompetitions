using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOMAC.Domain.Models
{
    [Table("Bracker")]
    public class Bracket : DomainObject
    {
        [Column("BrcId")]
        public new int Id { get; set; }

        [Column("BrcTrnId")]
        public int TournamentId { get; set; }

        public Tournament Tournament { get; set; }

        public int Round { get; set; }

        public List<Match> Matches { get; set; }

    }
}
