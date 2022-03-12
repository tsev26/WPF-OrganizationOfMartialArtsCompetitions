using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace OOMAC.Domain.Models
{

    [Table("Contestant")]
    public class Contestant : DomainObject
    {

        [Column("ConId")]
        public new int Id { get; set; }

        [Column("ConLName")]
        public string LastName { get; set; }

        [Column("ConFName")]
        public string FirstName { get; set; }

        [Column("ConEmail")]
        public string Email { get; set; }

        [Column("ConAge")]
        public int Age { get; set; }

        [Column("ConTechnicalSkill")]
        public TechnicalSkill TechnicalSkill { get; set; }

        public List<Tournament> Tournaments { get; set; }


    }
}
