

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOMAC.Domain.Models
{
    [Table("Pool")]
    public class Pool : DomainObject
    {
        [Column("PolId")]
        public new int Id { get; set; }

        [Column("PolTrnId")]
        public int TournamentId { get; set; }

        public Tournament Tournament { get; set; }

        public List<Match> Matches { get; set; }
    }
}
