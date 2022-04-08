﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOMAC.Domain.Models
{
    [Table("Tournament")]
    public class Tournament : DomainObject
    {
        [Column("TrnId")]
        public new int Id { get; set; }

        [Column("TrnName")]
        public string Name { get; set; }

        [Column("TrnMinAge")]
        public int MinAge { get; set; }

        [Column("TrnMaxAge")]
        public int MaxAge { get; set; }

        [Column("TrnMinTechSkill")]
        public TechnicalSkill MinTechnicalSkill { get; set; }

        [Column("TrnMaxTechSkill")]
        public TechnicalSkill MaxTechnicalSkill { get; set; }

        public List<Contestant> Contestans { get; set; }

        public List<Pool> Pools { get; set; }

        public List<Bracket> Brackets { get; set; }

        public int CountContestants => Contestans.Count;

        public bool Started => Pools.Count > 0 || Brackets.Count > 0;
    }
}
