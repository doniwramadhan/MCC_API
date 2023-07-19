using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIMCC.Models
{
    public abstract class BaseProp
    {
        [Key]
        [Column("guid")]
        public Guid Guid { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
      
        [Column("modified_date")]
        public DateTime ModifiedDate { get; set;}
    }
}
