using System.ComponentModel.DataAnnotations;

namespace OOMAC.Domain.Models
{
    public class DomainObject
    {
        [Key]
        public int Id { get; set; }
    }
}
